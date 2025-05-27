using DevExpress.XtraReports.UI;
using System;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_TheoDoiMuaHang : XtraReport
    {
        public Rpt_TheoDoiMuaHang()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            System.Data.DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ThongTinSelect");
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
            //GroupHeader1.GroupFields.Add(new GroupField("MaDonHang", XRColumnSortOrder.Ascending));
            //col_MaDonHang.DataBindings.Add("Text", null, "MaDonHang", "Số đơn hàng: {0}");           

            colTinhTrang.DataBindings.Add("Text", DataSource, "TinhTrang");
            col_SoDH.DataBindings.Add("Text", DataSource, "MaDonHang");
            col_NgayDatHang.DataBindings.Add("Text", DataSource, "NgayDatHang", "{0:dd/MM/yyyy}");
            col_NgayGiaoHang.DataBindings.Add("Text", DataSource, "NgayGiaoHang", "{0:dd/MM/yyyy}");
            col_NCC.DataBindings.Add("Text", DataSource, "TenNhaCungCap");            
            col_DVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colMaNVL.DataBindings.Add("Text", DataSource, "Code");
            col_TenSP.DataBindings.Add("Text", DataSource, "TenSanPham");
            col_SoLuong.DataBindings.Add("Text", DataSource, "SoLuong", "{0:#,##0}"); 
            col_SLNhan.DataBindings.Add("Text", DataSource, "SLNhan", "{0:#,##0}");
            col_ConLai.DataBindings.Add("Text", DataSource, "ConLai", "{0:#,##0}");

            colTongSL.DataBindings.Add("Text", DataSource, "SoLuong");
            colTongSL.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

         
            colTongSLNhan.DataBindings.Add("Text", DataSource, "SLNhan");
            colTongSLNhan.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

            colTongConLai.DataBindings.Add("Text", DataSource, "ConLai");
            colTongConLai.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");
        }
    }
}
