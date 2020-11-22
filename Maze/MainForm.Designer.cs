namespace Maze
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexagonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wilsonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kruskalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ellersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recursiveDivisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sidewinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.growingTreeAlgorithmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recursiveBacktrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.longWindyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highRiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowRiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primlikeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridControl1 = new Maze.GridControl();
            this.recursiveFillerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridToolStripMenuItem,
            this.algorithmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.triangularToolStripMenuItem,
            this.squareToolStripMenuItem,
            this.hexagonalToolStripMenuItem,
            this.toolStripMenuItem1,
            this.generateToolStripMenuItem});
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.gridToolStripMenuItem.Text = "Grid";
            // 
            // triangularToolStripMenuItem
            // 
            this.triangularToolStripMenuItem.Name = "triangularToolStripMenuItem";
            this.triangularToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.triangularToolStripMenuItem.Text = "Triangular";
            this.triangularToolStripMenuItem.Click += new System.EventHandler(this.triangularToolStripMenuItem_Click);
            // 
            // squareToolStripMenuItem
            // 
            this.squareToolStripMenuItem.Name = "squareToolStripMenuItem";
            this.squareToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.squareToolStripMenuItem.Text = "Square";
            this.squareToolStripMenuItem.Click += new System.EventHandler(this.squareToolStripMenuItem_Click);
            // 
            // hexagonalToolStripMenuItem
            // 
            this.hexagonalToolStripMenuItem.Name = "hexagonalToolStripMenuItem";
            this.hexagonalToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.hexagonalToolStripMenuItem.Text = "Hexagonal";
            this.hexagonalToolStripMenuItem.Click += new System.EventHandler(this.hexagonalToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 6);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.generateToolStripMenuItem.Text = "Regenerate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // algorithmToolStripMenuItem
            // 
            this.algorithmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primsToolStripMenuItem,
            this.wilsonsToolStripMenuItem,
            this.kruskalsToolStripMenuItem,
            this.ellersToolStripMenuItem,
            this.recursiveDivisionToolStripMenuItem,
            this.binaryTreeToolStripMenuItem,
            this.sidewinderToolStripMenuItem,
            this.recursiveFillerToolStripMenuItem,
            this.toolStripMenuItem2,
            this.growingTreeAlgorithmsToolStripMenuItem,
            this.recursiveBacktrackToolStripMenuItem,
            this.longWindyToolStripMenuItem,
            this.highRiverToolStripMenuItem,
            this.lowRiverToolStripMenuItem,
            this.primlikeToolStripMenuItem});
            this.algorithmToolStripMenuItem.Name = "algorithmToolStripMenuItem";
            this.algorithmToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.algorithmToolStripMenuItem.Text = "Algorithm";
            // 
            // primsToolStripMenuItem
            // 
            this.primsToolStripMenuItem.Name = "primsToolStripMenuItem";
            this.primsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.primsToolStripMenuItem.Text = "Prim\'s";
            // 
            // wilsonsToolStripMenuItem
            // 
            this.wilsonsToolStripMenuItem.Name = "wilsonsToolStripMenuItem";
            this.wilsonsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.wilsonsToolStripMenuItem.Text = "Wilson\'s";
            // 
            // kruskalsToolStripMenuItem
            // 
            this.kruskalsToolStripMenuItem.Name = "kruskalsToolStripMenuItem";
            this.kruskalsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.kruskalsToolStripMenuItem.Text = "Kruskal\'s";
            // 
            // ellersToolStripMenuItem
            // 
            this.ellersToolStripMenuItem.Name = "ellersToolStripMenuItem";
            this.ellersToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ellersToolStripMenuItem.Text = "Eller\'s";
            // 
            // recursiveDivisionToolStripMenuItem
            // 
            this.recursiveDivisionToolStripMenuItem.Name = "recursiveDivisionToolStripMenuItem";
            this.recursiveDivisionToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.recursiveDivisionToolStripMenuItem.Text = "Recursive Division";
            // 
            // binaryTreeToolStripMenuItem
            // 
            this.binaryTreeToolStripMenuItem.Name = "binaryTreeToolStripMenuItem";
            this.binaryTreeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.binaryTreeToolStripMenuItem.Text = "Binary Tree";
            // 
            // sidewinderToolStripMenuItem
            // 
            this.sidewinderToolStripMenuItem.Name = "sidewinderToolStripMenuItem";
            this.sidewinderToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.sidewinderToolStripMenuItem.Text = "Sidewinder";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(205, 6);
            // 
            // growingTreeAlgorithmsToolStripMenuItem
            // 
            this.growingTreeAlgorithmsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.growingTreeAlgorithmsToolStripMenuItem.Enabled = false;
            this.growingTreeAlgorithmsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.growingTreeAlgorithmsToolStripMenuItem.Name = "growingTreeAlgorithmsToolStripMenuItem";
            this.growingTreeAlgorithmsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.growingTreeAlgorithmsToolStripMenuItem.Text = "Growing Tree algorithms:";
            // 
            // recursiveBacktrackToolStripMenuItem
            // 
            this.recursiveBacktrackToolStripMenuItem.Name = "recursiveBacktrackToolStripMenuItem";
            this.recursiveBacktrackToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.recursiveBacktrackToolStripMenuItem.Text = "Recursive Backtrack";
            // 
            // longWindyToolStripMenuItem
            // 
            this.longWindyToolStripMenuItem.Name = "longWindyToolStripMenuItem";
            this.longWindyToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.longWindyToolStripMenuItem.Text = "Long Windy";
            // 
            // highRiverToolStripMenuItem
            // 
            this.highRiverToolStripMenuItem.Name = "highRiverToolStripMenuItem";
            this.highRiverToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.highRiverToolStripMenuItem.Text = "High River";
            // 
            // lowRiverToolStripMenuItem
            // 
            this.lowRiverToolStripMenuItem.Name = "lowRiverToolStripMenuItem";
            this.lowRiverToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.lowRiverToolStripMenuItem.Text = "Low River";
            // 
            // primlikeToolStripMenuItem
            // 
            this.primlikeToolStripMenuItem.Name = "primlikeToolStripMenuItem";
            this.primlikeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.primlikeToolStripMenuItem.Text = "Prim-like";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(0, 24);
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(284, 238);
            this.gridControl1.TabIndex = 0;
            // 
            // recursiveFillerToolStripMenuItem
            // 
            this.recursiveFillerToolStripMenuItem.Name = "recursiveFillerToolStripMenuItem";
            this.recursiveFillerToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.recursiveFillerToolStripMenuItem.Text = "Recursive Filler";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Mazes";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GridControl gridControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexagonalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wilsonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recursiveBacktrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primlikeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem growingTreeAlgorithmsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowRiverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highRiverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem longWindyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sidewinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ellersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recursiveDivisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kruskalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recursiveFillerToolStripMenuItem;

    }
}

