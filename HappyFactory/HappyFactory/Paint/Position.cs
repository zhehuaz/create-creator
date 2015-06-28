using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFactory.Paint
{
    /// <summary>
    /// 
    /// </summary>
    public class Position
    {
        int Left { get; set; }
        int Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public Position(int left, int top)
        {
            this.Left = left;
            this.Top = top;
        }

        public Position() : this(0, 0) { }
    }
}
