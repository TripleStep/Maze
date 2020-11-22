using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maze
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public Grid GetGrid() { return gridControl1.GetGrid(); }

        #region Grid shape

        private void triangularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridControl1.SetTriangularGrid();
            UpdateShapeMenus();
            UpdateAlgorithmMenus();
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridControl1.SetSquareGrid();
            UpdateShapeMenus();
            UpdateAlgorithmMenus();
        }

        private void hexagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridControl1.SetHexagonalGrid();
            UpdateShapeMenus();
            UpdateAlgorithmMenus();
        }

        void UpdateShapeMenus()
        {
            Grid grid = GetGrid();
            triangularToolStripMenuItem.Checked = grid.Shape == Grid.Shapes.Triangular;
            squareToolStripMenuItem.Checked = grid.Shape == Grid.Shapes.Square;
            hexagonalToolStripMenuItem.Checked = grid.Shape == Grid.Shapes.Hexagonal;
        }

        #endregion Grid shape

        #region Algorithms

        List<Action> algorithmItemUpdates = new List<Action>();

        void AddAlgorithmMenuItems()
        {
            AddAlgorithmMenuItem(primsToolStripMenuItem, Grid.Algorithms.Prims);
            AddAlgorithmMenuItem(wilsonsToolStripMenuItem, Grid.Algorithms.Wilsons);
            //AddAlgorithmMenuItem(recursiveFillerToolStripMenuItem, Grid.Algorithms.RecursiveFiller);
            AddAlgorithmMenuItem(recursiveBacktrackToolStripMenuItem, Grid.Algorithms.RecursiveBacktrack);
            AddAlgorithmMenuItem(primlikeToolStripMenuItem, Grid.Algorithms.PrimLike);
            AddAlgorithmMenuItem(lowRiverToolStripMenuItem, Grid.Algorithms.LowRiver);
            AddAlgorithmMenuItem(highRiverToolStripMenuItem, Grid.Algorithms.HighRiver);
            AddAlgorithmMenuItem(longWindyToolStripMenuItem, Grid.Algorithms.LongWindy);
            AddAlgorithmMenuItem(binaryTreeToolStripMenuItem, Grid.Algorithms.BinaryTree);
            AddAlgorithmMenuItem(sidewinderToolStripMenuItem, Grid.Algorithms.Sidewinder);
            AddAlgorithmMenuItem(ellersToolStripMenuItem, Grid.Algorithms.Ellers);
            AddAlgorithmMenuItem(recursiveDivisionToolStripMenuItem, Grid.Algorithms.RecursiveDivision);
            AddAlgorithmMenuItem(kruskalsToolStripMenuItem, Grid.Algorithms.Kruskals);
        }

        void AddAlgorithmMenuItem(ToolStripMenuItem item, Grid.Algorithms alg )
        {
            item.Click += new EventHandler((object sender, EventArgs e) => ClickAlgorithmMenuItem(alg));
            algorithmItemUpdates.Add(() => UpdateAlgorithmItem(item, alg));
        }

        void ClickAlgorithmMenuItem(Grid.Algorithms alg)
        {
            gridControl1.SetAlgorithm(alg);
            UpdateAlgorithmMenus();
        }

        void UpdateAlgorithmMenus()
        {
            foreach (var a in algorithmItemUpdates)
                a();
        }

        void UpdateAlgorithmItem(ToolStripMenuItem item, Grid.Algorithms alg)
        {
            item.Checked = GetGrid().Algorithm == alg;
            item.Enabled = GetGrid().GetMethod(alg) != null;
        }

        #endregion Algorithms

        private void MainForm_Load(object sender, EventArgs e)
        {
            gridControl1.EnsureGridExists();
            AddAlgorithmMenuItems();
            UpdateShapeMenus();
            UpdateAlgorithmMenus();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridControl1.Generate();
        }

    }
}
