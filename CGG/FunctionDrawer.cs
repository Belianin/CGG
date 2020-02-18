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

        public IEnumerable<Point> GetPoints()
        {
            for (var xx = 0; xx < size.Width; xx++)
            {
                var y = CalculateY(xx);
                var yy = y;//(y - maxY) * size.Height / (minY - maxY);
                yield return new Point(xx, (int) yy);
            }
        }
        
        private double CalculateY(int xx)
        {
            var x = a + xx * (b - a) / (double) size.Width;
            return function(x);
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