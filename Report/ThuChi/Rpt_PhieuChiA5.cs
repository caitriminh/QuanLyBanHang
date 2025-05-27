using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_PhieuChiA5 : XtraReport
    {
        public Rpt_PhieuChiA5()
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
            lblMaPhieu.Text = Form_Template.dtReport.Rows[0]["MaPhieu"].ToString();
            lblNgayLap.Text = Convert.ToDateTime(Form_Template.dtReport.Rows[0]["NgayChungTu"]).ToString("dd/MM/yyyy");
            lblKhachHang.Text = @": " + Form_Template.dtReport.Rows[0]["TenNhaCungCap"].ToString();
            lbl_SoTien.Text = Form_Template.dtReport.Rows[0]["DienGiai"].ToString();
            lblDiaChiKH.Text = @": " + Form_Template.dtReport.Rows[0]["DiaChi"];
            lbl_SoTien.Text = @": " + Convert.ToDecimal(Form_Template.dtReport.Rows[0]["SoTien"]).ToString("#,##0");
            lblThanhTienBangChu.Text = Form_Template.dtReport.Rows[0]["ThanhTienBangChu"].ToString();
            lbl_diengiai.Text = @": " + Form_Template.dtReport.Rows[0]["DienGiai"];
            lbl_ChungTuKemTheo.Text = @": ";
        }
    }
}
