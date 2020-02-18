using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace CGG
{
    public partial class MainForm : Form
    {
        private Font font = new Font("Arial", 10);
        private int a = -5;
        private int b = 50;
        private Point Center => new Point(-a * Scale, Size.Height / 2);
        private int Scale => Size.Width / (b - a);
        private Settings settings = new Settings();
        
        public MainForm()
        {
            InitializeComponent();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawAxises(e.Graphics);
            var g = e.Graphics;
            var pen = Pens.DodgerBlue;
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
            g.DrawLine(Pens.Black, 0, Center.Y, Size.Width, Center.Y);
            g.DrawLine(Pens.Black, Center.X, 0, Center.X, Size.Height);
            
            const int scale = 10;
            var step = (Size.Width - Center.X) / scale;
            for (var x = 0; x < scale; x++)
            {
                var xx = Center.X + x * step;
                g.DrawLine(Pens.Black, xx, Center.Y - 5, xx, Center.Y + 5);
                g.DrawString((x * b / (double) scale).ToString(CultureInfo.CurrentCulture), font, Brushes.Black, xx, Center.Y + 5);
            }
        }

        private static double MathFunction(double x)
        {
            return Math.Sin(x) * 10;
            if (x < 1 && x > -1)
                return 10;
            if (x < 10)
                return x;
            if (x < 30)
                return -x / 2;
            return x;
            //return x - 2;
            return Math.Sin(x) * 100;
            //return x * Math.Sin(x * x);
        }
    }

    internal class Settings
    {
        public ScaleMode ScaleMode { get; set; } = ScaleMode.None;
    }
}