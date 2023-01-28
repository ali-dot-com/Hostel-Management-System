
namespace HMS_1._0
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.usertxt = new System.Windows.Forms.TextBox();
            this.passtxt = new System.Windows.Forms.TextBox();
            this.showpasschk = new System.Windows.Forms.CheckBox();
            this.loginpic = new System.Windows.Forms.PictureBox();
            this.close = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginpic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1300, 720);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // usertxt
            // 
            this.usertxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.usertxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usertxt.Location = new System.Drawing.Point(250, 280);
            this.usertxt.MaxLength = 30;
            this.usertxt.Name = "usertxt";
            this.usertxt.Size = new System.Drawing.Size(290, 24);
            this.usertxt.TabIndex = 1;
            this.usertxt.TextChanged += new System.EventHandler(this.usertxt_TextChanged);
            // 
            // passtxt
            // 
            this.passtxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.passtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passtxt.Location = new System.Drawing.Point(250, 330);
            this.passtxt.MaxLength = 30;
            this.passtxt.Name = "passtxt";
            this.passtxt.PasswordChar = '*';
            this.passtxt.Size = new System.Drawing.Size(290, 24);
            this.passtxt.TabIndex = 2;
            // 
            // showpasschk
            // 
            this.showpasschk.AutoSize = true;
            this.showpasschk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showpasschk.Location = new System.Drawing.Point(428, 370);
            this.showpasschk.Name = "showpasschk";
            this.showpasschk.Size = new System.Drawing.Size(127, 19);
            this.showpasschk.TabIndex = 4;
            this.showpasschk.Text = "Show Password";
            this.showpasschk.UseVisualStyleBackColor = true;
            this.showpasschk.CheckedChanged += new System.EventHandler(this.showpasschk_CheckedChanged);
            // 
            // loginpic
            // 
            this.loginpic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.loginpic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loginpic.BackgroundImage")));
            this.loginpic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loginpic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginpic.Location = new System.Drawing.Point(260, 439);
            this.loginpic.Margin = new System.Windows.Forms.Padding(0);
            this.loginpic.Name = "loginpic";
            this.loginpic.Size = new System.Drawing.Size(235, 86);
            this.loginpic.TabIndex = 5;
            this.loginpic.TabStop = false;
            this.loginpic.Click += new System.EventHandler(this.loginpic_Click);
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(189)))), ((int)(((byte)(158)))));
            this.close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("close.BackgroundImage")));
            this.close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Location = new System.Drawing.Point(1255, 5);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(20, 20);
            this.close.TabIndex = 62;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 681);
            this.Controls.Add(this.close);
            this.Controls.Add(this.loginpic);
            this.Controls.Add(this.showpasschk);
            this.Controls.Add(this.passtxt);
            this.Controls.Add(this.usertxt);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HMS";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginpic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox usertxt;
        private System.Windows.Forms.TextBox passtxt;
        private System.Windows.Forms.CheckBox showpasschk;
        private System.Windows.Forms.PictureBox loginpic;
        private System.Windows.Forms.PictureBox close;
    }
}

