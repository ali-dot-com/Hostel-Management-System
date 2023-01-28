
namespace HMS_1._0
{
    partial class aboutus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(aboutus));
            this.label1 = new System.Windows.Forms.Label();
            this.backbtn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.backbtn)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("Outfit SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(1093, 687);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 24);
            this.label1.TabIndex = 68;
            this.label1.Text = "HMS By 5 Snakes & Co.\r\n";
            // 
            // backbtn
            // 
            this.backbtn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backbtn.BackgroundImage")));
            this.backbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.backbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backbtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.backbtn.Location = new System.Drawing.Point(12, 12);
            this.backbtn.Name = "backbtn";
            this.backbtn.Size = new System.Drawing.Size(132, 52);
            this.backbtn.TabIndex = 97;
            this.backbtn.TabStop = false;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // aboutus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.backbtn);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Outfit SemiBold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "aboutus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HMS";
            ((System.ComponentModel.ISupportInitialize)(this.backbtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox backbtn;
    }
}