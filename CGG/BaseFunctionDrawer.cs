using System;
using System.Drawing;

namespace CGG
{
    public abstract class BaseFunctionDrawer
    {
        protected readonly Size Size;
        protected readonly double A;
        protected readonly double B;
        protected readonly Func<double, double> Function;
        protected Point Center => new Point((int) (-A * Size.Width / (B - A)), Size.Height / 2);

        protected BaseFunctionDrawer(Size size, double a, double b, Func<double, double> function)
        {
            Size = size;
            A = a;
            B = b;
            Function = function;
        }

        protected double CalculateY(double xx)
        {
            var x = A + xx * (B - A) / Size.Width;
            return Function(x);
        }
        
        protected (double maxY, double minY) FindMaxAndMin()
        {
            var min = Function(A);
            var max = Function(A);
            for (var xx = 0; xx < Size.Width; xx++)
            {
                var y = CalculateY(xx);

                if (y > max)
                    max = y;
                else if (y < min)
                    min = y;
            }

            return (max, min);
        }
    }
}