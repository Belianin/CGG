using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public class FunctionDrawer
    {
        private readonly Size size;
        private readonly int a;
        private readonly int b;
        private readonly Func<double, double> function;
        private readonly double maxY;
        private readonly double minY;

        public FunctionDrawer(Size size, int a, int b, Func<double, double> function)
        {
            this.size = size;
            this.a = a;
            this.b = b;
            this.function = function;

            (maxY, minY) = FindMaxAndMin();
        }

        public IEnumerable<Point> GetPoints(ScaleMode scale)
        {
            for (var xx = 0; xx < size.Width; xx++)
            {
                var y = CalculateY(xx);
                var yy = Scale(scale, y);
                yield return new Point(xx, (int) yy);
            }
        }
        
        private double CalculateY(int xx)
        {
            var x = a + xx * (b - a) / (double) size.Width;
            return function(x);
        }

        private double Scale(ScaleMode scaleMode, double y)
        {
            return scaleMode switch
            {
                ScaleMode.None => y,
                ScaleMode.Y => (y - maxY) * size.Height / (minY - maxY),
                _ => throw new ArgumentOutOfRangeException(nameof(scaleMode), scaleMode, null)
            };
        }

        private (double maxY, double minY) FindMaxAndMin()
        {
            var min = 0d;
            var max = 0d;
            for (var xx = 0; xx < size.Width; xx++)
            {
                var y = CalculateY(xx);

                if (y > maxY)
                    max = y;
                else if (y < minY)
                    min = y;
            }

            return (max, min);
        }
    }
}