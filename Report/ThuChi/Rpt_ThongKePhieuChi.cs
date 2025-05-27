using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_ThongKePhieuChi : XtraReport
    {
        public Rpt_ThongKePhieuChi()
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
            lbl_NguoiDaiDien.Text = dt.Rows[0]["NguoiDaiDien"].ToString();
            /////////////////////////////////////////////////////////////////////
            lblNgayIn.Text = DateTime.Now.ToString();
            lblNgayThang.Text = "Ngày   " + DateTime.Now.Day.ToString() + "   tháng   " + DateTime.Now.Month.ToString() + "  năm  " + DateTime.Now.Year.ToString();
            //GroupHeader1.GroupFields.Add(new GroupField("TenLoai", XRColumnSortOrder.Ascending));
            //colLoaiSanPham.DataBindings.Add("Text", null, "TenLoai", "Loại sản phẩm: {0}");
         

            colSTT.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.RecordNumber, "{0:#,##0}");
            colMaPhieu.DataBindings.Add("Text", DataSource, "MaPhieu");
            colTenKH.DataBindings.Add("Text", DataSource, "TenNhaCungCap");
            colNgayNhap.DataBindings.Add("Text", DataSource, "NgayChungTu", "{0:dd/MM/yyyy}");
            colLoaiThuChi.DataBindings.Add("Text", DataSource, "LoaiThuChi");         
            colDienGiai.DataBindings.Add("Text", DataSource, "DienGiai");         
            colThanhTien.DataBindings.Add("Text", DataSource, "SoTien", "{0:#,##0}");

           

            colTongThanhTien.DataBindings.Add("Text", DataSource, "SoTien");
            colTongThanhTien.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,##0}");

          
        }
    }
}
