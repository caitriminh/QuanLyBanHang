using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_BangGia : XtraReport
    {
        public Rpt_BangGia()
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
            GroupHeader1.GroupFields.Add(new GroupField("LoaiSanPham", XRColumnSortOrder.Ascending));
            colLoaiSanPham.DataBindings.Add("Text", null, "LoaiSanPham", "Loại sản phẩm: {0}");

            colMaSP.DataBindings.Add("Text", DataSource, "Code");           
            colTenSP.DataBindings.Add("Text", DataSource, "TenSanPham");
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colVAT.DataBindings.Add("Text", DataSource, "VAT", "{0:#,##0}");         
            colGiaSauThue.DataBindings.Add("Text", DataSource, "GiaSauThue", "{0:#,##0}");
            colGiaTruocThue.DataBindings.Add("Text", DataSource, "GiaTruocThue", "{0:#,##0}");
            colNgayApDung.DataBindings.Add("Text", DataSource, "NgayApDung", "{0:dd/MM/yyyy}");

           
        }
    }
}
