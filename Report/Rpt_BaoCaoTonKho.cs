using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BaoCaoTonKho : XtraReport
    {
        public Rpt_BaoCaoTonKho()
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
            //GroupHeader1.GroupFields.Add(new GroupField("TenLoai", XRColumnSortOrder.Ascending));
            //colLoaiSanPham.DataBindings.Add("Text", null, "TenLoai", "Loại sản phẩm: {0}");
           
            colMaSP.DataBindings.Add("Text", DataSource, "Code");  
            colLoaiSP.DataBindings.Add("Text", DataSource, "TenLoai");
            colTenSP.DataBindings.Add("Text", DataSource, "TenSanPham");
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");         
            colKy.DataBindings.Add("Text", DataSource, "Ky","{0: dd/MM/yyyy}");
            colTonDau.DataBindings.Add("Text", DataSource, "TonDau", "{0: #,##0}");
            colSLNhap.DataBindings.Add("Text", DataSource, "SLNhap", "{0: #,##0}");
            colSLXuat.DataBindings.Add("Text", DataSource, "SLXuat", "{0: #,##0}");
            colTonCuoi.DataBindings.Add("Text", DataSource, "TonCuoi", "{0: #,##0}");

            colTongTondau.DataBindings.Add("Text", DataSource, "TonDau");
            colTongTondau.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongNhap.DataBindings.Add("Text", DataSource, "SLNhap");
            colTongNhap.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongXuat.DataBindings.Add("Text", DataSource, "SLXuat");
            colTongXuat.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongTonCuoi.DataBindings.Add("Text", DataSource, "TonCuoi");
            colTongTonCuoi.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
        }
    }
}
