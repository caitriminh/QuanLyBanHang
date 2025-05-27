using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_DSNhanVien : XtraReport
    {
        public Rpt_DSNhanVien()
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

            GroupHeader1.GroupFields.Add(new GroupField("PhongBanText", XRColumnSortOrder.Ascending));
            lbl_PhongBan.DataBindings.Add("Text", null, "PhongBanText", "Phòng ban: {0}");

            colMaNV.DataBindings.Add("Text", DataSource, "MaNV");
            colHoTen.DataBindings.Add("Text", DataSource, "TenNhanVien");
            colGioiTinh.DataBindings.Add("Text", DataSource, "GioiTinh");
            colDienThoai.DataBindings.Add("Text", DataSource, "DienThoai");
            colNgaySinh.DataBindings.Add("Text", DataSource, "NgaySinh", "{0:dd/MM/yyyy}");
            colEmail.DataBindings.Add("Text", DataSource, "Email");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            col_NoiSinh.DataBindings.Add("Text", DataSource, "NoiSinhText");
            col_ChucVu.DataBindings.Add("Text", DataSource, "ChucVuText");
        }
    }
}
