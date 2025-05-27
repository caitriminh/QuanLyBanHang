using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_ThongKeBanHang : XtraReport
    {
        public Rpt_ThongKeBanHang()
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
            lbl_NguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();
            //GroupHeader1.GroupFields.Add(new GroupField("TenLoai", XRColumnSortOrder.Ascending));
            //colLoaiSanPham.DataBindings.Add("Text", null, "TenLoai", "Loại sản phẩm: {0}");

            lbl_TenKhachHang.Text = ": " + Form_Template.dtReport.Rows[0]["TenKhachHang"].ToString();
            lbl_DiaChi.Text = ": " + Form_Template.dtReport.Rows[0]["DiaChi"].ToString();
            lbl_MST.Text = ": " + Form_Template.dtReport.Rows[0]["MaSoThue"].ToString();
            xrLabel12.Text = "Từ " + Convert.ToDateTime(Form_Template.tungay).ToString("dd/MM/yyyy") + " đến " + Convert.ToDateTime(Form_Template.denngay).ToString("dd/MM/yyyy");

            colSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##0}");
            colTenKH.DataBindings.Add("Text", DataSource, "TenKhachHang");
            colNgayNhap.DataBindings.Add("Text", DataSource, "NgayLap", "{0:dd/MM/yyyy}");
            colTenSP.DataBindings.Add("Text", DataSource, "TenSanPham");         
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia", "{0:#,##0}");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien", "{0:#,##0}");

            colTongSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colTongSoLuong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTongThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

          
        }
    }
}
