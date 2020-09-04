namespace Garph
{
    partial class Form1
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
            this.trackAmp = new System.Windows.Forms.TrackBar();
            this.trackOffset = new System.Windows.Forms.TrackBar();
            this.trackFreq = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbMean = new System.Windows.Forms.Label();
            this.lbAv = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbAmp = new System.Windows.Forms.Label();
            this.lbFreq = new System.Windows.Forms.Label();
            this.lbOffset = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbPeak = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackAmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFreq)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackAmp
            // 
            this.trackAmp.Location = new System.Drawing.Point(12, 12);
            this.trackAmp.Maximum = 50;
            this.trackAmp.Minimum = 1;
            this.trackAmp.Name = "trackAmp";
            this.trackAmp.Size = new System.Drawing.Size(500, 42);
            this.trackAmp.TabIndex = 1;
            this.trackAmp.Value = 1;
            this.trackAmp.Scroll += new System.EventHandler(this.trackAmp_Scroll);
            // 
            // trackOffset
            // 
            this.trackOffset.Location = new System.Drawing.Point(12, 51);
            this.trackOffset.Maximum = 25;
            this.trackOffset.Minimum = -25;
            this.trackOffset.Name = "trackOffset";
            this.trackOffset.Size = new System.Drawing.Size(500, 42);
            this.trackOffset.TabIndex = 2;
            this.trackOffset.Value = 1;
            this.trackOffset.Scroll += new System.EventHandler(this.trackOffset_Scroll);
            // 
            // trackFreq
            // 
            this.trackFreq.Location = new System.Drawing.Point(12, 90);
            this.trackFreq.Maximum = 100;
            this.trackFreq.Minimum = 1;
            this.trackFreq.Name = "trackFreq";
            this.trackFreq.Size = new System.Drawing.Size(500, 42);
            this.trackFreq.TabIndex = 3;
            this.trackFreq.Value = 2;
            this.trackFreq.Scroll += new System.EventHandler(this.trackFreq_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbMean, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbAv, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbAmp, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbFreq, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbOffset, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbPeak, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 147);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(205, 203);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Average:";
            // 
            // lbMean
            // 
            this.lbMean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMean.AutoSize = true;
            this.lbMean.Location = new System.Drawing.Point(105, 4);
            this.lbMean.Name = "lbMean";
            this.lbMean.Size = new System.Drawing.Size(97, 13);
            this.lbMean.TabIndex = 0;
            this.lbMean.Text = "label1";
            // 
            // lbAv
            // 
            this.lbAv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAv.AutoSize = true;
            this.lbAv.Location = new System.Drawing.Point(105, 25);
            this.lbAv.Name = "lbAv";
            this.lbAv.Size = new System.Drawing.Size(97, 13);
            this.lbAv.TabIndex = 1;
            this.lbAv.Text = "label1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mean:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Amplitude:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Frequency:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "DC Offset:";
            // 
            // lbAmp
            // 
            this.lbAmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAmp.AutoSize = true;
            this.lbAmp.Location = new System.Drawing.Point(105, 45);
            this.lbAmp.Name = "lbAmp";
            this.lbAmp.Size = new System.Drawing.Size(97, 13);
            this.lbAmp.TabIndex = 7;
            this.lbAmp.Text = "label6";
            // 
            // lbFreq
            // 
            this.lbFreq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFreq.AutoSize = true;
            this.lbFreq.Location = new System.Drawing.Point(105, 65);
            this.lbFreq.Name = "lbFreq";
            this.lbFreq.Size = new System.Drawing.Size(97, 13);
            this.lbFreq.TabIndex = 8;
            this.lbFreq.Text = "label7";
            // 
            // lbOffset
            // 
            this.lbOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOffset.AutoSize = true;
            this.lbOffset.Location = new System.Drawing.Point(105, 85);
            this.lbOffset.Name = "lbOffset";
            this.lbOffset.Size = new System.Drawing.Size(97, 13);
            this.lbOffset.TabIndex = 9;
            this.lbOffset.Text = "-";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Peak To Peak:";
            // 
            // lbPeak
            // 
            this.lbPeak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPeak.AutoSize = true;
            this.lbPeak.Location = new System.Drawing.Point(105, 105);
            this.lbPeak.Name = "lbPeak";
            this.lbPeak.Size = new System.Drawing.Size(97, 13);
            this.lbPeak.TabIndex = 11;
            this.lbPeak.Text = "label7";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(518, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(130, 368);
            this.listBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 389);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.trackFreq);
            this.Controls.Add(this.trackOffset);
            this.Controls.Add(this.trackAmp);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackAmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFreq)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackAmp;
        private System.Windows.Forms.TrackBar trackOffset;
        private System.Windows.Forms.TrackBar trackFreq;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbMean;
        private System.Windows.Forms.Label lbAv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbAmp;
        private System.Windows.Forms.Label lbFreq;
        private System.Windows.Forms.Label lbOffset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbPeak;
        private System.Windows.Forms.ListBox listBox1;
    }
}

