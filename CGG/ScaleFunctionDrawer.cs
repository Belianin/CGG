using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public class ScaleFunctionDrawer : BaseFunctionDrawer, IFunctionDrawer
    {
        private readonly Func<double, double> scale;

        public ScaleFunctionDrawer(Size size, double a, double b, Func<double, double> function, ScaleMode scaleMode)
            : base(size, a, b, function)
        {
            var (maxY, minY) = FindMaxAndMin();
            
            switch (scaleMode)
            {
                case ScaleMode.None:
                    scale = y => -y + Center.Y;
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
                var yy = scale(CalculateY(xx));
                yield return new Point(xx, (int) yy);
            }
        }
    }
}