using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Maze
{
    public class PaintContext : PickContext
    {
        readonly Graphics g;

        public Graphics Graphics { get { return g; } }

        public PaintContext(Grid grid, Graphics g)
            :base(grid)
        {
            this.g = g;
        }

    }
}
