using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_HoaHongNhanVienv2 : XtraReport
    {
        public Rpt_HoaHongNhanVienv2()
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
            //GroupHeader1.GroupFields.Add(new GroupField("TenNhanVien", XRColumnSortOrder.Ascending));
            //col_NhanVienSale.DataBindings.Add("Text", null, "TenNhanVien", "Nhân viên bán hàng: {0}");

            colNgayNhap.DataBindings.Add("Text", DataSource, "NgayNhap", "{0:dd/MM/yyyy}");
            colMaNV.DataBindings.Add("Text", DataSource, "Code");
            colHoTen.DataBindings.Add("Text", DataSource, "TenNhanVien");
            colDinhMuc.DataBindings.Add("Text", DataSource, "DinhMuc", "{0:#,##0}");
            colSoTien.DataBindings.Add("Text", DataSource, "SoTien", "{0:#,##0}");
            colHoaHong.DataBindings.Add("Text", DataSource, "HoaHong", "{0:#,##0}");
            colTyle.DataBindings.Add("Text", DataSource, "Tyle", "{0:#,##0}");
            colPhanTram.DataBindings.Add("Text", DataSource, "PhanTram", "{0:#,##0}");

            colSumDinhMuc.DataBindings.Add("Text", DataSource, "DinhMuc");
            colSumDinhMuc.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colSumHoaHong.DataBindings.Add("Text", DataSource, "HoaHong");
            colSumHoaHong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colSumSoTien.DataBindings.Add("Text", DataSource, "SoTien");
            colSumSoTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
        }
    }
}
