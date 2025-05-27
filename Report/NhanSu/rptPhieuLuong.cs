using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VisualStudio2019.Class;

namespace VisualStudio2019.Report
{
    public partial class rptPhieuLuong : XtraReport
    {
        public rptPhieuLuong()
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


            lbl_ngaylap.Text = "Kỳ Lương Tháng " + Convert.ToDateTime(Form_Template.dtReport.Rows[0]["KyLuong"]).ToString("MM/yyyy");

            colMaNV.DataBindings.Add("Text", DataSource, "Code");
            colHoTen.DataBindings.Add("Text", DataSource, "TenNhanVien");
            col_PhongBan.DataBindings.Add("Text", DataSource, "PhongBan");
            col_ChucVu.DataBindings.Add("Text", DataSource, "ChucVu");

            colMucLuong.DataBindings.Add("Text", DataSource, "MucLuong").FormatString = "{0:#,##}";
            colLuongTangCaGio.DataBindings.Add("Text", DataSource, "LuongTangCaGio").FormatString = "{0:#,##}";

            colCong100.DataBindings.Add("Text", DataSource, "CongThucTe").FormatString = "{0:#,##0.0}";
            colCongCN.DataBindings.Add("Text", DataSource, "CongTangCa").FormatString = "{0:#,##0.0}";
            col_CongLeTet.DataBindings.Add("Text", DataSource, "CongLeTet").FormatString = "{0:#,##0.0}";
            colCong150.DataBindings.Add("Text", DataSource, "GioTangCa").FormatString = "{0:#,##0.0}";
            col_PhepNam.DataBindings.Add("Text", DataSource, "PhepNam").FormatString = "{0:#,##0.0}";

            colLuongChinh.DataBindings.Add("Text", DataSource, "LuongThucTe").FormatString = "{0:#,##}";           
            colLuongTangCa.DataBindings.Add("Text", DataSource, "LuongTangCa").FormatString = "{0:#,##}";
            colLuongCN.DataBindings.Add("Text", DataSource, "LuongCN").FormatString = "{0:#,##}";
            col_LuongLeTet.DataBindings.Add("Text", DataSource, "LuongLeTet").FormatString = "{0:#,##}";
            col_LuongPhepNam.DataBindings.Add("Text", DataSource, "LuongPhepNam").FormatString = "{0:#,##}";
            col_PhuCap.DataBindings.Add("Text", DataSource, "PhuCap").FormatString = "{0:#,##}";
            col_TongLuong.DataBindings.Add("Text", DataSource, "TongLuong").FormatString = "{0:#,##0}";

            col_BHXH.DataBindings.Add("Text", DataSource, "Tien_BHXH").FormatString = "{0:#,##0}";
            col_UngLuong.DataBindings.Add("Text", DataSource, "UngLuong").FormatString = "{0:#,##0}";
            col_ThueTNCN.DataBindings.Add("Text", DataSource, "ThueTNCN").FormatString = "{0:#,##0}";
            colThucLanh.DataBindings.Add("Text", DataSource, "ThucNhanFinal").FormatString = "{0:#,##0}";

            lbl_TenNhanVien.DataBindings.Add("Text", DataSource, "TenNhanVien");
        }
    }
}
