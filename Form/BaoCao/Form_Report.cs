using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using VisualStudio2019.Report;

namespace VisualStudio2019.Form
{
    public partial class Form_Report : XtraForm
    {
             
        public Form_Report()
        {
            InitializeComponent();
        }
        private void Form_Report_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            switch (Form_Template.IntReport)
            {
                case 1:
                    {
                        Text = @"Phiếu Xuất Hàng";
                        var rpt = new Rpt_PhieuXuat();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 2:
                    {
                        Text = @"Danh Sách Khách Hàng";
                        var rpt = new Rpt_DSKhachHang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 3:
                    {
                        Text = @"Báo Cáo Kinh Doanh";
                        var rpt = new Rpt_BaoCaoDoanhThu();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 4:
                    {
                        Text = @"Danh Sách Sản Phẩm";
                        var rpt = new Rpt_DSSanPham();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 5:
                    {
                        Text = @"Phiếu Nhập Hàng";
                        var rpt = new Rpt_PhieuNhap();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 6:
                    {
                        Text = @"Báo Cáo Tồn Kho";
                        var rpt = new Rpt_BaoCaoTonKho();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 7:
                    {
                        Text = @"Danh Sách Nhà Cung Cấp";
                        var rpt = new Rpt_DSNhaCungCap();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 8:
                    {
                        Text = @"Danh Sách Nhân Viên";
                        var rpt = new Rpt_DSNhanVien();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 9:
                    {
                        Text = @"Phiếu Thu";
                        var rpt = new Rpt_PhieuThu();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 10:
                    {
                        Text = @"Phiếu Chi";
                        var rpt = new Rpt_PhieuChi();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 11:
                    {
                        Text = @"Báo Cáo Nhập Hàng";
                        var rpt = new Rpt_BaoCaoNhapHang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 12:
                    {
                        Text = @"Báo Cáo Chi Phí";
                        var rpt = new Rpt_BaoCaoChiPhi();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 13:
                    {
                        Text = @"Doanh Thu Theo Khách Hàng";
                        var rpt = new Rpt_DoanhThuTheoKhachHang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 14:
                    {
                        Text = @"Nhập Hàng Theo Nhà Cung Cấp";
                        var rpt = new Rpt_NhapHangTheoNCC();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 15:
                    {
                        Text = @"Bảng Giá";
                        var rpt = new Rpt_BangGia();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 16:
                    {
                        Text = @"Bảng Báo Giá";
                        var rpt = new Rpt_BangBaoGia();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 17:
                    {
                        Text = @"Bảng Báo Giá v2";
                        var rpt = new Rpt_BangBaoGiav2();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 18:
                    {
                        Text = @"Báo Cáo Tiền Chiếc Khấu";
                        var rpt = new Rpt_BaoCaoTienChiecKhau();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 19:
                    {
                        Text = @"Thống Kê Bán Hàng";
                        var rpt = new Rpt_ThongKeBanHang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 20:
                    {
                        Text = @"Định Mức Sản Phẩm";
                        var rpt = new Rpt_DinhMuc();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 21:
                    {
                        Text = @"Đơn Đặt Hàng";
                        var rpt = new Rpt_DonDatHang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 22:
                    {
                        Text = @"Phiếu Xuất Báng Hàng";
                        var rpt = new Rpt_PhieuXuatA5();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 23:
                    {
                        Text = @"Đơn Mua Hàng";
                        var rpt = new Rpt_DonMuaHang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 24:
                    {
                        Text = @"Báo Cáo Theo Dõi Đơn Hàng";
                        var rpt = new Rpt_TheoDoiMuaHang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 25:
                    {
                        Text = @"Bảng Giá Nhà Cung Cấp";
                        var rpt = new Rpt_BangGiaNCC();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 26:
                    {
                        Text = @"Doanh Số Nhân Viên";
                        var rpt = new Rpt_HoaHongNhanVien();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 27:
                    {
                        Text = @"Nhân Viên Nghỉ Việc";
                        var rpt = new Rpt_DSNhanVienNghiViec();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 28:
                    {
                        Text = @"Bảng Tổng Hợp Phép Năm";
                        var rpt = new Rpt_PhepNam();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 29:
                    {
                        Text = @"Phiếu Lương";
                        var rpt = new rptPhieuLuong();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 30:
                    {
                        Text = @"Bảng Lương Nhân Viên";
                        var rpt = new Rpt_BangLuongNhanVien();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 31:
                    {
                        Text = @"Bảng Chi Tiết Chấm Công";
                        var rpt = new rptBangChiTietChamCong();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 32:
                    {
                        Text = @"Báo Cáo Tiền Chiếc Khấu";
                        var rpt = new Rpt_BaoCaoTienChiecKhauA5();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 33:
                    {
                        Text = @"Đơn Đặt Hàng";
                        var rpt = new Rpt_DonDatHangA5();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 34:
                    {
                        Text = @"Phiếu Nhập Kho";
                        var rpt = new Rpt_PhieuNhapA5();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 35:
                    {
                        Text = @"Phiếu Chi";
                        var rpt = new Rpt_PhieuChiA5();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 36:
                    {
                        Text = @"Phiếu Thu";
                        var rpt = new Rpt_PhieuThuA5();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 37:
                    {
                        Text = @"Lệnh Sản Xuất";
                        var rpt = new Rpt_LenhSanXuat();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 38:
                    {
                        Text = @"Phiếu Giao Nhận";
                        var rpt = new Rpt_PhieuGiaoNhan();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 39:
                    {
                        Text = @"Đơn Hàng Tài Trợ";
                        var rpt = new Rpt_DonHangTaiTro();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 40:
                    {
                        Text = @"Thống Kê Phiếu Thu";
                        var rpt = new Rpt_ThongKePhieuThu();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 41:
                    {
                        Text = @"Thống Kê Phiếu Chi";
                        var rpt = new Rpt_ThongKePhieuChi();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 42:
                    {
                        Text = @"Hoa Hồng Nhân Viên";
                        var rpt = new Rpt_HoaHongNhanVienv2();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 43:
                    {
                        Text = @"Đơn Hàng Quà Tặng";
                        var rpt = new Rpt_DonHangQuaTang();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
                case 44:
                    {
                        Text = @"Báo Cáo Công Việc";
                        var rpt = new Rpt_BaoCaoCongViec();
                        documentViewer1.PrintingSystem = rpt.PrintingSystem;
                        rpt.DataSource = Form_Template.dtReport;
                        rpt.BindData();
                        rpt.CreateDocument();
                    }
                    break;
            }
        }
    }
}