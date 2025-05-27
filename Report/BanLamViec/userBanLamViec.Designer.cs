namespace QuanLyBanHang.Report.BanLamViec
{
    partial class userBanLamViec
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
            this.components = new System.ComponentModel.Container();
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.widgetView1 = new DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView(this.components);
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Widget.Document(this.components);
            this.document2 = new DevExpress.XtraBars.Docking2010.Views.Widget.Document(this.components);
            this.document3 = new DevExpress.XtraBars.Docking2010.Views.Widget.Document(this.components);
            this.stackGroup1 = new DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup(this.components);
            this.stackGroup2 = new DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widgetView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackGroup2)).BeginInit();
            this.SuspendLayout();
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.MenuManager = this.barManager1;
            this.documentManager1.View = this.widgetView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.widgetView1});
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1070, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 770);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1070, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 770);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1070, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 770);
            // 
            // widgetView1
            // 
            this.widgetView1.Documents.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseDocument[] {
            this.document1,
            this.document2,
            this.document3});
            this.widgetView1.RootContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.widgetView1.StackGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup[] {
            this.stackGroup1,
            this.stackGroup2});
            this.widgetView1.QueryControl += new DevExpress.XtraBars.Docking2010.Views.QueryControlEventHandler(this.widgetView1_QueryControl);
            // 
            // document1
            // 
            this.document1.Caption = "Doanh Thu";
            this.document1.ControlName = "Doanh Thu";
            this.document1.ControlTypeName = "QuanLyBanHang.Report.BanLamViec.user1";
            this.document1.Height = 404;
            this.document1.Width = 508;
            // 
            // document2
            // 
            this.document2.Caption = "Thống Kê Bán Hàng";
            this.document2.ControlName = "Thống Kê Bán Hàng";
            this.document2.ControlTypeName = "QuanLyBanHang.Report.BanLamViec.user3";
            this.document2.Height = 746;
            this.document2.Width = 509;
            // 
            // document3
            // 
            this.document3.Caption = "Tỷ Lệ Bán Hàng";
            this.document3.ControlName = "Tỷ Lệ Bán Hàng";
            this.document3.ControlTypeName = "QuanLyBanHang.Report.BanLamViec.userTyLeBanHang";
            this.document3.Height = 342;
            this.document3.Width = 508;
            // 
            // stackGroup1
            // 
            this.stackGroup1.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Widget.Document[] {
            this.document1,
            this.document3});
            // 
            // stackGroup2
            // 
            this.stackGroup2.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Widget.Document[] {
            this.document2});
            // 
            // userBanLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "userBanLamViec";
            this.Size = new System.Drawing.Size(1070, 770);
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widgetView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackGroup2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView widgetView1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.Document document1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.Document document2;
        private DevExpress.XtraBars.Docking2010.Views.Widget.Document document3;
        private DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup stackGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup stackGroup2;
    }
}
