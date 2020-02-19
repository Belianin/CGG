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
        private double a = -5;
        private double b = 10;
        private Point Center => new Point((int) (-a * ClientSize.Width / (b - a)), ClientSize.Height / 2);
        private Settings settings = new Settings();
        
        public MainForm()
        {
            InitializeComponent();
            BackColor = settings.Theme.Background;
            //FormBorderStyle = FormBorderStyle.None;
            ClientSize = new Size(1024, 768);
            Invalidate();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                Invalidate();
            else if (e.KeyCode == Keys.Enter)
            {
//                var grid = new PropertyGrid();
//                Controls.Add(grid);
//                grid.SelectedObject = settings;
//                grid.Show();
//                Invalidate();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //var delta = 1 + (b - a) / 200;
            if (e.Delta > 0)
            {
                b *= 1.01;
                a *= 1.01;
                Invalidate();
            }
            else if (e.Delta < 0)
            {
                b *= 0.99;
                a *= 0.99;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawAxises(e.Graphics);
            var g = e.Graphics;
            var drawer = new ScaleFunctionDrawer(ClientSize, a, b, MathFunction, settings.ScaleMode); // new FunctionDrawer(ClientSize, a, b, MathFunction);
            DrawFunction(g, drawer.GetPoints(), settings.Theme.Function);
            
            // drawer = new ScaleFunctionDrawer(ClientSize, a, b, x => x, settings.ScaleMode);
            // DrawFunction(g, drawer.GetPoints(), Color.Red);
        }

        private void DrawFunction(Graphics g, IEnumerable<Point> points, Color color)
        {
            var pen = new Pen(color);
            var prevPoint = new Point();
            
            foreach (var point in points)
            {
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
                g.DrawString(Math.Round(x * b / scale, 2).ToString(CultureInfo.CurrentCulture), font, new SolidBrush(settings.Theme.Axis), xx, Center.Y + 5);
            }
        }

        private static double MathFunction(double x)
        {
            //return Math.Sin(x) * 100;
            return x * Math.Sin(x * x) * 2;
        }
    }

    internal class Settings
    {
        public ScaleMode ScaleMode { get; set; } = ScaleMode.Proportional;
        
        public Theme Theme { get; set; } = Themes.Dark;
    }
}