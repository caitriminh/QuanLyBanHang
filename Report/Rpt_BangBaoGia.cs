using DevExpress.PivotGrid.OLAP.Mdx;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BangBaoGia : XtraReport
    {
        public Rpt_BangBaoGia()
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
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();

            colSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##0}");                     
            colTenSP.DataBindings.Add("Text", DataSource, "TenSanPham");
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            XrPictureBox1.DataBindings.Add(new XRBinding("ImageUrl", DataSource, "URLAnh"));
            colGiaBan.DataBindings.Add("Text", DataSource, "GiaBan", "{0:#,##0}");           

           
        }
    }
}
