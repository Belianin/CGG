using System;

namespace CGG
{
    public class FunctionParameters
    {
        public double A { get; set; } = 10;

        public double B { get; set; } = 10;

        public double Alpha { get; set; } = -10;

        public double Beta { get; set; } = 10;

        public bool TryEvaluate(double x, out double y)
        {
            y = 0;
            var sqrt = x * x * x / (B + x);
            if (sqrt < 0)
                return false;

            y = A - x + Math.Sqrt(sqrt);
            return true;
        }
    }
}