using System;
using System.Drawing;
using System.Windows.Forms;

namespace CGG
{
    public partial class MainForm : Form
    {
        private Font font = new Font("Arial", 10);
        private int a = -20;
        private int b = 80;
        private Point Center => new Point(-a * Size.Width / (b - a), Size.Height / 2);
        private int Scale => Size.Width / (b - a);
        
        public MainForm()
        {
            InitializeComponent();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawAxises(e.Graphics, Center, Scale);
            var g = e.Graphics;
            var pen = Pens.DodgerBlue;
            var drawer = new FunctionDrawer(Size, a, b, MathFunction);
            var prevPoint = new Point();
            foreach (var point in drawer.GetPoints())
            {
                var shiftedPoint = new Point(point.X, point.Y + Center.Y);
                g.DrawLine(pen, prevPoint, shiftedPoint);
                prevPoint = shiftedPoint;
            }
        }

        private void DrawAxises(Graphics g, Point center, int scale)
        {
            g.DrawLine(Pens.Black, 0, center.Y, Size.Width, center.Y);
            const int xCount = 10;
            for (var x = 0; x < xCount; x++)
            {
                var xx = center.X + x * Size.Width / xCount;
                g.DrawLine(Pens.Black, xx, center.Y - 5, xx, center.Y + 5);
                g.DrawString((x * scale).ToString(), font, Brushes.Black, xx, center.Y + 5);
            }
            
            g.DrawLine(Pens.Black, center.X, 0, center.X, Size.Height);
            const int yCount = 10;
            for (var y = 0; y < yCount; y++)
            {
                var yy = center.Y + y * Size.Height / yCount;
                g.DrawLine(Pens.Black, center.X - 5, yy, center.X + 5, yy);
                //g.DrawString((x * scale).ToString(), font, brush, xx, center.Y + 5);
            }
        }

        private static double MathFunction(double x)
        {
            //return x - 2;
            return Math.Sin(x) * 100;
            //return x * Math.Sin(x * x);
        }
    }
}