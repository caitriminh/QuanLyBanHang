using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Linq;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BaoCaoTienChiecKhauA5 : XtraReport
    {
        public Rpt_BaoCaoTienChiecKhauA5()
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
            lblWebsite.Text = "Website: " + dt.Rows[0]["Website"].ToString() + " - Email: " + dt.Rows[0]["Email"].ToString();
            lbl_MST.Text = "MST: " + dt.Rows[0]["MST"].ToString();
            lbl_NguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text ="Ngày in:" + DateTime.Now.ToString();
            DateTime ngayLonNhat = Form_Template.dtReport.AsEnumerable().Max(row => row.Field<DateTime>("NgayLap"));
            DateTime ngayNhoNhat = Form_Template.dtReport.AsEnumerable().Min(row => row.Field<DateTime>("NgayLap"));

            lblNgayThang.Text = "Ngày   " + ngayLonNhat.Day.ToString() + "   tháng   " + ngayLonNhat.Month.ToString() + "  năm  " + ngayLonNhat.Year.ToString();
            LblFromDateToDate.Text = "Từ ngày: " + ngayNhoNhat.ToString("dd/MM/yyyy") + " đến ngày: " + ngayLonNhat.ToString("dd/MM/yyyy");
            //GroupHeader1.GroupFields.Add(new GroupField("MaHoaDon", XRColumnSortOrder.Ascending));
            //colMaHD.DataBindings.Add("Text", null, "MaHoaDon", "Số HĐ: {0}");
            //colNgayLap.DataBindings.Add("Text", null, "NgayLap", "Ngày HĐ: {0: dd/MM/yyyy}");
            //colKhachHang.DataBindings.Add("Text", null, "TenKH", "KH: {0}");


            lbl_TenKhachHang.Text = ": " + Form_Template.dtReport.Rows[0]["TenKhachHang"];
            lbl_DCKhachHang.Text = ": " + Form_Template.dtReport.Rows[0]["DiaChi"];

            colNgayHD.DataBindings.Add("Text", DataSource, "NgayLap", "{0:dd/MM/yyyy}");
            colMaHD.DataBindings.Add("Text", DataSource, "MaHoaDon");
             
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colTenSP.DataBindings.Add("Text", DataSource, "TenSanPham");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");
            colDonGia.DataBindings.Add("Text", DataSource, "GiaTruocThue", "{0:#,##0}");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien", "{0:#,##0}");
            colThanhTienTT.DataBindings.Add("Text", DataSource, "TienTruocThue", "{0:#,##0}");
            colChiecKhau.DataBindings.Add("Text", DataSource, "ChiecKhau", "{0:#,##0}");
            colTienChiecKhau.DataBindings.Add("Text", DataSource, "TienChiecKhau", "{0:#,##0}");
            colThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan", "{0:#,##0}");

            colTongSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colTongSoLuong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");


            colTongThanhTienST.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTongThanhTienST.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

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
