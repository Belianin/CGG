using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public class ScaleFunctionDrawer : BaseFunctionDrawer, IFunctionDrawer
    {
        private readonly int maxY;
        private readonly int minY;
        private readonly Func<double, double> scale;

        public ScaleFunctionDrawer(Size size, int a, int b, Func<double, double> function, ScaleMode scaleMode)
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

            maxY = 10;
            minY = -10;
        }

        public IEnumerable<Point> GetPoints()
        {
            for (var xx = 0; xx < Size.Width; xx++)
            {
                var y = (int) scale(CalculateY(xx));
                yield return new Point(xx, y);
            }
        }

        private (int maxY, int minY) FindMaxAndMin()
        {
            var min = Function(A);
            var max = Function(A);
            for (var xx = 0; xx < Size.Width; xx++)
            {
                var y = CalculateY(xx);

                if (y > maxY)
                    max = y;
                else if (y < minY)
                    min = y;
            }

            return ((int) max, (int) min);
        }
    }
}