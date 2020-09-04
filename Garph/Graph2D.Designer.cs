namespace Garph
{
    partial class Graph2D
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
            this.window = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // window
            // 
            this.window.BackColor = System.Drawing.Color.Black;
            this.window.Location = new System.Drawing.Point(12, 12);
            this.window.Name = "window";
            this.window.Size = new System.Drawing.Size(320, 100);
            this.window.TabIndex = 1;
            this.window.Paint += new System.Windows.Forms.PaintEventHandler(this.window_Paint);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 125);
            this.Controls.Add(this.window);
            this.Name = "Display";
            this.Text = "Display";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel window;
    }
}