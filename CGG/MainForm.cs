using System;
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
        private Point Center => new Point((int) (-a * Size.Width / (b - a)), Size.Height / 2);
        private Settings settings = new Settings();
        
        public MainForm()
        {
            InitializeComponent();
            BackColor = settings.Theme.Background;
            Size = new Size(1024, 768);
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
            if (e.Delta > 0)
            {
                b += 0.5;
                a -= 0.5;
                Invalidate();
            }
            else if (e.Delta < 0)
            {
                b -= 0.5;
                a += 0.5;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawAxises(e.Graphics);
            var g = e.Graphics;
            var pen = new Pen(settings.Theme.Function);
            var drawer = new ScaleFunctionDrawer(Size, a, b, MathFunction, settings.ScaleMode);// new FunctionDrawer(Size, a, b, MathFunction);
            var prevPoint = new Point();
            foreach (var point in drawer.GetPoints())
            {
                var shiftedPoint = point;
                g.DrawLine(pen, prevPoint, shiftedPoint);
                prevPoint = shiftedPoint;
            }
        }

        private void DrawAxises(Graphics g)
        {
            var pen = new Pen(settings.Theme.Axis);
            g.DrawLine(pen, 0, Center.Y, Size.Width, Center.Y);
            g.DrawLine(pen, Center.X, 0, Center.X, Size.Height);
            
            const int scale = 10;
            var step = (Size.Width - Center.X) / scale;
            for (var x = 0; x < scale; x++)
            {
                var xx = Center.X + x * step;
                g.DrawLine(pen, xx, Center.Y - 5, xx, Center.Y + 5);
                g.DrawString(Math.Round(x * b / scale, 2).ToString(CultureInfo.CurrentCulture), font, new SolidBrush(settings.Theme.Axis), xx, Center.Y + 5);
            }
        }

        private static double MathFunction(double x)
        {
            return x * Math.Sin(x * x) * 2;
        }
    }

    internal class Settings
    {
        public ScaleMode ScaleMode { get; set; } = ScaleMode.Proportional;
        
        public Theme Theme { get; set; } = Themes.Dark;
    }
}