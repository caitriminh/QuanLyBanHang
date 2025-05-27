namespace QuanLyBanHang.Report.BanLamViec
{
    partial class frmBanLamViec
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
            this.userBanLamViec1 = new QuanLyBanHang.Report.BanLamViec.userBanLamViec();
            this.SuspendLayout();
            // 
            // userBanLamViec1
            // 
            this.userBanLamViec1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userBanLamViec1.Location = new System.Drawing.Point(0, 0);
            this.userBanLamViec1.Name = "userBanLamViec1";
            this.userBanLamViec1.Size = new System.Drawing.Size(1343, 619);
            this.userBanLamViec1.TabIndex = 0;
            // 
            // frmBanLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 619);
            this.Controls.Add(this.userBanLamViec1);
            this.Name = "frmBanLamViec";
            this.Text = "Bàn Làm Việc";
            this.Load += new System.EventHandler(this.frmBanLamViec_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private userBanLamViec userBanLamViec1;
    }
}