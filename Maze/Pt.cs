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

        int Kind3
        {
            get
            {
                int ix = X % 4, iy = Y % 6;
                if (ix < 0)
                    ix += 4;
                if (iy < 0)
                    iy += 6;
                if (ix == 0)
                {
                    if (iy == 2)
                        return 0;
                    else if (iy == 4)
                        return 3;
                }
                else if (ix == 2)
                {
                    if (iy == 1)
                        return 1;
                    else if (iy == 5)
                        return 2;
                }
                return -1;
            }
        }

        public Pt[] Neighbours3
        {
            get
            {
                switch (Kind3)
                {
                    case 0:
                    case 2:
                        return new Pt[]
                        {
                            Off(0,2),
                            Off(-2,-1),
                            Off(2,-1),
                        };
                    case 1:
                    case 3:
                        return new Pt[]
                        {
                            Off(0,-2),
                            Off(2,1),
                            Off(-2,1),
                        };
                    default:
                        return null;
                }
            }
        }

        public Pt[] Vertices3
        {
            get
            {
                switch (Kind3)
                {
                    case 0:
                    case 2:
                        return new Pt[]
                        {
                            Off(2,1),
                            Off(-2,1),
                            Off(0,-2),
                        };
                    case 1:
                    case 3:
                        return new Pt[]
                        {
                            Off(-2,-1),
                            Off(2,-1),
                            Off(0,2),
                        };
                    default:
                        return null;
                }
            }
        }
    }
}
