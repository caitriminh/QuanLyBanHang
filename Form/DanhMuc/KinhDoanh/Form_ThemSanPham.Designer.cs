
namespace VisualStudio2019.Form
{
    partial class Form_ThemSanPham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ThemSanPham));
            this.Layoutkhachhang = new DevExpress.XtraLayout.LayoutControl();
            this.chkStatus = new DevExpress.XtraEditors.CheckEdit();
            this.cboDVT = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtQuyCach = new DevExpress.XtraEditors.TextEdit();
            this.cboLoaiSanPham = new DevExpress.XtraEditors.LookUpEdit();
            this.txtGhiChu = new DevExpress.XtraEditors.TextEdit();
            this.txtTenSP = new DevExpress.XtraEditors.TextEdit();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaSP = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.cboLoaiHang = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.Layoutkhachhang)).BeginInit();
            this.Layoutkhachhang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDVT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuyCach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiSanPham.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenSP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // Layoutkhachhang
            // 
            this.Layoutkhachhang.Controls.Add(this.cboLoaiHang);
            this.Layoutkhachhang.Controls.Add(this.chkStatus);
            this.Layoutkhachhang.Controls.Add(this.cboDVT);
            this.Layoutkhachhang.Controls.Add(this.txtQuyCach);
            this.Layoutkhachhang.Controls.Add(this.cboLoaiSanPham);
            this.Layoutkhachhang.Controls.Add(this.txtGhiChu);
            this.Layoutkhachhang.Controls.Add(this.txtTenSP);
            this.Layoutkhachhang.Controls.Add(this.btnDong);
            this.Layoutkhachhang.Controls.Add(this.btnLuu);
            this.Layoutkhachhang.Controls.Add(this.txtMaSP);
            this.Layoutkhachhang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layoutkhachhang.Location = new System.Drawing.Point(0, 0);
            this.Layoutkhachhang.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Layoutkhachhang.Name = "Layoutkhachhang";
            this.Layoutkhachhang.Root = this.Root;
            this.Layoutkhachhang.Size = new System.Drawing.Size(648, 317);
            this.Layoutkhachhang.TabIndex = 4;
            // 
            // chkStatus
            // 
            this.chkStatus.Location = new System.Drawing.Point(14, 228);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Properties.Caption = "Tình trạng";
            this.chkStatus.Size = new System.Drawing.Size(620, 20);
            this.chkStatus.StyleController = this.Layoutkhachhang;
            this.chkStatus.TabIndex = 24;
            // 
            // cboDVT
            // 
            this.cboDVT.Location = new System.Drawing.Point(101, 93);
            this.cboDVT.Name = "cboDVT";
            this.cboDVT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDVT.Properties.NullText = "[Chọn đơn vị tính]";
            this.cboDVT.Properties.PopupView = this.gridLookUpEdit1View;
            this.cboDVT.Size = new System.Drawing.Size(533, 23);
            this.cboDVT.StyleController = this.Layoutkhachhang;
            this.cboDVT.TabIndex = 23;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Đơn vị tính";
            this.gridColumn1.FieldName = "DonViTinh";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // txtQuyCach
            // 
            this.txtQuyCach.Location = new System.Drawing.Point(101, 120);
            this.txtQuyCach.Name = "txtQuyCach";
            this.txtQuyCach.Size = new System.Drawing.Size(533, 23);
            this.txtQuyCach.StyleController = this.Layoutkhachhang;
            this.txtQuyCach.TabIndex = 22;
            // 
            // cboLoaiSanPham
            // 
            this.cboLoaiSanPham.Location = new System.Drawing.Point(101, 147);
            this.cboLoaiSanPham.Name = "cboLoaiSanPham";
            this.cboLoaiSanPham.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLoaiSanPham.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLoai", "Loại sản phẩm")});
            this.cboLoaiSanPham.Properties.NullText = "Chọn loại sản phẩm";
            this.cboLoaiSanPham.Size = new System.Drawing.Size(533, 23);
            this.cboLoaiSanPham.StyleController = this.Layoutkhachhang;
            this.cboLoaiSanPham.TabIndex = 21;
            this.cboLoaiSanPham.EditValueChanged += new System.EventHandler(this.cboLoaiSanPham_EditValueChanged);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(101, 201);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(533, 23);
            this.txtGhiChu.StyleController = this.Layoutkhachhang;
            this.txtGhiChu.TabIndex = 20;
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(101, 66);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(533, 23);
            this.txtTenSP.StyleController = this.Layoutkhachhang;
            this.txtTenSP.TabIndex = 19;
            // 
            // btnDong
            // 
            this.btnDong.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDong.ImageOptions.SvgImage")));
            this.btnDong.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnDong.Location = new System.Drawing.Point(551, 252);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(83, 27);
            this.btnDong.StyleController = this.Layoutkhachhang;
            this.btnDong.TabIndex = 15;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLuu.ImageOptions.SvgImage")));
            this.btnLuu.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnLuu.Location = new System.Drawing.Point(474, 252);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(73, 27);
            this.btnLuu.StyleController = this.Layoutkhachhang;
            this.btnLuu.TabIndex = 14;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(101, 39);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Properties.ReadOnly = true;
            this.txtMaSP.Size = new System.Drawing.Size(533, 23);
            this.txtMaSP.StyleController = this.Layoutkhachhang;
            this.txtMaSP.TabIndex = 18;
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
            this.Root.Size = new System.Drawing.Size(648, 317);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem3,
            this.layoutControlItem8,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem9,
            this.layoutControlItem4,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(648, 293);
            this.layoutControlGroup1.Text = "Thông Tin Sản Phẩm";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtMaSP;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(624, 27);
            this.layoutControlItem5.Text = "Mã sản phẩm";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(83, 16);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtTenSP;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 27);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(624, 27);
            this.layoutControlItem6.Text = "Tên sản phẩm";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(83, 16);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtGhiChu;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 162);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(624, 27);
            this.layoutControlItem7.Text = "Ghi chú";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(83, 16);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cboLoaiSanPham;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(624, 27);
            this.layoutControlItem3.Text = "Loại sản phẩm";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(83, 16);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtQuyCach;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 81);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(624, 27);
            this.layoutControlItem8.Text = "Quy cách";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(83, 16);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnLuu;
            this.layoutControlItem1.Location = new System.Drawing.Point(460, 213);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(77, 31);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnDong;
            this.layoutControlItem2.Location = new System.Drawing.Point(537, 213);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(87, 31);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 213);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(460, 31);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.cboDVT;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(624, 27);
            this.layoutControlItem9.Text = "Đơn vị tính";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(83, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkStatus;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 189);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(624, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 293);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(648, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // cboLoaiHang
            // 
            this.cboLoaiHang.Location = new System.Drawing.Point(101, 174);
            this.cboLoaiHang.Name = "cboLoaiHang";
            this.cboLoaiHang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLoaiHang.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LoaiHang", "Loại hàng")});
            this.cboLoaiHang.Properties.NullText = "Chọn loại hàng";
            this.cboLoaiHang.Size = new System.Drawing.Size(533, 23);
            this.cboLoaiHang.StyleController = this.Layoutkhachhang;
            this.cboLoaiHang.TabIndex = 25;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.cboLoaiHang;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 135);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(624, 27);
            this.layoutControlItem10.Text = "Loại hàng";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(83, 16);
            // 
            // Form_ThemSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 317);
            this.Controls.Add(this.Layoutkhachhang);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "Form_ThemSanPham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập Nhật Sản Phẩm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ThemSanPham_FormClosing);
            this.Load += new System.EventHandler(this.Form_ThemSanPham_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_ThemSanPham_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Layoutkhachhang)).EndInit();
            this.Layoutkhachhang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDVT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuyCach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiSanPham.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenSP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
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
        private DevExpress.XtraEditors.LookUpEdit cboLoaiSanPham;
        private DevExpress.XtraEditors.TextEdit txtGhiChu;
        private DevExpress.XtraEditors.TextEdit txtTenSP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit txtMaSP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit txtQuyCach;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.GridLookUpEdit cboDVT;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.CheckEdit chkStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LookUpEdit cboLoaiHang;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
    }
}