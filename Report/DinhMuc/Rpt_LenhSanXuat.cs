using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_LenhSanXuat : XtraReport
    {
        public Rpt_LenhSanXuat()
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
            GroupHeader1.GroupFields.Add(new GroupField("NgayGiaoHang", XRColumnSortOrder.Descending));
            colgroup_ngaygiaohang.DataBindings.Add("Text", null, "NgayGiaoHang", "Ngày giao hàng: {0:dd/MM/yyyy}");

            colngaygiaohang.DataBindings.Add("Text", DataSource, "NgayGiaoHang", "{0:dd/MM/yyyy}");           
            colthu.DataBindings.Add("Text", DataSource, "Thu");
            colkhachhang.DataBindings.Add("Text", DataSource, "TenKhachHang");         
            coltensp.DataBindings.Add("Text", DataSource, "TenSanPham");
            colsoluong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}");
            colghichu.DataBindings.Add("Text", DataSource, "GhiChu");

            colSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##0}");

            coltongsoluong.DataBindings.Add("Text", DataSource, "SoLuong");
            coltongsoluong.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

          
        }
    }
}
