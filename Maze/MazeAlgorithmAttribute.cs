using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    [System.AttributeUsage(AttributeTargets.Method)]
    class MazeAlgorithmAttribute : Attribute
    {
        Grid.Algorithms alg;

        public Grid.Algorithms Algorithm { get { return alg; } }

        public MazeAlgorithmAttribute(Grid.Algorithms alg)
        {
            this.alg = alg;
        }
    }
}
