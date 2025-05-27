using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_DSNhaCungCap : XtraReport
    {
        public Rpt_DSNhaCungCap()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ThongTinSelect");
            if (dt == null) { return; }
            lblTenDonVi.Text = dt.Rows[0]["TenDonVi"].ToString();
            lblDiaChi.Text = "Đ/c: " + dt.Rows[0]["DiaChi"].ToString();
            lblSoDT.Text = "Số ĐT: " + dt.Rows[0]["SoDT"].ToString();
            lblEmail.Text = "Email: " + dt.Rows[0]["Email"].ToString();
            lblWebsite.Text = "Website: " + dt.Rows[0]["Website"].ToString();
            lblMST.Text = "MST: " + dt.Rows[0]["MST"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();
                      
            colMaNCC.DataBindings.Add("Text", DataSource, "Code");
            colTenNCC.DataBindings.Add("Text", DataSource, "TenNhaCungCap");
            colDienThoai1.DataBindings.Add("Text", DataSource, "Dienthoai1");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colDienThoai2.DataBindings.Add("Text", DataSource, "Dienthoai2");
            colEmail.DataBindings.Add("Text", DataSource, "Email");
            colMST.DataBindings.Add("Text", DataSource, "MaSoThue");
            colSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##0}");
        }
    }
}
