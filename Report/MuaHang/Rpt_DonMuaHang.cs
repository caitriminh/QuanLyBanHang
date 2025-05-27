using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_DonMuaHang : XtraReport
    {
        public Rpt_DonMuaHang()
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
            lbl_MST.Text= "MST: " + dt.Rows[0]["MST"].ToString();
            lbl_nguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();          
            lblMaHD.Text = Form_Template.dtReport.Rows[0]["MaDonHang"].ToString();
            lbl_NgayDatHang.Text =Convert.ToDateTime( Form_Template.dtReport.Rows[0]["NgayDatHang"]).ToString("dd/MM/yyyy");
            lblNhaCungCap.Text = @": " + Form_Template.dtReport.Rows[0]["TenNhaCungCap"].ToString();         
            lblDiaChiNCC.Text = @": " + Form_Template.dtReport.Rows[0]["DiaChi"];
            lbl_DienThoaiNCC.Text = @": " + Form_Template.dtReport.Rows[0]["DienThoai1"];
            lbl_MSTNCC.Text = @": " + Form_Template.dtReport.Rows[0]["MaSoThue"];

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

            lbl_NgayGiaoHang.Text ="- Ngày giao hàng: " + Convert.ToDateTime(Form_Template.dtReport.Rows[0]["NgayDatHang"]).ToString("dd/MM/yyyy");
            lblDiaDiemGiaoHang.Text = "- Địa điểm giao hàng: " + dt.Rows[0]["TenDonVi"].ToString();
            lblTenCty.Text = dt.Rows[0]["TenDonVi"].ToString();
            lbl_TenCtyMua.Text = Form_Template.dtReport.Rows[0]["TenNhaCungCap"].ToString().ToUpper();           
        }
    }
}
