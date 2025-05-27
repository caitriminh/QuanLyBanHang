using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class rptBangChiTietChamCong : XtraReport
    {
        public rptBangChiTietChamCong()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ThongTinSelect");
            if (dt == null) { return; }
            lbl_tencty.Text = dt.Rows[0]["TenDonVi"].ToString();
            xrLabel2.Text = "Đ/c: " + dt.Rows[0]["DiaChi"].ToString();
            lbl_sodt_sofax.Text = "Số ĐT: " + dt.Rows[0]["SoDT"].ToString();


            lbl_ngaylap.Text = "Từ ngày " + Convert.ToDateTime(Form_Template.tungay).ToString("dd/MM/yyyyy") + " đến ngày " + Convert.ToDateTime(Form_Template.denngay).ToString("dd/MM/yyyyy");

            col_TenNhanVien.Text  = @": " + Form_Template.dtReport.Rows[0]["TenNhanVien"].ToString();
            col_PhongBan.Text = @": " + Form_Template.dtReport.Rows[0]["PhongBan"].ToString();
            col_ChucVu.Text = @": " + Form_Template.dtReport.Rows[0]["ChucVu"].ToString();

            col_NgayChamCong.DataBindings.Add("Text", DataSource, "NgayChamCong", "{0:dd/MM/yyyy}");
            col_GioVao.DataBindings.Add("Text", DataSource, "Vao1");
            col_GioRa.DataBindings.Add("Text", DataSource, "Ra1");
            col_Cong100.DataBindings.Add("Text", DataSource, "Cong100", "{0:#,##0.0}");
            col_CongTangCa.DataBindings.Add("Text", DataSource, "CongTangCa", "{0:#,##0.0}");
            col_TongCong.DataBindings.Add("Text", DataSource, "TongCong", "{0:#,##0.0}");
            col_GioTangCa.DataBindings.Add("Text", DataSource, "Cong150", "{0:#,##0.0}");

            col_TongCong100.DataBindings.Add("Text", DataSource, "Cong100");
            col_TongCong100.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_TongCongTangCa.DataBindings.Add("Text", DataSource, "CongTangCa");
            col_TongCongTangCa.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_TongTongCong.DataBindings.Add("Text", DataSource, "TongCong");
            col_TongTongCong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            col_TongGioTangCa.DataBindings.Add("Text", DataSource, "Cong150");
            col_TongGioTangCa.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0.0}");

            lbl_TenNhanVien.Text = Form_Template.dtReport.Rows[0]["TenNhanVien"].ToString();
        }
    }
}
