
namespace VisualStudio2019.Form
{
    partial class Form_Nhacungcap
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Nhacungcap));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.BtnThem = new DevExpress.XtraBars.BarButtonItem();
            this.BtnSua = new DevExpress.XtraBars.BarButtonItem();
            this.BtnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.BtnIn = new DevExpress.XtraBars.BarButtonItem();
            this.BtnExcel = new DevExpress.XtraBars.BarButtonItem();
            this.BtnNap = new DevExpress.XtraBars.BarButtonItem();
            this.BtnDong = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.grcNCC = new DevExpress.XtraGrid.GridControl();
            this.gvNCC = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTinhTrang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblMaNCC = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcNCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNCC)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BtnThem,
            this.BtnSua,
            this.BtnXoa,
            this.BtnIn,
            this.BtnExcel,
            this.BtnNap,
            this.BtnDong});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 9;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnThem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnSua, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnXoa, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnIn, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnExcel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnDong, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // BtnThem
            // 
            this.BtnThem.Caption = "Thêm";
            this.BtnThem.Enabled = false;
            this.BtnThem.Id = 0;
            this.BtnThem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnThem.ImageOptions.SvgImage")));
            this.BtnThem.Name = "BtnThem";
            this.BtnThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnThem_ItemClick);
            // 
            // BtnSua
            // 
            this.BtnSua.Caption = "Sửa";
            this.BtnSua.Enabled = false;
            this.BtnSua.Id = 2;
            this.BtnSua.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSua.ImageOptions.SvgImage")));
            this.BtnSua.Name = "BtnSua";
            this.BtnSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSua_ItemClick);
            // 
            // BtnXoa
            // 
            this.BtnXoa.Caption = "Xóa";
            this.BtnXoa.Enabled = false;
            this.BtnXoa.Id = 3;
            this.BtnXoa.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnXoa.ImageOptions.SvgImage")));
            this.BtnXoa.Name = "BtnXoa";
            this.BtnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnXoa_ItemClick);
            // 
            // BtnIn
            // 
            this.BtnIn.Caption = "In";
            this.BtnIn.Enabled = false;
            this.BtnIn.Id = 4;
            this.BtnIn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnIn.ImageOptions.SvgImage")));
            this.BtnIn.Name = "BtnIn";
            this.BtnIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnIn_ItemClick);
            // 
            // BtnExcel
            // 
            this.BtnExcel.Caption = "Xuất Excel";
            this.BtnExcel.Enabled = false;
            this.BtnExcel.Id = 5;
            this.BtnExcel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExcel.ImageOptions.SvgImage")));
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnExcel_ItemClick);
            // 
            // BtnNap
            // 
            this.BtnNap.Caption = "Nạp Lại";
            this.BtnNap.Id = 6;
            this.BtnNap.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnNap.ImageOptions.SvgImage")));
            this.BtnNap.Name = "BtnNap";
            this.BtnNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnNap_ItemClick);
            // 
            // BtnDong
            // 
            this.BtnDong.Caption = "Đóng";
            this.BtnDong.Id = 7;
            this.BtnDong.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDong.ImageOptions.SvgImage")));
            this.BtnDong.Name = "BtnDong";
            this.BtnDong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnDong_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1153, 34);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 652);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1153, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 34);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 618);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1153, 34);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 618);
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            // 
            // grcNCC
            // 
            this.grcNCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcNCC.Location = new System.Drawing.Point(0, 34);
            this.grcNCC.MainView = this.gvNCC;
            this.grcNCC.MenuManager = this.barManager1;
            this.grcNCC.Name = "grcNCC";
            this.grcNCC.Size = new System.Drawing.Size(1153, 618);
            this.grcNCC.TabIndex = 10;
            this.grcNCC.UseEmbeddedNavigator = true;
            this.grcNCC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNCC});
            this.grcNCC.DoubleClick += new System.EventHandler(this.grcNCC_DoubleClick);
            // 
            // gvNCC
            // 
            this.gvNCC.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn9,
            this.gridColumn10,
            this.colTinhTrang,
            this.gridColumn8,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13});
            gridFormatRule3.Column = this.colTinhTrang;
            gridFormatRule3.Name = "Format0";
            formatConditionRuleValue3.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.Green;
            formatConditionRuleValue3.Appearance.Options.UseFont = true;
            formatConditionRuleValue3.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue3.Expression = "[TinhTrang] = \'Khả dụng\'";
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.colTinhTrang;
            gridFormatRule4.Name = "Format1";
            formatConditionRuleValue4.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            formatConditionRuleValue4.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue4.Appearance.Options.UseFont = true;
            formatConditionRuleValue4.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue4.Expression = "[TinhTrang] <> \'Khả dụng\'";
            gridFormatRule4.Rule = formatConditionRuleValue4;
            this.gvNCC.FormatRules.Add(gridFormatRule3);
            this.gvNCC.FormatRules.Add(gridFormatRule4);
            this.gvNCC.GridControl = this.grcNCC;
            this.gvNCC.Name = "gvNCC";
            this.gvNCC.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvNCC.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gvNCC.OptionsSelection.MultiSelect = true;
            this.gvNCC.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvNCC.OptionsView.ColumnAutoWidth = false;
            this.gvNCC.OptionsView.ShowAutoFilterRow = true;
            this.gvNCC.OptionsView.ShowGroupedColumns = true;
            this.gvNCC.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã NCC";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.MinWidth = 25;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 94;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Nhà cung cấp";
            this.gridColumn2.FieldName = "TenNhaCungCap";
            this.gridColumn2.MinWidth = 25;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 380;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Địa chỉ";
            this.gridColumn4.FieldName = "DiaChi";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 323;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Số ĐT 1";
            this.gridColumn5.FieldName = "Dienthoai1";
            this.gridColumn5.MinWidth = 25;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 155;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Số ĐT 2";
            this.gridColumn6.FieldName = "Dienthoai2";
            this.gridColumn6.MinWidth = 25;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 137;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Email";
            this.gridColumn7.FieldName = "Email";
            this.gridColumn7.MinWidth = 25;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 179;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Ghi chú";
            this.gridColumn9.FieldName = "Ghichu";
            this.gridColumn9.MinWidth = 25;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.FixedWidth = true;
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 250;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Mã số thuế";
            this.gridColumn10.FieldName = "MaSoThue";
            this.gridColumn10.MinWidth = 25;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 8;
            this.gridColumn10.Width = 112;
            // 
            // colTinhTrang
            // 
            this.colTinhTrang.Caption = "Tình trạng";
            this.colTinhTrang.FieldName = "TinhTrang";
            this.colTinhTrang.MinWidth = 25;
            this.colTinhTrang.Name = "colTinhTrang";
            this.colTinhTrang.OptionsColumn.FixedWidth = true;
            this.colTinhTrang.OptionsColumn.ReadOnly = true;
            this.colTinhTrang.Visible = true;
            this.colTinhTrang.VisibleIndex = 1;
            this.colTinhTrang.Width = 113;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Ngày tạo";
            this.gridColumn8.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm:ss}";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn8.FieldName = "NgayTao";
            this.gridColumn8.MinWidth = 25;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 10;
            this.gridColumn8.Width = 160;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Người tạo";
            this.gridColumn11.FieldName = "NguoiTao";
            this.gridColumn11.MinWidth = 25;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.FixedWidth = true;
            this.gridColumn11.OptionsColumn.ReadOnly = true;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 11;
            this.gridColumn11.Width = 130;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Ngày sửa";
            this.gridColumn12.DisplayFormat.FormatString = "{dd/MM/yyyy HH:mm:ss}";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn12.FieldName = "NgaySua";
            this.gridColumn12.MinWidth = 25;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.FixedWidth = true;
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 12;
            this.gridColumn12.Width = 160;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Người sửa";
            this.gridColumn13.FieldName = "NguoiSua";
            this.gridColumn13.MinWidth = 25;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.FixedWidth = true;
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 13;
            this.gridColumn13.Width = 130;
            // 
            // lblMaNCC
            // 
            this.lblMaNCC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMaNCC.Location = new System.Drawing.Point(528, 252);
            this.lblMaNCC.Name = "lblMaNCC";
            this.lblMaNCC.Size = new System.Drawing.Size(0, 16);
            this.lblMaNCC.TabIndex = 11;
            this.lblMaNCC.Text = "labelControl1";
            // 
            // Form_Nhacungcap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 652);
            this.Controls.Add(this.lblMaNCC);
            this.Controls.Add(this.grcNCC);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form_Nhacungcap";
            this.Text = "Danh Mục Nhà Cung Cấp";
            this.Load += new System.EventHandler(this.Form_Nhacungcap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcNCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNCC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem BtnThem;
        private DevExpress.XtraBars.BarButtonItem BtnSua;
        private DevExpress.XtraBars.BarButtonItem BtnXoa;
        private DevExpress.XtraBars.BarButtonItem BtnIn;
        private DevExpress.XtraBars.BarButtonItem BtnNap;
        private DevExpress.XtraBars.BarButtonItem BtnDong;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraGrid.GridControl grcNCC;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNCC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.LabelControl lblMaNCC;
        private DevExpress.XtraGrid.Columns.GridColumn colTinhTrang;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraBars.BarButtonItem BtnExcel;
    }
}