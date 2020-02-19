using System;
using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public class FunctionDrawer : BaseFunctionDrawer, IFunctionDrawer
    {
        public FunctionDrawer(Size size, double a, double b, Func<double, double> function) 
            : base(size, a, b, function) {}

        public IEnumerable<Point> GetPoints()
        {
            for (var xx = 0; xx < Size.Width; xx++)
                yield return new Point(xx, (int) CalculateY(xx));
        }
    }
}