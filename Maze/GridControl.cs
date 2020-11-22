using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maze
{
    public partial class GridControl : UserControl
    {

        Color CursorColor = Color.Purple;
        Color StartColor = Color.Red;
        Color FinishColor = Color.Green;
        Color WallColor = Color.Black;
        Color CellColor = Color.LightGray;
        Color SolutionColor = Color.DarkGray;

        Bitmap img;
        Grid grid;
        Pt? cursor, start, finish;

        public GridControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public Grid GetGrid() { return grid; }

        public void EnsureGridExists()
        {
            if (grid != null)
                return;
            Generate(new SquareGrid());
        }

        public void SetTriangularGrid()
        {
            if (grid.Shape != Grid.Shapes.Triangular)
                Generate(new TriangularGrid());
        }

        public void SetSquareGrid()
        {
            if (grid.Shape != Grid.Shapes.Square)
                Generate(new SquareGrid());
        }

        public void SetHexagonalGrid()
        {
            if (grid.Shape != Grid.Shapes.Hexagonal)
                Generate(new HexagonalGrid());
        }

        public void Generate(Grid grid)
        {
            Grid.Algorithms alg = Grid.Algorithms.RecursiveBacktrack;
            if (this.grid != null)
                alg = this.grid.Algorithm;
            this.grid = grid;
            start = finish = null;
            grid.Algorithm = alg;
            Generate();
        }

        public void SetAlgorithm(Grid.Algorithms alg)
        {
            if (grid.Algorithm != alg)
            {
                grid.Algorithm = alg;
                Generate();
            }
        }

        public void Generate()
        {
            ShowWorking();
            Pt sz = new PickContext(grid).Pt(new Point(Width, Height));
            grid.Generate(sz);
            start = finish = null;
            RedrawGrid();
        }

        void ShowWorking()
        {
            using (Graphics g = CreateGraphics())
            {
                string msg = "Generating maze...";
                SizeF sz = g.MeasureString(msg,Font);
                g.DrawString(msg, Font, Brushes.Gray,
                    (Width - sz.Width) * 0.5f,
                    (Height - sz.Height) * 0.5f);
            }
        }

        private void GridControl_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (grid != null)
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    PaintContext pc = new PaintContext(grid, e.Graphics);
                    //grid.Paint(pc);
                    e.Graphics.DrawImage(img, new Point(0, 0));

                    if (cursor.HasValue)
                        grid.FillCell(pc, cursor.Value, CursorColor);

                    if (start.HasValue)
                        grid.FillCell(pc, start.Value, StartColor);

                    if (finish.HasValue)
                        grid.FillCell(pc, finish.Value, FinishColor);
                }
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(ex.ToString(), Font, Brushes.Black, 0, 0);
            }
        }

        private void GridControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (grid != null)
            {
                PickContext pc = new PickContext(grid);
                Pt loc = pc.Pt(e.Location);
                if (grid.GetCell(loc) == Grid.In)
                {
                    cursor = loc;
                    Invalidate();
                }
                else if (cursor.HasValue)
                {
                    cursor = null;
                    Invalidate();
                }
            }
        }

        private void GridControl_MouseClick(object sender, MouseEventArgs e)
        {
            if(grid==null)
                return;

            if (cursor.HasValue)
            {
                if (start.HasValue &&
                    !finish.HasValue)
                {
                    if (start.Equals(cursor))
                    {
                        start = finish = null;
                    }
                    else
                    {
                        finish = cursor;
                        grid.Solve(start.Value, finish.Value);
                    }
                }
                else
                {
                    start = cursor;
                    finish = null;
                    grid.Unsolve();
                }
                RedrawGrid();
            }
        }

        private void GridControl_MouseLeave(object sender, EventArgs e)
        {
            if (cursor.HasValue)
            {
                cursor = null;
                Invalidate();
            }
        }

        private void GridControl_Resize(object sender, EventArgs e)
        {
            RedrawGrid();
        }

        void RedrawGrid()
        {
            if(img!=null)
                img.Dispose();
            img = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                using (Brush back = new SolidBrush(BackColor))
                    g.FillRectangle(back, 0, 0, Width, Height);
                if (grid != null)
                {
                    PaintContext pc = new PaintContext(grid, g);
                    grid.Paint(pc, SolutionColor, WallColor, CellColor);
                }
            }
            Invalidate();
        }

    }
}
