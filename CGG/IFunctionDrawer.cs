using System.Collections.Generic;
using System.Drawing;

namespace CGG
{
    public interface IFunctionDrawer
    {
        IEnumerable<Point> GetPoints();
    }
}