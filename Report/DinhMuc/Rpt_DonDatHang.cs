using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_DonDatHang : XtraReport
    {
        public Rpt_DonDatHang()
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
            lblWebsite.Text = "Website: " + dt.Rows[0]["Website"].ToString();
            lblEmail.Text = "Email: " + dt.Rows[0]["Email"].ToString();
            lbl_MST.Text= "MST: " + dt.Rows[0]["MST"].ToString();
            lbl_nguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = "Ngày in: " + DateTime.Now.ToString();          
            lblMaHD.Text = Form_Template.dtReport.Rows[0]["MaDonHang"].ToString();
            lbl_NgayDatHang.Text = Convert.ToDateTime( Form_Template.dtReport.Rows[0]["NgayDatHang"]).ToString("dd/MM/yyyy");
            LblNgayGiaoHang.Text = Convert.ToDateTime(Form_Template.dtReport.Rows[0]["NgayGiaoHang"]).ToString("dd/MM/yyyy");
            LblNguoiLienHe.Text = string.IsNullOrWhiteSpace(Form_Template.dtReport.Rows[0]["NguoiLienHe"].ToString()) ? ":" : ": " +
                Form_Template.dtReport.Rows[0]["NguoiLienHe"].ToString() + " - ĐT: " + Form_Template.dtReport.Rows[0]["SoDTNguoiLienHe"].ToString();

            lblKhachHang.Text = @": " + Form_Template.dtReport.Rows[0]["TenKhachHang"].ToString();         
            lblDiaChiKH.Text = @": " + Form_Template.dtReport.Rows[0]["DiaChi"];

            colMaSP.DataBindings.Add("Text", DataSource, "Code");
            colTenSanPham.DataBindings.Add("Text", DataSource, "TenSanPham");
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh"); 
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");          
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia", "{0:#,##0}");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien", "{0:#,##0}");
                       

            colTongThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTongThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongSL.DataBindings.Add("Text", DataSource, "SoLuong");
            colTongSL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
           
            lblDiaDiemGiaoHang.Text = "- Địa điểm giao hàng: " + Form_Template.dtReport.Rows[0]["TenKhachHang"].ToString();
            lblTenCty.Text = dt.Rows[0]["TenDonVi"].ToString();
            lbl_TenCtyMua.Text = Form_Template.dtReport.Rows[0]["TenKhachHang"].ToString().ToUpper();           
        }
    }
}
