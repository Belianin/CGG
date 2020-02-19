using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public class ScaleFunctionDrawer : BaseFunctionDrawer, IFunctionDrawer
    {
        private readonly double maxY;
        private readonly double minY;
        private readonly Func<double, double> scale;

        public ScaleFunctionDrawer(Size size, double a, double b, Func<double, double> function, ScaleMode scaleMode)
            : base(size, a, b, function)
        {
            (maxY, minY) = FindMaxAndMin();
            
            switch (scaleMode)
            {
                case ScaleMode.None:
                    scale = y => y + Center.Y;
                    break;
                case ScaleMode.Proportional:
                    scale = y => (y - maxY) * Size.Height / (minY - maxY);
                    break;
            }
        }

        public IEnumerable<Point> GetPoints()
        {
            for (var xx = 0; xx < Size.Width; xx++)
            {
                var yy = (int) scale(CalculateY(xx));
                yield return new Point(xx, yy);
            }
        }
    }
}