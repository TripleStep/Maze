using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Maze
{
    public struct Pt
    {
        public readonly int X, Y;

        public Pt(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Pt))
                return false;
            Pt pt = (Pt)obj;
            return pt.X == X && pt.Y == Y;
        }

        public int CompareTo(Pt p)
        {
            if (X != p.X)
                return X - p.X;
            return Y - p.Y;
        }

        public override int GetHashCode()
        {
            return X * 100 + Y * 101;
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }

        public Pt Off(int x, int y)
        {
            return new Pt(X + x, Y + y);
        }

        public static Pt Midway(Pt pt0, Pt pt1)
        {
            return new Pt((pt0.X + pt1.X) / 2, (pt0.Y + pt1.Y) / 2);
        }

        public Pt[] Neighbours6
        {
            get
            {
                return new Pt[]{
                    Off(-3,-1),
                    Off(0,-2),
                    Off(3,-1),
                    Off(3,1),
                    Off(0,2),
                    Off(-3,1),
                };
            }
        }

        public Pt[] Vertices6
        {
            get
            {
                return new Pt[]{
                    Off(-2,0),
                    Off(-1,-1),
                    Off(1,-1),
                    Off(2,0),
                    Off(1,1),
                    Off(-1,1),
                };
            }
        }

        public Pt[] Neighbours4
        {
            get
            {
                return new Pt[]{
                    Off(-2,0),
                    Off(0,-2),
                    Off(2,0),
                    Off(0,2),
                };
            }
        }

        public Pt[] Vertices4
        {
            get
            {
                return new Pt[]{
                    Off(-1,1),
                    Off(-1,-1),
                    Off(1,-1),
                    Off(1,1),
                };
            }
        }

        bool PointsUp3 { get { return (X + Y) % 4 == 0; } }

        public Pt[] Neighbours3
        {
            get
            {
                if (PointsUp3)
                    return new Pt[]{
                        Off(-2,0),
                        Off(2,0),
                        Off(0,2),
                    };
                else
                    return new Pt[]{
                        Off(2,0),
                        Off(-2,0),
                        Off(0,-2),
                    };
            }
        }

        public Pt[] Vertices3
        {
            get
            {
                if (PointsUp3)
                    return new Pt[]{
                        Off(-2,1),
                        Off(0,-1),
                        Off(2,1),
                    };
                else
                    return new Pt[]{
                        Off(2,-1),
                        Off(0,1),
                        Off(-2,-1),
                    };
            }
        }
    }
}
