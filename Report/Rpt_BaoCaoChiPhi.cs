using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BaoCaoChiPhi : XtraReport
    {
        public Rpt_BaoCaoChiPhi()
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
            //GroupHeader1.GroupFields.Add(new GroupField("TenLoai", XRColumnSortOrder.Ascending));
            //colLoaiSanPham.DataBindings.Add("Text", null, "TenLoai", "Loại sản phẩm: {0}");
           
            colKy.DataBindings.Add("Text", DataSource, "Thang");           
            colLoaiChiPhi.DataBindings.Add("Text", DataSource, "LoaiThuChi");
            colThangNay.DataBindings.Add("Text", DataSource, "ThangNay", "{0:#,##0}");         
            colThangTruoc.DataBindings.Add("Text", DataSource, "ThangTruoc", "{0:#,##0}");
            colNamNay.DataBindings.Add("Text", DataSource, "NamNay", "{0:#,##0}");

            colTongThangTruoc.DataBindings.Add("Text", DataSource, "ThangTruoc");
            colTongThangTruoc.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongThangNay.DataBindings.Add("Text", DataSource, "ThangNay");
            colTongThangNay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongNamNay.DataBindings.Add("Text", DataSource, "NamNay");
            colTongNamNay.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
        }
    }
}
