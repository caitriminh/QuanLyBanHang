using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_DinhMuc : XtraReport
    {
        public Rpt_DinhMuc()
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
            lbl_TenSP.Text = ":" + Form_Template.dtReport.Rows[0]["TenSanPham"].ToString().ToUpper();
            col_STT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##0}");            
            col_MaNL.DataBindings.Add("Text", DataSource, "CodeNL");
            col_DVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            col_TenNVL.DataBindings.Add("Text", DataSource, "TenNguyenLieu"); 
            col_SoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");
            col_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");

           
        }
    }
}
