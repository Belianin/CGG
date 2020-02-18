using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public class XFunctionDrawer : BaseFunctionDrawer, IFunctionDrawer
    {
        private readonly int maxY;
        private readonly int minY;
        
        public XFunctionDrawer(Size size, int a, int b, Func<double, double> function)
            : base(size, a, b, function)
        {
            (maxY, minY) = FindMaxAndMin();
        }

        public IEnumerable<Point> GetPoints()
        {
            var dy = Size.Width / (double) Size.Height;
            var dx = Size.Height / (double) Size.Width;
            for (var x = 0; x < Size.Width; x++)
            {
                var xx = (x - A) * Size.Width / (B - A);
                var yy = (CalculateY(xx) - maxY) * Size.Width / (B - A) * dy / dx;
                yield return new Point(xx, (int) yy);
            }
        }
    }
}