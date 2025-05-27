using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_HoaHongNhanVien : XtraReport
    {
        public Rpt_HoaHongNhanVien()
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
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();
            GroupHeader1.GroupFields.Add(new GroupField("TenNhanVien", XRColumnSortOrder.Ascending));
            col_NhanVienSale.DataBindings.Add("Text", null, "TenNhanVien", "Nhân viên bán hàng: {0}");

            col_MaDonHang.DataBindings.Add("Text", DataSource, "MaDonHang");           
            col_TenKhachHang.DataBindings.Add("Text", DataSource, "TenKhachHang");           
            col_DoanhSo.DataBindings.Add("Text", DataSource, "ThanhToan", "{0:#,##0}");

            colTongDoanhSo.DataBindings.Add("Text", DataSource, "ThanhToan");
            colTongDoanhSo.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
        }
    }
}
