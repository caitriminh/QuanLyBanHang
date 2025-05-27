using DevExpress.XtraReports.UI;
using System;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BaoCaoTienChiecKhau : XtraReport
    {
        public Rpt_BaoCaoTienChiecKhau()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            System.Data.DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ThongTinSelect");
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
            //GroupHeader1.GroupFields.Add(new GroupField("MaHoaDon", XRColumnSortOrder.Ascending));
            //colMaHD.DataBindings.Add("Text", null, "MaHoaDon", "Số HĐ: {0}");
            //colNgayLap.DataBindings.Add("Text", null, "NgayLap", "Ngày HĐ: {0: dd/MM/yyyy}");
            //colKhachHang.DataBindings.Add("Text", null, "TenKH", "KH: {0}");

            colNgayHD.DataBindings.Add("Text", DataSource, "NgayLap", "{0:dd/MM/yyyy}");
            colMaHD.DataBindings.Add("Text", DataSource, "MaHoaDon");
            colTenKH.DataBindings.Add("Text", DataSource, "TenKhachHang");            
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colTenSP.DataBindings.Add("Text", DataSource, "TenSanPham");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");
            colDonGia.DataBindings.Add("Text", DataSource, "GiaTruocThue", "{0:#,##0}");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien", "{0:#,##0}");
            colThanhTienTT.DataBindings.Add("Text", DataSource, "TienTruocThue", "{0:#,##0}");
            colChiecKhau.DataBindings.Add("Text", DataSource, "ChiecKhau","{0:#,##0}");
            colTienChiecKhau.DataBindings.Add("Text", DataSource, "TienChiecKhau", "{0:#,##0}");
            colThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan", "{0:#,##0}");

            colTongSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colTongSoLuong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

         
            colTongThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTongThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongThanhTienTT.DataBindings.Add("Text", DataSource, "TienTruocThue");
            colTongThanhTienTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");


            colTongThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan");
            colTongThanhToan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongTienCK.DataBindings.Add("Text", DataSource, "TienChiecKhau");
            colTongTienCK.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
                       

            try
            {
                object sumObject = Form_Template.dtReport.Compute("SUM(TienChiecKhau)", string.Empty);
                decimal sum = Convert.ToDecimal(sumObject);
                lblGhiChu.Text = "- Tiền chiếc khấu của quý khách là " + Form_Template.dtReport.Rows[0]["ChiecKhau"].ToString() + "%, Số tiển: " + sum.ToString("#,##0") + " VND";
            }
            catch (Exception)            {

                lblGhiChu.Text = "- Tiền chiếc khấu của quý khách là 0 VND";
            }

        }
    }
}
