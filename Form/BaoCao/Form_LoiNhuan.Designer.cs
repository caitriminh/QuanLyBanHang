
namespace VisualStudio2019.Form
{
    partial class Form_LoiNhuan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_LoiNhuan));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.BtnIn = new DevExpress.XtraBars.BarButtonItem();
            this.BtnExcel = new DevExpress.XtraBars.BarButtonItem();
            this.BtnNap = new DevExpress.XtraBars.BarButtonItem();
            this.BtnDong = new DevExpress.XtraBars.BarButtonItem();
            this.dteThang = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnTim = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.grcTongHopChiPhi = new DevExpress.XtraGrid.GridControl();
            this.grvViewTongHopChiPhi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn81 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThangTruoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colThangNay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoThangTruoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLuyKeNamNay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcTongHopChiPhi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvViewTongHopChiPhi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
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
            this.BtnExcel,
            this.BtnNap,
            this.BtnDong,
            this.dteThang,
            this.btnTim,
            this.BtnIn});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 12;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnIn, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnExcel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnNap, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnDong, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.dteThang, "", true, true, true, 106, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnTim)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // BtnIn
            // 
            this.BtnIn.Caption = "In";
            this.BtnIn.Enabled = false;
            this.BtnIn.Id = 11;
            this.BtnIn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnIn.ImageOptions.SvgImage")));
            this.BtnIn.Name = "BtnIn";
            this.BtnIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnIn_ItemClick_1);
            // 
            // BtnExcel
            // 
            this.BtnExcel.Caption = "Excel";
            this.BtnExcel.Enabled = false;
            this.BtnExcel.Id = 5;
            this.BtnExcel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnExcel.ImageOptions.SvgImage")));
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnExcel_ItemClick);
            // 
            // BtnNap
            // 
            this.BtnNap.Caption = "Nạp";
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
            // dteThang
            // 
            this.dteThang.Caption = "Tháng";
            this.dteThang.Edit = this.repositoryItemDateEdit1;
            this.dteThang.Id = 8;
            this.dteThang.Name = "dteThang";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "MM/yyyy";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "MM/yyyy";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.MaskSettings.Set("mask", "MM/yyyy");
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            // 
            // btnTim
            // 
            this.btnTim.Caption = "&Tìm";
            this.btnTim.Id = 10;
            this.btnTim.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnTim.ImageOptions.SvgImage")));
            this.btnTim.Name = "btnTim";
            this.btnTim.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTim_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1271, 34);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 614);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1271, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 34);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 580);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1271, 34);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 580);
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            // 
            // grcTongHopChiPhi
            // 
            this.grcTongHopChiPhi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcTongHopChiPhi.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grcTongHopChiPhi.Location = new System.Drawing.Point(0, 34);
            this.grcTongHopChiPhi.MainView = this.grvViewTongHopChiPhi;
            this.grcTongHopChiPhi.Margin = new System.Windows.Forms.Padding(4);
            this.grcTongHopChiPhi.Name = "grcTongHopChiPhi";
            this.grcTongHopChiPhi.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit7,
            this.repositoryItemSpinEdit1});
            this.grcTongHopChiPhi.Size = new System.Drawing.Size(1271, 580);
            this.grcTongHopChiPhi.TabIndex = 24;
            this.grcTongHopChiPhi.UseEmbeddedNavigator = true;
            this.grcTongHopChiPhi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvViewTongHopChiPhi});
            // 
            // grvViewTongHopChiPhi
            // 
            this.grvViewTongHopChiPhi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn81,
            this.gridColumn62,
            this.colThangTruoc,
            this.colThangNay,
            this.colSoThangTruoc,
            this.colLuyKeNamNay});
            this.grvViewTongHopChiPhi.DetailHeight = 431;
            this.grvViewTongHopChiPhi.GridControl = this.grcTongHopChiPhi;
            this.grvViewTongHopChiPhi.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LuyKeNamNay", this.colLuyKeNamNay, "{0:#,##0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThangTruoc", this.colThangTruoc, "{0:#,##0}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThangNay", this.colThangNay, "{0:#,##0}")});
            this.grvViewTongHopChiPhi.Name = "grvViewTongHopChiPhi";
            this.grvViewTongHopChiPhi.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvViewTongHopChiPhi.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvViewTongHopChiPhi.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvViewTongHopChiPhi.OptionsMenu.ShowConditionalFormattingItem = true;
            this.grvViewTongHopChiPhi.OptionsView.ColumnAutoWidth = false;
            this.grvViewTongHopChiPhi.OptionsView.ShowAutoFilterRow = true;
            this.grvViewTongHopChiPhi.OptionsView.ShowFooter = true;
            this.grvViewTongHopChiPhi.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn81
            // 
            this.gridColumn81.Caption = "Kỳ";
            this.gridColumn81.FieldName = "Thang";
            this.gridColumn81.MinWidth = 25;
            this.gridColumn81.Name = "gridColumn81";
            this.gridColumn81.OptionsColumn.AllowEdit = false;
            this.gridColumn81.OptionsColumn.FixedWidth = true;
            this.gridColumn81.Visible = true;
            this.gridColumn81.VisibleIndex = 0;
            this.gridColumn81.Width = 120;
            // 
            // gridColumn62
            // 
            this.gridColumn62.Caption = "Loại chi phí";
            this.gridColumn62.FieldName = "LoaiThuChi";
            this.gridColumn62.MinWidth = 25;
            this.gridColumn62.Name = "gridColumn62";
            this.gridColumn62.OptionsColumn.AllowEdit = false;
            this.gridColumn62.Visible = true;
            this.gridColumn62.VisibleIndex = 1;
            this.gridColumn62.Width = 255;
            // 
            // colThangTruoc
            // 
            this.colThangTruoc.AppearanceHeader.Options.UseTextOptions = true;
            this.colThangTruoc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colThangTruoc.Caption = "Tháng trước";
            this.colThangTruoc.ColumnEdit = this.repositoryItemSpinEdit7;
            this.colThangTruoc.FieldName = "ThangTruoc";
            this.colThangTruoc.MinWidth = 27;
            this.colThangTruoc.Name = "colThangTruoc";
            this.colThangTruoc.OptionsColumn.FixedWidth = true;
            this.colThangTruoc.OptionsColumn.ReadOnly = true;
            this.colThangTruoc.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThangTruoc", "{0:#,##0}")});
            this.colThangTruoc.Visible = true;
            this.colThangTruoc.VisibleIndex = 2;
            this.colThangTruoc.Width = 129;
            // 
            // repositoryItemSpinEdit7
            // 
            this.repositoryItemSpinEdit7.AutoHeight = false;
            this.repositoryItemSpinEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit7.MaskSettings.Set("mask", "n0");
            this.repositoryItemSpinEdit7.Name = "repositoryItemSpinEdit7";
            this.repositoryItemSpinEdit7.UseMaskAsDisplayFormat = true;
            // 
            // colThangNay
            // 
            this.colThangNay.AppearanceHeader.Options.UseTextOptions = true;
            this.colThangNay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colThangNay.Caption = "Tháng này";
            this.colThangNay.ColumnEdit = this.repositoryItemSpinEdit7;
            this.colThangNay.FieldName = "ThangNay";
            this.colThangNay.MinWidth = 25;
            this.colThangNay.Name = "colThangNay";
            this.colThangNay.OptionsColumn.AllowEdit = false;
            this.colThangNay.OptionsColumn.FixedWidth = true;
            this.colThangNay.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThangNay", "{0:#,##0}")});
            this.colThangNay.Visible = true;
            this.colThangNay.VisibleIndex = 3;
            this.colThangNay.Width = 129;
            // 
            // colSoThangTruoc
            // 
            this.colSoThangTruoc.AppearanceHeader.Options.UseTextOptions = true;
            this.colSoThangTruoc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSoThangTruoc.Caption = "(+/- So tháng trước)";
            this.colSoThangTruoc.ColumnEdit = this.repositoryItemSpinEdit7;
            this.colSoThangTruoc.DisplayFormat.FormatString = "{0:#.##0;#.##0;-}";
            this.colSoThangTruoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSoThangTruoc.FieldName = "gridColumn2";
            this.colSoThangTruoc.MinWidth = 25;
            this.colSoThangTruoc.Name = "colSoThangTruoc";
            this.colSoThangTruoc.UnboundDataType = typeof(int);
            this.colSoThangTruoc.UnboundExpression = "[ThangNay] - [ThangTruoc]";
            this.colSoThangTruoc.Visible = true;
            this.colSoThangTruoc.VisibleIndex = 4;
            this.colSoThangTruoc.Width = 157;
            // 
            // colLuyKeNamNay
            // 
            this.colLuyKeNamNay.AppearanceHeader.Options.UseTextOptions = true;
            this.colLuyKeNamNay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colLuyKeNamNay.Caption = "Lũy kế năm nay";
            this.colLuyKeNamNay.ColumnEdit = this.repositoryItemSpinEdit7;
            this.colLuyKeNamNay.FieldName = "NamNay";
            this.colLuyKeNamNay.MinWidth = 25;
            this.colLuyKeNamNay.Name = "colLuyKeNamNay";
            this.colLuyKeNamNay.OptionsColumn.AllowEdit = false;
            this.colLuyKeNamNay.OptionsColumn.FixedWidth = true;
            this.colLuyKeNamNay.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NamNay", "{0:#,##0}")});
            this.colLuyKeNamNay.Visible = true;
            this.colLuyKeNamNay.VisibleIndex = 5;
            this.colLuyKeNamNay.Width = 129;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.BeepOnError = true;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.MaskSettings.Set("mask", "P");
            this.repositoryItemSpinEdit1.MaskSettings.Set("valueAfterDelete", null);
            this.repositoryItemSpinEdit1.MaskSettings.Set("hideInsignificantZeros", null);
            this.repositoryItemSpinEdit1.MaskSettings.Set("autoHideDecimalSeparator", false);
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            this.repositoryItemSpinEdit1.UseMaskAsDisplayFormat = true;
            // 
            // Form_LoiNhuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 614);
            this.Controls.Add(this.grcTongHopChiPhi);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form_LoiNhuan";
            this.Text = "Báo Cáo Lợi Nhuận";
            this.Load += new System.EventHandler(this.Form_BaoCaoChiPhi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcTongHopChiPhi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvViewTongHopChiPhi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem BtnExcel;
        private DevExpress.XtraBars.BarButtonItem BtnNap;
        private DevExpress.XtraBars.BarButtonItem BtnDong;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraBars.BarEditItem dteThang;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraBars.BarButtonItem btnTim;
        private DevExpress.XtraGrid.GridControl grcTongHopChiPhi;
        private DevExpress.XtraGrid.Views.Grid.GridView grvViewTongHopChiPhi;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn81;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn colThangTruoc;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit7;
        private DevExpress.XtraGrid.Columns.GridColumn colThangNay;
        private DevExpress.XtraGrid.Columns.GridColumn colSoThangTruoc;
        private DevExpress.XtraGrid.Columns.GridColumn colLuyKeNamNay;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraBars.BarButtonItem BtnIn;
    }
}