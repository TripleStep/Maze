using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Maze
{
    public class Range : IEnumerable<int>
    {
        int start, end;

        public int Start { get { return start; } }
        public int End { get { return end; } }

        public Range(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = start; i < end; ++i)
                yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
