using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_PhieuXuatA5 : XtraReport
    {
        public Rpt_PhieuXuatA5()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            if (Form_Template.strLoaiPhieu == "XB")
            {
                lblTieuDe.Text = "PHIẾU XUẤT KHO";
            }
            else if (Form_Template.strLoaiPhieu == "PXDC")
            {
                lblTieuDe.Text = "PHIẾU XUẤT ĐIỀU CHỈNH";
            }
            else if (Form_Template.strLoaiPhieu == "XNL")
            {
                lblTieuDe.Text = "PHIẾU XUẤT NGUYÊN LIỆU";
            }
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ThongTinSelect");
            if (dt == null) { return; }
            lblTenDonVi.Text = dt.Rows[0]["TenDonVi"].ToString();
            lblDiaChi.Text = "Đ/c: " + dt.Rows[0]["DiaChi"].ToString();
            lblSoDT.Text = "Số ĐT: " + dt.Rows[0]["SoDT"].ToString();
            lblEmail.Text = "Email: " + dt.Rows[0]["Email"].ToString();
            lblWebsite.Text = "Website: " + dt.Rows[0]["Website"].ToString();
            lbl_MST.Text= "MST: " + dt.Rows[0]["MST"].ToString();
            lbl_NguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + Convert.ToDateTime(Form_Template.dtReport.Rows[0]["NgayLap"]).Day + "   tháng   " + Convert.ToDateTime(Form_Template.dtReport.Rows[0]["NgayLap"]).Month + "  năm  " + Convert.ToDateTime(Form_Template.dtReport.Rows[0]["NgayLap"]).Year;
            lblMaHD.Text = Form_Template.dtReport.Rows[0]["MaHoaDon"].ToString();
            lblNgayLap.Text = Convert.ToDateTime( Form_Template.dtReport.Rows[0]["NgayLap"]).ToString("dd/MM/yyyy");
            lblKhachHang.Text = @": " + Form_Template.dtReport.Rows[0]["TenKhachHang"].ToString();
            lblDienGiai.Text = Form_Template.dtReport.Rows[0]["DienGiai"].ToString();
            lblDiaChiKH.Text = @": " + Form_Template.dtReport.Rows[0]["DiaChi"];

            colMaSP.DataBindings.Add("Text", DataSource, "Code");
            colTenSanPham.DataBindings.Add("Text", DataSource, "TenSanPham");
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh"); 
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");          
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia", "{0:#,##0}");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien", "{0:#,##0}");

            lblDocSo.DataBindings.Add("Text", DataSource, "ThanhTienBangChu");

            colTongThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            colTongThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongSL.DataBindings.Add("Text", DataSource, "SoLuong");
            colTongSL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
            //colTongTienText.Text = "Tổng số tiền (viết bằng chữ): " + PianoSchool.DtReport.Rows[0]["ThanhToanText"] + " đồng.";
        }
    }
}
