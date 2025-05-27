
namespace VisualStudio2019.Form
{
    partial class Form_ThemTT_NgayNghi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ThemTT_NgayNghi));
            this.Layoutkhachhang = new DevExpress.XtraLayout.LayoutControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.dteNgayNghi = new DevExpress.XtraEditors.DateEdit();
            this.cboLoaiNgayNghi = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.Layoutkhachhang)).BeginInit();
            this.Layoutkhachhang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNgayNghi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNgayNghi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiNgayNghi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // Layoutkhachhang
            // 
            this.Layoutkhachhang.Controls.Add(this.cboLoaiNgayNghi);
            this.Layoutkhachhang.Controls.Add(this.btnDong);
            this.Layoutkhachhang.Controls.Add(this.btnLuu);
            this.Layoutkhachhang.Controls.Add(this.dteNgayNghi);
            this.Layoutkhachhang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layoutkhachhang.Location = new System.Drawing.Point(0, 0);
            this.Layoutkhachhang.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Layoutkhachhang.Name = "Layoutkhachhang";
            this.Layoutkhachhang.Root = this.Root;
            this.Layoutkhachhang.Size = new System.Drawing.Size(465, 163);
            this.Layoutkhachhang.TabIndex = 4;
            // 
            // btnDong
            // 
            this.btnDong.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDong.ImageOptions.SvgImage")));
            this.btnDong.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnDong.Location = new System.Drawing.Point(369, 93);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(82, 27);
            this.btnDong.StyleController = this.Layoutkhachhang;
            this.btnDong.TabIndex = 15;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLuu.ImageOptions.SvgImage")));
            this.btnLuu.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnLuu.Location = new System.Drawing.Point(287, 93);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(78, 27);
            this.btnLuu.StyleController = this.Layoutkhachhang;
            this.btnLuu.TabIndex = 14;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.emptySpaceItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(465, 163);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(465, 134);
            this.layoutControlGroup1.Text = "Thông Tin Ngày Nghỉ";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.dteNgayNghi;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(441, 27);
            this.layoutControlItem6.Text = "Ngày nghỉ";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(82, 16);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnLuu;
            this.layoutControlItem1.Location = new System.Drawing.Point(273, 54);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(82, 31);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnDong;
            this.layoutControlItem2.Location = new System.Drawing.Point(355, 54);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(86, 31);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 54);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(273, 31);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 134);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(465, 29);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dteNgayNghi
            // 
            this.dteNgayNghi.EditValue = null;
            this.dteNgayNghi.Location = new System.Drawing.Point(100, 39);
            this.dteNgayNghi.Name = "dteNgayNghi";
            this.dteNgayNghi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteNgayNghi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteNgayNghi.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dteNgayNghi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteNgayNghi.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dteNgayNghi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteNgayNghi.Properties.MaskSettings.Set("mask", "d");
            this.dteNgayNghi.Size = new System.Drawing.Size(351, 23);
            this.dteNgayNghi.StyleController = this.Layoutkhachhang;
            this.dteNgayNghi.TabIndex = 19;
            // 
            // cboLoaiNgayNghi
            // 
            this.cboLoaiNgayNghi.Location = new System.Drawing.Point(100, 66);
            this.cboLoaiNgayNghi.Name = "cboLoaiNgayNghi";
            this.cboLoaiNgayNghi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLoaiNgayNghi.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LoaiNgayNghi", "Loại ngày nghỉ")});
            this.cboLoaiNgayNghi.Properties.NullText = "[Chọn loại ngày nghỉ]";
            this.cboLoaiNgayNghi.Size = new System.Drawing.Size(351, 23);
            this.cboLoaiNgayNghi.StyleController = this.Layoutkhachhang;
            this.cboLoaiNgayNghi.TabIndex = 20;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cboLoaiNgayNghi;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 27);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(441, 27);
            this.layoutControlItem3.Text = "Loại ngày nghỉ";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(82, 16);
            // 
            // Form_ThemTT_NgayNghi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 163);
            this.Controls.Add(this.Layoutkhachhang);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "Form_ThemTT_NgayNghi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập Nhật Thiết Lập Ngày Nghỉ";
            this.Load += new System.EventHandler(this.Form_ThemTT_NgayNghi_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_ThemTT_NgayNghi_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Layoutkhachhang)).EndInit();
            this.Layoutkhachhang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNgayNghi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNgayNghi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiNgayNghi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl Layoutkhachhang;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LookUpEdit cboLoaiNgayNghi;
        private DevExpress.XtraEditors.DateEdit dteNgayNghi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}