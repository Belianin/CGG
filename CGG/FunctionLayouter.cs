using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public class FunctionLayouter
    {
        private readonly Size size;
        private readonly FunctionParameters function;
        private readonly Func<double, double> scale;
        private Point Center => new Point((int) (-function.Alpha * size.Width / (function.Beta - function.Alpha)), size.Height / 2);

        public FunctionLayouter(Size size, FunctionParameters function, ScaleMode scaleMode)
        {
            var (maxY, minY) = FindMaxAndMin();
            this.size = size;
            this.function = function;

            scale = scaleMode switch
            {
                ScaleMode.None => (y => -y + Center.Y),
                ScaleMode.Proportional => (y => (y - maxY) * this.size.Height / (minY - maxY)),
                _ => scale
            };
        }

        public IEnumerable<IEnumerable<Point>> GetPoints()
        {
            var result = new List<Point>();
            for (var xx = 0; xx < size.Width; xx++)
            {
                if (!TryCalculateY(xx, out var y))
                {
                    if (result.Count != 0)
                    {
                        yield return result;
                        result = new List<Point>();
                    }
                }
                else
                {
                    var yy = scale(y);
                    result.Add(new Point(xx, (int) yy));
                }
            }

            if (result.Count != 0)
                yield return result;
        }

        private bool TryCalculateY(double xx, out double y)
        {
            var x = function.Alpha + xx * (function.Beta - function.Alpha) / size.Width;
            return function.TryEvaluate(x, out y);
        }

        private (double maxY, double minY) FindMaxAndMin()
        {
            var min = 0d;
            var max = 0d;
            for (var xx = 0; xx < size.Width; xx++)
            {
                if (!function.TryEvaluate(xx, out var y))
                    continue;

                if (y > max)
                    max = y;
                else if (y < min)
                    min = y;
            }

            return (max, min);
        }
    }
}