﻿namespace Maze
{
    partial class GridControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GridControl";
            this.Size = new System.Drawing.Size(225, 231);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GridControl_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridControl_MouseClick);
            this.MouseLeave += new System.EventHandler(this.GridControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GridControl_MouseMove);
            this.Resize += new System.EventHandler(this.GridControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
