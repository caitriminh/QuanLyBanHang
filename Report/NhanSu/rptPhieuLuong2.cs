using DevExpress.XtraReports.UI;
using System;

namespace QuanLyKho.NghiepVu
{
    public partial class rptPhieuLuong2 : XtraReport
    {
        public rptPhieuLuong2()
        {
            InitializeComponent();
            // DataSource = Data.Data._dtreport;
        }


        public void BindData()
        {
            //var dt = ExecSQL.ExecProcedureDataFistOrDefault<khoThongTin>("prokhoThongTin", new { action = "GET_DATA" });
            //if (dt != null)
            //{
            //    lbl_tencty.Text = dt.tencty;
            //    lblDiaChi.Text = dt.diachi;
            //}

            //lbl_ngaylap.Text = "Kỳ Lương Tuần " + Convert.ToInt32(Data.Data._dtreport.Rows[0]["Tuan"]) + " / " + Convert.ToDateTime(Data.Data._dtreport.Rows[0]["Ky"]).Year;

            //colMaNV.DataBindings.Add("Text", Data.Data._dtreport, "MaNV");
            //colHoTen.DataBindings.Add("Text", Data.Data._dtreport, "HoTen");
            //colMucLuong.DataBindings.Add("Text", Data.Data._dtreport, "MucLuong").FormatString = "{0:#,##}";
            //colCong100.DataBindings.Add("Text", Data.Data._dtreport, "Cong100").FormatString = "{0:#,##0.0}";
            //colLuongChinh.DataBindings.Add("Text", Data.Data._dtreport, "Luong100").FormatString = "{0:#,##}";
            //colCong150.DataBindings.Add("Text", Data.Data._dtreport, "Cong150").FormatString = "{0:#,##0.0}";
            //colLuongTangCa.DataBindings.Add("Text", Data.Data._dtreport, "Luong150").FormatString = "{0:#,##}";
            //colPhat.DataBindings.Add("Text", Data.Data._dtreport, "Phat").FormatString = "{0:#,##0}";
            //colPhuCap.DataBindings.Add("Text", Data.Data._dtreport, "PhuCap").FormatString = "{0:#,##0}";
            //colThuong.DataBindings.Add("Text", Data.Data._dtreport, "Thuong").FormatString = "{0:#,##0}";
            //colThucLanh.DataBindings.Add("Text", Data.Data._dtreport, "ThucLanh").FormatString = "{0:#,##0}";


            //col_ngaycong.DataBindings.Add("Text", Data.Data._dtreport2, "NgayChamCong").FormatString = "{0:dd/MM/yyyy}";
            //col_vao.DataBindings.Add("Text", Data.Data._dtreport2, "start1").FormatString = "{0:HH:mm}";
            //col_ra.DataBindings.Add("Text", Data.Data._dtreport2, "end1").FormatString = "{0:HH:mm}";
            //col_cong.DataBindings.Add("Text", Data.Data._dtreport2, "TongCong").FormatString = "{0:#,##0.0}";
        }
    }
}
