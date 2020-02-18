using System;
using System.Drawing;

namespace CGG
{
    public abstract class BaseFunctionDrawer
    {
        protected readonly Size Size;
        protected readonly int A;
        protected readonly int B;
        protected readonly Func<double, double> Function;
        protected Point Center => new Point(-A * Size.Width / (B - A), Size.Height / 2);

        protected BaseFunctionDrawer(Size size, int a, int b, Func<double, double> function)
        {
            Size = size;
            A = a;
            B = b;
            Function = function;
        }

        protected double CalculateY(int xx)
        {
            var x = A + xx * (B - A) / (double) Size.Width;
            return Function(x);
        }
        
        protected (int maxY, int minY) FindMaxAndMin()
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

            return ((int) max, (int) min);
        }
    }
}