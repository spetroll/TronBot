using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronBot
{
    public class Point : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }




        #region ICloneable Member

        public object Clone()
        {
            return new Point(this.X, this.Y);
        }

        #endregion
    }
}
