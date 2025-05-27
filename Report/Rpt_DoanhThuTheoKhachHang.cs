using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_DoanhThuTheoKhachHang : XtraReport
    {
        public Rpt_DoanhThuTheoKhachHang()
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
            lbl_MST.Text = "MST: " + dt.Rows[0]["MST"].ToString();
            lbl_NguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();
                     
            colMaKH.DataBindings.Add("Text", DataSource, "MaKhachHang");           
            colTenKH.DataBindings.Add("Text", DataSource, "TenKhachHang");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien", "{0:#,##0}");         
            colTienChiecKhau.DataBindings.Add("Text", DataSource, "TienChiecKhau", "{0:#,##0}");
            colThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan", "{0:#,##0}");
            colTongSo.DataBindings.Add("Text", DataSource, "TongSo", "{0:#,##0}");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");

            colTongChiecKhau.DataBindings.Add("Text", DataSource, "TienChiecKhau");
            colTongChiecKhau.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTongThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan");
            colTongThanhToan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongSoHoaDon.DataBindings.Add("Text", DataSource, "TongSo");
            colTongSoHoaDon.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colTongSoLuong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
        }
    }
}
