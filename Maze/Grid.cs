#define WALL_SINGLE

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace Maze
{
    public abstract class Grid
    {

        public enum Shapes { Triangular, Square, Hexagonal };
        public abstract Shapes Shape { get; }

        public enum Algorithms
        {
            Prims, Wilsons, Kruskals, Ellers,
            //RecursiveFiller,
            RecursiveBacktrack, BinaryTree, Sidewinder, RecursiveDivision,
            PrimLike, LowRiver, HighRiver, LongWindy
        };
        Algorithms algorithm;
        public Algorithms Algorithm { get { return algorithm; } set { algorithm = value; } }

        static Random rnd = new Random();
        public const int In = 1, Frontier = 2, Out = 3;
        Dictionary<Pt, int> _cells = new Dictionary<Pt, int>();
        Dictionary<Pt, int> _walls = new Dictionary<Pt, int>();
        Pt[] _soln;
        public abstract Pt[] Neighbours(Pt pt);
        public abstract Pt[] Vertices(Pt pt);
        public abstract void Init(Pt sz);
        public abstract Point DefPoint(Pt p);
        public abstract Pt DefPt(Point p);
        public virtual bool PlotsMidway { get { return false; } }
        public abstract Func<Pt, int>[] Axes { get; }
        public Pt origin;

        public Grid()
        {
        }

        public static int Rnd(int i)
        {
            return rnd.Next(i);
        }
        static T RandomOf<T>(T[] a, out int i)
        {
            i = Rnd(a.Length);
            return a[i];
        }
        public static T RandomOf<T>(T[] a)
        {
            int i;
            return RandomOf(a, out i);
        }
        public static T LastOfList<T>(List<T> a)
        {
            return a[a.Count - 1];
        }
        public static T RandomOfList<T>(List<T> a)
        {
            int i = Rnd(a.Count);
            return a[i];
        }
        public static T UsuallyLastOfList<T>(List<T> a)
        {
            if (Rnd(4) == 1)
                return RandomOfList(a);
            return a[a.Count - 1];
        }
        public static T FromFirstFewOfList<T>(List<T> a)
        {
            //int n = 10;
            //int i = Rnd(Math.Min(a.Count, n));
            //return a[i];

            int n = 3 + a.Count / 2;
            int i = Rnd(Math.Min(a.Count, n));
            return a[i];
        }
        public static T FromLastFewOfList<T>(List<T> a)
        {
            int n = 10;
            //int n = 10 + a.Count / 10;
            int i = a.Count - 1 - Rnd(Math.Min(a.Count, n));
            return a[i];
        }
        static int IndexOf<T>(T[] a, T e)
        {
            for (int i = 0; i < a.Length; ++i)
                if (a[i].Equals(e))
                    return i;
            return -1;
        }
        static bool GetBit(int w, int i)
        {
            return 0 != (w & (1 << i));
        }
        static int SetBit(int w, int i, bool f)
        {
            if (f)
                return (1 << i) | w;
            return w & ~(1 << i);
        }
        public int GetCell(Pt pt)
        {
            int ret = 0;
            _cells.TryGetValue(pt, out ret);
            return ret;
        }
        public void SetCell(Pt pt, int v)
        {
            _cells[pt] = v;
        }
        int GetWall(Pt pt)
        {
            int ret = 0;
            _walls.TryGetValue(pt, out ret);
            return ret;
        }
        void SetWall(Pt pt, int v)
        {
            _walls[pt] = v;
        }
        public void AllWalls()
        {
            foreach (Pt p in Locs)
                if (GetCell(p) != 0)
                    foreach (Pt n in Neighbours(p))
                        SetWall(p, n, true);
        }
        public void WalledEdges()
        {
            foreach (Pt p in Locs)
                foreach (Pt n in Neighbours(p))
                    if (GetCell(p) != GetCell(n))
                        SetWall(p, n, true);
        }

        public bool GetWall(Pt pt0, Pt pt1)
        {
            Pt[] n = Neighbours(pt0);
            int i = IndexOf(n, pt1);
            if (i == -1)
                return false;
#if WALL_SINGLE
            //if (i * 2 >= n.Length)
            if (pt0.CompareTo(pt1) < 0)
                return GetWall(pt1, pt0);
#endif
            return GetBit(GetWall(pt0), i);
        }
#if WALL_SINGLE
        public void SetWall(Pt pt0, Pt pt1, bool b)
        {
            Pt[] n = Neighbours(pt0);
            int i = IndexOf(n, pt1);
            if (i == -1)
                return;
            if (pt0.CompareTo(pt1) < 0)
            {
                SetWall(pt1, pt0, b);
                return;
            }
            SetWall(pt0, SetBit(GetWall(pt0), i, b));
        }
#else
        public void SetWall(Pt pt0, Pt pt1, bool b)
        {
            int i0 = IndexOf(Neighbours(pt0), pt1);
            int i1 = IndexOf(Neighbours(pt1), pt0);
            if (i0 == -1 || i1 == -1)
                return;
            SetWall(pt0, SetBit(GetWall(pt0), i0, b));
            SetWall(pt1, SetBit(GetWall(pt1), i1, b));
        }
#endif
        public void Paint(PaintContext pc, Color solutionColor, Color wallColor, Color cellColor)
        {
            //using (Brush br = new SolidBrush(cellColor))
            //using (Pen pe = new Pen(cellColor))
            //    FillCells(pc, pe, br);
            DrawSolution(pc, solutionColor);
            using (Pen pe = new Pen(wallColor, 1.5f))
                DrawWalls(pc, pe);
        }
        //public void FillCell(PaintContext pc, Pt pt)
        //{
        //    FillCell(pc, pt, GetCell(pt) == 0 ? Brushes.Red : Brushes.Green);
        //}
        public void FillCell(PaintContext pc, Pt pt, Color cellColor)
        {
            using (Brush br = new SolidBrush(cellColor))
            using (Pen pe = new Pen(cellColor))
                FillCell(pc, pt, pe, br);
        }
        public void FillCell(PaintContext pc, Pt pt, Pen pe, Brush br)
        {
            Pt[] vs = Vertices(pt);
            Point[] pts = pc.Points(vs);
            pc.Graphics.DrawPolygon(Pens.Green, pts);
            pc.Graphics.FillPolygon(br, pts);
        }
        public void FillCells(PaintContext pc, Pen pe, Brush br)
        {
            foreach (Pt pt in Locs)
                FillCell(pc, pt, pe, br);
        }
        public void DrawSolution(PaintContext pc, Color solutionColor)
        {
            if (_soln == null)
                return;
            //using (Pen pe = new Pen(Color.Black, 1.5f))
            using (Pen pe = new Pen(solutionColor, 5.0f))
            {
                pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                pc.Graphics.DrawLines(pe, pc.Points(_soln));
            }
        }
        public void DrawWalls(PaintContext pc, Pen pe)
        {
            foreach (KeyValuePair<Pt, int> kvp in _walls)
            {
                Pt pt0 = kvp.Key;
                int w = kvp.Value;
                Pt[] vs = Vertices(pt0);
                Pt[] ns = Neighbours(pt0);
                for (int i = 0; i < vs.Length; ++i)
                {
                    Pt pt1 = ns[i];
                    //if (pt0.CompareTo(pt1) >= 0)
                    if (GetBit(w, i))
                    {
                        Pt v0 = vs[i];
                        Pt v1 = vs[(i + 1) % vs.Length];
                        pc.Graphics.DrawLine(pe, pc.Point(v0), pc.Point(v1));
                    }
                }
            }
        }
        public IEnumerable<Pt> Locs { get { return _cells.Keys; } }

        public Pt[] Select(IEnumerable<Pt> e, int i)
        {
            List<Pt> pts = new List<Pt>();
            foreach (Pt p in e)
                if (GetCell(p) == i)
                    pts.Add(p);
            return pts.ToArray();
        }

        public Pt[] SelectNot(IEnumerable<Pt> e, int i)
        {
            List<Pt> pts = new List<Pt>();
            foreach (Pt p in e)
                if (GetCell(p) != i)
                    pts.Add(p);
            return pts.ToArray();
        }

        void SetInWithFrontier(Pt c)
        {
            SetCell(c, In);
            foreach (Pt n in Neighbours(c))
                if (GetCell(n) == Out)
                    SetCell(n, Frontier);
        }

        protected void ChangeCells(int from, int to)
        {
            Pt[] pts = Select(Locs, from);
            foreach (Pt p in pts)
                SetCell(p, to);
        }

        public MethodInfo GetMethod(Algorithms alg)
        {
            foreach (MethodInfo m in GetType().GetMethods())
                foreach (object o in m.GetCustomAttributes(true))
                    if (o is MazeAlgorithmAttribute)
                    {
                        MazeAlgorithmAttribute maa = (MazeAlgorithmAttribute)o;
                        if (maa.Algorithm == alg)
                            return m;
                    }
            return null;
        }

        public void Generate(Pt sz)
        {
            Clear();
            Init(sz);
            origin = Locs.First();
            MethodInfo m = GetMethod(Algorithm);
            if (m != null)
                m.Invoke(this, new object[0]);
        }

/*
        [MazeAlgorithm(Algorithms.RecursiveFiller)]
        public void RecursiveFiller()
        {
            AllWalls();
            int x0, x1, y0, y1;
            GetBounds(out x0, out x1, out y0, out y1, Out);
            SquareGrid sq = new SquareGrid();
            sq.Init(new Pt((x1 - x0) / 2, (y1 - y0) / 2));
            sq.Ellers();
            foreach (Pt sqFrom in sq.Locs)
            {
                Pt ptFrom = new Pt(sqFrom.X * 2, sqFrom.Y * 2);
                foreach(Pt sqTo in sq.Neighbours(sqFrom))
                    if (!sq.GetWall(sqFrom, sqTo))
                    {
                        Pt ptTo = new Pt(sqTo.X * 2, sqTo.Y * 2);
                        Debug.Assert(_cells.ContainsKey(ptFrom));
                        Debug.Assert(_cells.ContainsKey(ptTo));
                        SetCell(ptFrom, In);
                        SetCell(ptTo, In);
                        Pt loop = ptFrom;
                        while (!loop.Equals(ptTo))
                        {

                        }
                    }
            }
        }
 * */

        [MazeAlgorithm(Algorithms.Prims)]
        public void Prims()
        {
            // All cells are out to begin with
            AllWalls();
            // Pick a cell to be in
            SetInWithFrontier(RandomOf(Select(Locs, Out)));
            while (true)
            {
                // Choose a frontier cell
                Pt[] pts = Select(Locs, Frontier);
                if (pts.Length == 0)
                    return;
                Pt pt0 = RandomOf(pts);
                // Choose a neighbour that is in
                Pt pt1 = RandomOf(Select(Neighbours(pt0), In));
                // Break down the wall between them
                SetWall(pt0, pt1, false);
                // The frontier cell becomes in
                SetInWithFrontier(pt0);
            }
        }

        [MazeAlgorithm(Algorithms.Wilsons)]
        public void Wilsons()
        {
            AllWalls();
            // Set a cell to be in
            SetCell(RandomOf(Select(Locs, Out)), In);
            while (true)
            {
                Pt[] pts = Select(Locs, Out);
                if (pts.Length == 0)
                    return;
                Pt start = RandomOf(pts);
                Dictionary<Pt, int> dirns = new Dictionary<Pt, int>();
                Pt loop = start;
                while (GetCell(loop) != In)
                {
                    Pt[] n = Neighbours(loop);
                    Pt next = RandomOf(SelectNot(n, 0));
                    int i = IndexOf(n, next);
                    dirns[loop] = i;
                    loop = next;
                }
                loop = start;
                while (dirns.ContainsKey(loop))
                {
                    Pt next = Neighbours(loop)[dirns[loop]];
                    SetCell(loop, In);
                    SetWall(loop, next, false);
                    loop = next;
                }
            }
        }

        [MazeAlgorithm(Algorithms.RecursiveBacktrack)]
        public void RecursiveBacktrack()
        {
            AllWalls();
            Stack<Pt> stack = new Stack<Pt>();
            {
                Pt pt = RandomOf(Select(Locs, Out));
                SetCell(pt, In);
                stack.Push(pt);
            }
            while (stack.Count > 0)
            {
                Pt pt = stack.Peek();
                Pt[] ns = Select(Neighbours(pt), Out);
                if (ns.Length == 0)
                    stack.Pop();
                else
                {
                    Pt next = RandomOf(ns);
                    SetWall(pt, next, false);
                    SetCell(next, In);
                    stack.Push(next);
                }
            }
        }

        [MazeAlgorithm(Algorithms.RecursiveDivision)]
        public void RecursiveDivision()
        {
            AllWalls();
            List<Pt> pts = new List<Pt>();
            pts.AddRange(Locs);
            ChangeCells(Out,In);
            RecursiveDivision(pts);
        }

        void RecursiveDivision(List<Pt> pts)
        {
            if (pts.Count < 2)
                return;
            int y0, y1;
            int axis = ChooseAxis(out y0, out y1, pts);
            int pivot = (y0 + y1) / 2;
            List<Pt> l0 = new List<Pt>();
            List<Pt> l1 = new List<Pt>();
            Func<Pt,int> f=Axes[axis];
            foreach (Pt p in pts)
                if (f(p) < pivot)
                    l0.Add(p);
                else
                    l1.Add(p);
            RecursiveDivision(l0);
            RecursiveDivision(l1);
            while (true)
            {
                Pt p = RandomOfList(l0);
                foreach(Pt n in Neighbours(p))
                    if (l1.Contains(n))
                    {
                        SetWall(p, n, false);
                        return;
                    }
                l0.Remove(p);
            }
        }

        int ChooseAxis(out int y0, out int y1, List<Pt> pts)
        {
            int axes = Axes.Length;
            int bestRange = 0;
            int best = 0;
            int[] ranges = new int[axes];
            y0 = y1 = 0;
            for (int i = 0; i < axes; ++i)
            {
                int x0, x1;
                GetBoundsOf(out x0, out x1, pts, Axes[i]);
                if (i == 0 || x1 - x0 > bestRange)
                {
                    bestRange = x1 - x0;
                    best = i;
                    y0 = x0;
                    y1 = x1;
                }
            }
            if (bestRange == 0)
                return -1;
            return best;
        }

        void GrowingTree(Func<List<Pt>, Pt> selector)
        {
            AllWalls();
            // Choose a cell to be in the maze and add it to list
            List<Pt> list = new List<Pt>();
            {
                Pt pt = RandomOf(Select(Locs, Out));
                SetCell(pt, In);
                list.Add(pt);
            }
            while (list.Count > 0)
            {
                // Join from a cell in the list to a random neighbour, and add that to list
                Pt pt = selector(list);
                Pt[] ns = Select(Neighbours(pt), Out);
                Debug.Assert(ns.Length > 0);
                Pt next = RandomOf(ns);
                SetWall(pt, next, false);
                SetCell(next, In);
                list.Add(next);
                // Remove cells with no available neighbours
                List<Pt> check = new List<Pt>();
                check.AddRange(Neighbours(pt));
                check.AddRange(Neighbours(next));
                foreach(Pt pt2 in check)
                    if (Select(Neighbours(pt2), Out).Length == 0)
                        list.Remove(pt2);
            }
        }

        [MazeAlgorithm(Algorithms.Kruskals)]
        public void Kruskals()
        {
            AllWalls();
            ChangeCells(Out, In);
            // Build a list of walls
            List<Pt> wallsFrom = new List<Pt>();
            List<Pt> wallsTo = new List<Pt>();
            foreach (Pt from in Locs)
                foreach (Pt to in Select(Neighbours(from), In))
                    if (from.CompareTo(to) < 0)
                    {
                        wallsFrom.Add(from);
                        wallsTo.Add(to);
                    }
            // Map points to entries in an array of sets
            Dictionary<Pt, int> dict = new Dictionary<Pt, int>();
            int ind = 0;
            foreach (Pt pt in Locs)
                dict[pt] = ind++;
            ArrayOfSets arr = new ArrayOfSets(ind);
            // For each wall, in random order
            while (wallsFrom.Count > 0)
            {
                int i = Rnd(wallsFrom.Count);
                Pt from = wallsFrom[i];
                Pt to = wallsTo[i];
                wallsFrom.RemoveAt(i);
                wallsTo.RemoveAt(i);
                // Break down wall if places on either side are in different sets,
                //  and join the sets
                int iFrom = dict[from];
                int iTo = dict[to];
                if (arr[iFrom] != arr[iTo])
                {
                    arr.JoinAt(iFrom, iTo);
                    SetWall(from, to, false);
                }
            }
        }

        //[MazeAlgorithm(Algorithms.RecursiveBacktrack)]
        //public void RecursiveBacktrack()
        //{
        //    GrowingTree(LastOfList);
        //}

        [MazeAlgorithm(Algorithms.PrimLike)]
        public void PrimLike()
        {
            GrowingTree(RandomOfList);
        }

        [MazeAlgorithm(Algorithms.LowRiver)]
        public void LowRiver()
        {
            GrowingTree(FromFirstFewOfList);
        }

        [MazeAlgorithm(Algorithms.HighRiver)]
        public void HighRiver()
        {
            GrowingTree(UsuallyLastOfList);
        }

        [MazeAlgorithm(Algorithms.LongWindy)]
        public void LongWindy()
        {
            GrowingTree(FromLastFewOfList);
        }

        public void Unsolve()
        {
            _soln = null;
        }

        public void Solve(Pt start, Pt finish)
        {
            Dictionary<Pt, Pt> search = new Dictionary<Pt, Pt>();
            List<Pt> from = new List<Pt>();
            from.Add(start);
            while (from.Count > 0)
            {
                List<Pt> to = new List<Pt>();
                foreach (Pt p in from)
                {
                    Pt[] ns = Neighbours(p);
                    foreach (Pt n in ns)
                        if (!GetWall(p, n))
                        {
                            if (!search.ContainsKey(n))
                            {
                                to.Add(n);
                                search[n] = p;
                            }
                        }
                }
                from = to;
            }
            Pt loop = finish;
            List<Pt> pts = new List<Pt>();
            if(!PlotsMidway)
                pts.Add(loop);
            while (!loop.Equals(start))
            {
                Pt next;
                if (!search.TryGetValue(loop, out next))
                {
                    // No solution
                    _soln = null;
                    return;
                }
                if(PlotsMidway)
                    pts.Add(Pt.Midway(loop, next));
                else
                    pts.Add(next);
                loop = next;
            }
            if (pts.Count > 1)
                _soln = pts.ToArray();
        }

        protected void Clear()
        {
            _cells.Clear();
            _walls.Clear();
            _soln = null;
        }

        public void GetBounds(out int x0, out int x1, out int y0, out int y1, int sel)
        {
            x0 = x1 = y0 = y1 = 0;
            bool first = true;
            foreach (Pt p in Select(Locs, sel))
            {
                if (first)
                {
                    x0 = x1 = p.X;
                    y0 = y1 = p.Y;
                    first = false;
                }
                else
                {
                    x0 = Math.Min(x0, p.X);
                    x1 = Math.Max(x1, p.X);
                    y0 = Math.Min(y0, p.Y);
                    y1 = Math.Max(y1, p.Y);
                }
            }
        }

        public void GetBoundsOf(out int x0, out int x1, List<Pt> pts, Func<Pt, int> func)
        {
            x0 = x1 = 0;
            bool first = true;
            foreach (Pt p in pts)
                if (first)
                {
                    x0 = x1 = func(p);
                    first = false;
                }
                else
                {
                    int f = func(p);
                    x0 = Math.Min(x0, f);
                    x1 = Math.Max(x1, f);
                }
        }

    }

    public class SquareGrid : Grid
    {

        public override Pt[] Neighbours(Pt pt) { return pt.Neighbours4; }
        public override Pt[] Vertices(Pt pt) { return pt.Vertices4; }
        public override Point DefPoint(Pt p) { return new Point(p.X * 5, p.Y * 5); }
        public override Pt DefPt(Point p) { return new Pt((p.X / 10) * 2, (p.Y / 10) * 2); }
        public override Shapes Shape { get { return Shapes.Square; } }
        public override Func<Pt, int>[] Axes
        {
            get
            {
                return new Func<Pt, int>[]
                {
                    (Pt pt)=>pt.X,
                    (Pt pt)=>pt.Y,
                };
            }
        }
        public override void Init(Pt sz)
        {
            for (int y = 2; y < sz.Y - 1; y += 2)
                for (int x = 2; x < sz.X; x += 2)
                    SetCell(new Pt(x, y), Out);
        }

        /*
        [MazeAlgorithm(Algorithms.BinaryTree)]
        public void BinaryTree()
        {
            AllWalls();
            Pt[] pts = Select(Locs, Out);
            foreach (Pt p in pts)
            {
                Pt[] ans = Neighbours(p);
                List<Pt> ons = new List<Pt>();
                for (int i = 0; i < 2; ++i)
                {
                    if (GetCell(ans[i]) == GetCell(p))
                        ons.Add(ans[i]);
                }
                if (ons.Count > 0)
                    SetWall(p, RandomOfList(ons), false);
            }
            ChangeCells(Out, In);
        }
        */

        [MazeAlgorithm(Algorithms.BinaryTree)]
        public void BinaryTree()
        {
            AllWalls();
            ChangeCells(Out, In);
            int x0, x1, y0, y1;
            GetBounds(out x0, out x1, out y0, out y1, In);
            for (int x = x0+2; x <= x1; x += 2)
            {
                SetWall(new Pt(x, y0), new Pt(x - 2, y0), false);
            }
            for (int y = y0+2; y <= y1; y += 2)
            {
                SetWall(new Pt(x0, y), new Pt(x0, y - 2), false);
                for (int x = x0+2; x <= x1; x += 2)
                {
                    if(Rnd(2)==1)
                        SetWall(new Pt(x, y), new Pt(x - 2, y), false);
                    else
                        SetWall(new Pt(x, y), new Pt(x, y - 2), false);
                }
            }
        }

        [MazeAlgorithm(Algorithms.Sidewinder)]
        public void Sidewinder()
        {
            AllWalls();
            ChangeCells(Out, In);
            int x0, x1, y0, y1;
            GetBounds(out x0, out x1, out y0, out y1, In);
            for (int x = x0 + 2; x <= x1; x += 2)
            {
                SetWall(new Pt(x, y0), new Pt(x - 2, y0), false);
            }
            for (int y = y0 + 2; y <= y1; y += 2)
            {
                int x = x0;
                while (x <= x1)
                {
                    int len = 1;
                    while (x <= x1 - 2 && Rnd(2) == 1)
                    {
                        SetWall(new Pt(x, y), new Pt(x + 2, y), false);
                        ++len;
                        x += 2;
                    }
                    int x2 = x - 2 * Rnd(len);
                    SetWall(new Pt(x2, y), new Pt(x2, y - 2), false);
                    x += 2;
                }
            }
        }

#if false
        [MazeAlgorithm(Algorithms.Ellers)]
        public void Ellers()
        {
            AllWalls();
            ChangeCells(Out, In);
            int x0, x1, y0, y1;
            GetBounds(out x0, out x1, out y0, out y1, In);
            int rowLen = 1 + (x1 - x0) / 2;
            ArrayOfSets cur = new ArrayOfSets(rowLen);
            for (int y = y0; y <= y1 - 2; y += 2)
            {
                // Join some adjacent cells in the row at random
                for (int i = 1; i < rowLen; ++i)
                {
                    int x = x0 + i * 2;
                    if (cur[i - 1] != cur[i] && Rnd(2) != 0)
                    {
                        SetWall(new Pt(x, y), new Pt(x - 2, y), false);
                        cur.JoinAt(i, i - 1);
                    }
                }
                // Make vertical connections
                ArrayOfSets next = new ArrayOfSets(rowLen);
                int[] sets = cur.SetIDs.ToArray();
                for (int i = 0; i < sets.Length; ++i)
                {
                    int[] posns = cur.PositionsOf(sets[i]);
                    int conn = Math.Max(1, posns.Length / 2);
                    while (conn > 0)
                    {
                        int r = RandomOf(posns);
                        int x2 = x0 + 2 * r;
                        if (GetWall(new Pt(x2, y), new Pt(x2, y + 2)))
                        {
                            SetWall(new Pt(x2, y), new Pt(x2, y + 2), false);
                            next[r] = cur[r];
                            --conn;
                        }
                    }
                }
                cur = next;
            }
            // Join sets on final row
            for (int i = 1; i < rowLen; ++i)
            {
                if (cur[i - 1] != cur[i])
                {
                    int x = x0 + 2 * i;
                    SetWall(new Pt(x - 2, y1), new Pt(x, y1), false);
                    cur.JoinAt(i, i - 1);
                }
            }
        }
#else
        [MazeAlgorithm(Algorithms.Ellers)]
        public void Ellers()
        {
            AllWalls();
            ChangeCells(Out, In);
            int x0, x1, y0, y1;
            GetBounds(out x0, out x1, out y0, out y1, In);
            int rowLen = 1 + (x1 - x0) / 2;
            int[] L = new int[rowLen];
            int[] R = new int[rowLen];
            for (int i = 0; i < rowLen; ++i)
                L[i] = R[i] = i;
            for (int y = y0; y <= y1 - 2; y += 2)
            {
                // Join some adjacent cells in the row at random
                for (int i = 1; i < rowLen; ++i)
                {
                    if (R[i - 1] != i && Rnd(2) == 0)
                    {
                        int x = x0 + i * 2;
                        SetWall(new Pt(x, y), new Pt(x - 2, y), false);
                        R[L[i]] = R[i - 1];
                        L[R[i - 1]] = L[i];
                        L[i] = i - 1;
                        R[i - 1] = i;
                    }
                }
                // Make vertical connections
                for (int i = 0; i < rowLen; ++i)
                {
                    if (R[i] != i && Rnd(2) == 0)
                    {
                        R[L[i]] = R[i];
                        L[R[i]] = L[i];
                        L[i] = i;
                        R[i] = i;
                    }
                    else
                    {
                        int x = x0 + i * 2;
                        SetWall(new Pt(x, y), new Pt(x, y + 2), false);
                    }
                }
            }
            // Join sets on final row
            for (int i = 1; i < rowLen; ++i)
            {
                if (R[i - 1] != i)
                {
                    int x = x0 + i * 2;
                    SetWall(new Pt(x, y1), new Pt(x - 2, y1), false);
                    R[L[i]] = R[i - 1];
                    L[R[i - 1]] = L[i];
                    L[i] = i - 1;
                    R[i - 1] = i;
                }
            }
        }
#endif

    }

    public class HexagonalGrid : Grid
    {

        public override Pt[] Neighbours(Pt pt) { return pt.Neighbours6; }
        public override Pt[] Vertices(Pt pt) { return pt.Vertices6; }
        const int gw = 6, gh = 7;
        public override Point DefPoint(Pt p) { return new Point(p.X * gw, p.Y * gh); }
        public override Pt DefPt(Point p)
        {
            int x = p.X / gw;
            int y = p.Y / gh;
            return new Pt((y % 2 == 0) ? (3 + 6 * (x / 6)) : (6 * (x / 6)), y);
        }
        public override Shapes Shape { get { return Shapes.Hexagonal; } }
        public override Func<Pt, int>[] Axes
        {
            get
            {
                return new Func<Pt, int>[]
                {
                    (Pt Pt)=>Pt.X+Pt.Y*3,
                    (Pt Pt)=>Pt.X-Pt.Y*3,
                    (Pt Pt)=>Pt.Y*4,
                };
            }
        }
        public override void Init(Pt sz)
        {
            for (int y = 2; y < sz.Y - 1; y += 2)
            {
                for (int x = 3; x < sz.X; x += 6)
                    SetCell(new Pt(x, y), Out);
                for (int x = 6; x < sz.X; x += 6)
                    SetCell(new Pt(x, y + 1), Out);
            }
        }
    }

    public class TriangularGrid : Grid
    {

        public override Pt[] Neighbours(Pt pt) { return pt.Neighbours3; }
        public override Pt[] Vertices(Pt pt) { return pt.Vertices3; }
        public override Point DefPoint(Pt p) { return new Point(p.X * gw, p.Y * gh); }
        public override Pt DefPt(Point p) { return new Pt(2 * (p.X / (gw * 2)), 2 * (p.Y / (gh * 2))); }
        int gw = 4, gh = 4;
        public override Shapes Shape { get { return Shapes.Triangular; } }
        public override bool PlotsMidway { get { return true; } }
        public override Func<Pt, int>[] Axes
        {
            get
            {
                return new Func<Pt, int>[]
                {
                    (Pt Pt)=>Pt.X+Pt.Y,
                    (Pt Pt)=>Pt.X-Pt.Y,
                    (Pt Pt)=>Pt.X*2,
                };
            }
        }
        public override void Init(Pt sz)
        {
            for (int y = 2; y < sz.Y - 1; y += 2)
            {
                for (int x = 2; x < sz.X - 2; x += 2)
                    SetCell(new Pt(x, y), Out);
            }
        }
    }

}
