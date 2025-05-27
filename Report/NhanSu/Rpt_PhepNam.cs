using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class Rpt_PhepNam : XtraReport
    {
        public Rpt_PhepNam()
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

            GroupHeader1.GroupFields.Add(new GroupField("PhongBan", XRColumnSortOrder.Ascending));
            lbl_PhongBan.DataBindings.Add("Text", null, "PhongBan", "Phòng ban: {0}");

            colMaNV.DataBindings.Add("Text", DataSource, "Code");
            colHoTen.DataBindings.Add("Text", DataSource, "TenNhanVien");
            colGioiTinh.DataBindings.Add("Text", DataSource, "GioiTinh");
            col_NgayChinhThuc.DataBindings.Add("Text", DataSource, "NgayChinhThuc", "{0:dd/MM/yyyy}");
            colNgaySinh.DataBindings.Add("Text", DataSource, "NgaySinh", "{0:dd/MM/yyyy}");
            col_TongNgayPhep.DataBindings.Add("Text", DataSource, "TongNgayPhep", "{0:#,##0.0}");
            col_SoNgayNghi.DataBindings.Add("Text", DataSource, "SoNgayNghi", "{0:#,##0.0}");
            col_NgayNhanViec.DataBindings.Add("Text", DataSource, "NgayNhanViec", "{0:dd/MM/yyyy}");
            col_ChucVu.DataBindings.Add("Text", DataSource, "ChucVu");
            col_ConLai.DataBindings.Add("Text", DataSource, "ConLai", "{0:#,##0.0}");
        }
    }
}
