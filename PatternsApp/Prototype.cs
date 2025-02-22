using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    #region Task
    public class Point
    {
        public int X, Y;
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            return new Line()
            {
                Start = new Point()
                {
                    X = this.Start.X,
                    Y = this.Start.Y
                },
                End = new Point()
                {
                    X = this.End.X,
                    Y = this.End.Y
                },
            };
        }
    }
    #endregion
}
