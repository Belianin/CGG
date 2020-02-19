using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace CGG
{
    public partial class MainForm : Form
    {
        private Font font = new Font("Arial", 10);
        private FunctionParameters function;
        private Point Center => new Point((int) (-function.Alpha * ClientSize.Width / (function.Beta - function.Alpha)), ClientSize.Height / 2);
        private Settings settings = new Settings();
        private bool functionOpen = false;
        private PropertyGrid grid;
        
        public MainForm()
        {
            InitializeComponent();
            BackColor = settings.Theme.Background;
            //FormBorderStyle = FormBorderStyle.None;
            ClientSize = new Size(1024, 768);
            function = new FunctionParameters();
            grid = new PropertyGrid();
            Controls.Add(grid);
            grid.SelectedObject = function;
            Invalidate();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                Invalidate();
            else if (e.KeyCode == Keys.Enter)
            {
                if (functionOpen)
                {
                    grid.Hide();
                    functionOpen = false;
                }
                else
                {
                    grid.Show();
                    functionOpen = true;
                }
                Invalidate();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //var delta = 1 + (b - a) / 200;
            if (e.Delta > 0)
            {
                function.Alpha *= 1.01;
                function.Beta *= 1.01;
                Invalidate();
            }
            else if (e.Delta < 0)
            {
                function.Alpha *= 0.99;
                function.Beta *= 0.99;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawAxises(e.Graphics);
            var g = e.Graphics;
            var drawer = new FunctionLayouter(ClientSize, function, settings.ScaleMode); // new FunctionDrawer(ClientSize, a, b, MathFunction);
            foreach (var line in drawer.GetPoints())
            {
                DrawFunction(g, line, settings.Theme.Function);
            }
            
            // drawer = new ScaleFunctionDrawer(ClientSize, a, b, x => x, settings.ScaleMode);
            // DrawFunction(g, drawer.GetPoints(), Color.Red);
        }

        private void DrawFunction(Graphics g, IEnumerable<Point> points, Color color)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var pen = new Pen(color, 2);
            var prevPoint = new Point();
            var first = true;

            foreach (var point in points)
            {
                if (first)
                {
                    prevPoint = point;
                    first = false;
                    continue;
                }
                
                g.DrawLine(pen, prevPoint, point);
                prevPoint = point;
            }
            
        }

        private void DrawAxises(Graphics g)
        {
            var pen = new Pen(settings.Theme.Axis);
            g.DrawLine(pen, 0, Center.Y, ClientSize.Width, Center.Y);
            g.DrawLine(pen, Center.X, 0, Center.X, ClientSize.Height);
            
            const int scale = 10;
            var step = (ClientSize.Width - Center.X) / scale;
            for (var x = 0; x < scale; x++)
            {
                var xx = Center.X + x * step;
                g.DrawLine(pen, xx, Center.Y - 5, xx, Center.Y + 5);
                g.DrawString(Math.Round(x * function.Beta / scale, 2).ToString(CultureInfo.InvariantCulture), font, new SolidBrush(settings.Theme.Axis), xx, Center.Y + 5);
            }
        }
    }

    internal class Settings
    {
        public ScaleMode ScaleMode { get; set; } = ScaleMode.None;
        
        public Theme Theme { get; set; } = Themes.Default;
    }
}