using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Maze
{
    public class PickContext
    {
        public readonly Grid Grid;

        public PickContext(Grid g)
        {
            this.Grid = g;
        }

        public Point[] Points(Pt[] vs)
        {
            Point[] pts = new Point[vs.Length];
            for (int i = 0; i < vs.Length; ++i)
                pts[i] = Point(vs[i]);
            return pts;
        }

        public Point Point(Pt p)
        {
            return Grid.DefPoint(p);
        }

        public static int Distance2(Point p0, Point p1)
        {
            return (p0.X - p1.X) * (p0.X - p1.X) + (p0.Y - p1.Y) * (p0.Y - p1.Y);
        }

        public Pt Pt(Point p)
        {
            Pt loop = Grid.DefPt(p);
            bool changed = true;
            while (changed)
            {
                changed = false;
                int d = Distance2(Grid.DefPoint(loop), p);
                foreach (Pt n in Grid.Neighbours(loop))
                {
                    int d1 = Distance2(Grid.DefPoint(n), p);
                    if (d1 < d)
                    {
                        changed = true;
                        d = d1;
                        loop = n;
                    }
                }
            }
            return loop;
        }
    }
}