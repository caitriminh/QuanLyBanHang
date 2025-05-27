using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BaoCaoCongViec : XtraReport
    {
        public Rpt_BaoCaoCongViec()
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
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            xrLabel2.Text = "Từ " + Form_Template.tungay.ToString("dd/MM/yyyy") + " đến " + Form_Template.denngay.ToString("dd/MM/yyyy");
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();

            colSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##0}");           
            colNgayNhap.DataBindings.Add("Text", DataSource, "NgayNhap","{0:dd/MM/yyyy}");
            colTenTruong.DataBindings.Add("Text", DataSource, "TenTruong");
            colSoLuongHS.DataBindings.Add("Text", DataSource, "SoLuongHS");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colHieuTruong.DataBindings.Add("Text", DataSource, "HieuTruong");
            colNoiDung.DataBindings.Add("Text", DataSource, "NoiDung");
            colKhoKhan.DataBindings.Add("Text", DataSource, "KhoKhan");
            colHuongXuLy.DataBindings.Add("Text", DataSource, "HuongXuLy");
            colKetQua.DataBindings.Add("Text", DataSource, "KetQua");
        }
    }
}
