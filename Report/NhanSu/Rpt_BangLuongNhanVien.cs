using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BangLuongNhanVien : XtraReport
    {
        public Rpt_BangLuongNhanVien()
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
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();

            lbl_KyLuong.Text = "Kỳ Lương Tháng " + Convert.ToDateTime(Form_Template.dtReport.Rows[0]["KyLuong"]).ToString("MM/yyyy");

            GroupHeader1.GroupFields.Add(new GroupField("PhongBan", XRColumnSortOrder.Ascending));
            lbl_PhongBan.DataBindings.Add("Text", null, "PhongBan", "Phòng ban: {0}");

            colMaNV.DataBindings.Add("Text", DataSource, "Code");
            colHoTen.DataBindings.Add("Text", DataSource, "TenNhanVien");
            colChucVu.DataBindings.Add("Text", DataSource, "ChucVu");

            colLuongPhepNam.DataBindings.Add("Text", DataSource, "LuongPhepNam", "{0:#,##0}");
            colLuongTangCa.DataBindings.Add("Text", DataSource, "LuongTangCaFinal", "{0:#,##0}");
            colPhuCap.DataBindings.Add("Text", DataSource, "PhuCap", "{0:#,##0}");
            colLuongChinh.DataBindings.Add("Text", DataSource, "LuongThucTe", "{0:#,##0}");
            colLuongLeTet.DataBindings.Add("Text", DataSource, "LuongLeTet", "{0:#,##0}");
            colTongLuong.DataBindings.Add("Text", DataSource, "TongLuong", "{0:#,##0}");

            colBHXH.DataBindings.Add("Text", DataSource, "Tien_BHXH", "{0:#,##0}");          
            colTamUng.DataBindings.Add("Text", DataSource, "UngLuong", "{0:#,##0}");
            colThueTNCN.DataBindings.Add("Text", DataSource, "ThueTNCN", "{0:#,##0}");
            colThucNhan.DataBindings.Add("Text", DataSource, "ThucNhanFinal", "{0:#,##0}");

            colTongLuongPhepNam.DataBindings.Add("Text", DataSource, "LuongPhepNam");
            colTongLuongPhepNam.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongLuongTangCa.DataBindings.Add("Text", DataSource, "LuongTangCaFinal");
            colTongLuongTangCa.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongPhuCap.DataBindings.Add("Text", DataSource, "PhuCap");
            colTongPhuCap.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongLuongChinh.DataBindings.Add("Text", DataSource, "LuongThucTe");
            colTongLuongChinh.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongLuongLeTet.DataBindings.Add("Text", DataSource, "LuongLeTet");
            colTongLuongLeTet.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongTongLuong.DataBindings.Add("Text", DataSource, "TongLuong");
            colTongTongLuong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongBHXH.DataBindings.Add("Text", DataSource, "Tien_BHXH");
            colTongBHXH.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongTamUng.DataBindings.Add("Text", DataSource, "UngLuong");
            colTongTamUng.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongThueTNCN.DataBindings.Add("Text", DataSource, "ThueTNCN");
            colTongThueTNCN.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongThucNhan.DataBindings.Add("Text", DataSource, "ThucNhanFinal");
            colTongThucNhan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            col_subLuongPhepNam.DataBindings.Add("Text", DataSource, "LuongPhepNam");
            col_subLuongPhepNam.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subLuongTangCa.DataBindings.Add("Text", DataSource, "LuongTangCaFinal");
            col_subLuongTangCa.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subPhuCap.DataBindings.Add("Text", DataSource, "PhuCap");
            col_subPhuCap.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subLuongChinh.DataBindings.Add("Text", DataSource, "LuongThucTe");
            col_subLuongChinh.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subLuongLeTet.DataBindings.Add("Text", DataSource, "LuongLeTet");
            col_subLuongLeTet.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subTongLuong.DataBindings.Add("Text", DataSource, "TongLuong");
            col_subTongLuong.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subBHXH.DataBindings.Add("Text", DataSource, "Tien_BHXH");
            col_subBHXH.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subTamUng.DataBindings.Add("Text", DataSource, "UngLuong");
            col_subTamUng.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,##0}");

            col_subThueTNCN.DataBindings.Add("Text", DataSource, "ThueTNCN");
            col_subThueTNCN.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            col_subThucNhan.DataBindings.Add("Text", DataSource, "ThucNhanFinal");
            col_subThucNhan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

        }
    }
}
