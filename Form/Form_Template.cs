using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using QuanLyBanHang.Form.BaoCao;
using QuanLyBanHang.Form.HeThong;
using QuanLyBanHang.Form.TroGiup;
using QuanLyBanHang.Report.BanLamViec;
using SimpleBroker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using VisualStudio2019.Class;
using VisualStudio2019.Form;

namespace VisualStudio2019
{
    public partial class Form_Template : RibbonForm
    {
        public static string strMaHoaDon, TenDangNhap, strMaPhieuNhap, strMaDonDatHang, strDonMuaHang, strHoTen, strURLImage, strURLImageNV;
        public static bool Edit;
        public static bool Flag_Login = false;
        public static int IntReport, QuyenHan;
        public static int intMaNV=1;
        public static int intIDGiaBan, intIDGiaBanNCC, intIDChiecKhau, intIDSanPham, intIDMaKH, intIDMaNCC, intIDLoaiSP, intIDLoaiThuChi, intIDNhomQuyen, intIDNhomKhachhang, intIDDoiTuongCK, intIDDVT, intIDNhomSP, intIDDinhMuc, intIDTinhThanh, intIDTrinhDo, intIDPhongBan, intIDChucVu, intIDTonGiao, intIDDanToc, intIDNganHang, intIDLoaiNghiViec, intIDNhanVien, intIDQuanHe, intIDNguoiPhuThuoc, intIDLoaiHopDong, intIDHopDongLaoDong, intIDPhepNam, intIDDKTangCa, intIDLoaiNghiPhep, intIDNghiPhep_VS_CT, intIDChamCong, intIDBoSungCong, intIDQuyDoi, intIDMucLuong, intIDPhuCap, intIDUngLuong, intIDBangLuong, intIDHoaHongNhanVien, intIDGiaoViec;
        public static string TaiKhoanLogin, strLoaiPhieu, strMaPhieuThu;
        public static DateTime tungay, denngay;
        public static DataTable dtReport;

        [Obsolete]
        public Form_Template()
        {
            InitializeComponent();
            //InstallAutoUpdate();           
            InitSkins();
            InitSkinGallery();
            Assembly asm = typeof(OfficeSkins).Assembly;
            SkinManager.Default.RegisterAssembly(asm);
            //Load giao diện
            UserLookAndFeel.Default.SkinName = Ultis.GetSetting("ApplicationSkinName");
            var skin = CommonSkins.GetSkin(UserLookAndFeel.Default);
            DevExpress.Utils.Svg.SvgPalette fireBall = skin.CustomSvgPalettes[Ultis.GetSetting("ApplicationPalletName")];
            if (fireBall != null)
            {
                skin.SvgPalettes[Skin.DefaultSkinPaletteName].SetCustomPalette(fireBall);
            }
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

            FormClosing += (s, e) =>
            {
                Ultis.SetSetting("ApplicationSkinName", UserLookAndFeel.Default.SkinName);
                Ultis.SetSetting("ApplicationPalletName", UserLookAndFeel.Default.ActiveSvgPaletteName);
            };
            ///////////////////////////////          
            lblTenMay.Caption = Environment.MachineName;
            ReadURL();
        }

        private static Form_Template _defaultInstance;

        [Obsolete]
        public static Form_Template Default
        {
            get => _defaultInstance ?? (_defaultInstance = new Form_Template());
            set => _defaultInstance = value;
        }

        public void ReadURL()
        {
            string path = Path.Combine(Application.StartupPath, "config", "config.txt");

            // Kiểm tra xem tệp có tồn tại không
            if (File.Exists(path))
            {
                // Đọc tất cả các dòng từ tệp
                var lines = File.ReadLines(path).ToList();

                // Kiểm tra nếu số dòng đủ lớn
                if (lines.Count >= 7)
                {
                    strURLImage = ClsAes.Decrypt(lines[5], Form_CauHinh.password); // Dòng 6
                    strURLImageNV = ClsAes.Decrypt(lines[6], Form_CauHinh.password); // Dòng 7
                }
                else
                {
                    XtraMessageBox.Show("Tệp không đủ thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Không tìm thấy tệp cấu hình", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void InitSkins()
        {
            SkinManager.EnableFormSkins();
            BonusSkins.Register();
        }

        private void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(skinRibbonGalleryBarItem1, true);
        }


        public void OpenForm(Type typeform)
        {
            foreach (var frm in MdiChildren.Where(frm => frm.GetType() == typeform))
            {
                frm.Activate();
                return;
            }

            BeginInvoke(new Action(() =>
            {
                var form = (System.Windows.Forms.Form)(Activator.CreateInstance(typeform));
                form.MdiParent = this;
                form.Show();
            }));
        }

        //private System.Windows.Forms.Form GetChildFormByName(string formname)
        //{
        //    return MdiChildren.FirstOrDefault(f => f.Name == formname);
        //}

        public void GetPhanQuyen()
        {
            DataTable data = ExecSQL.ExecProcedureDataAsDataTable("sp_PhanQuyenSelect", new { NhomQuyen = QuyenHan });
            if (data.Rows.Count == 0) return;

            // Tạo ánh xạ giữa Menu và các BarButtonItem tương ứng
            var menuButtonMap = new Dictionary<int, BarButtonItem[]>
            {
                { 1, new[] { btnDoiMK } },
                { 2, new[] { btnUser } },
                { 25, new[] { btnPhanQuyen, btnNhomQuyen } },
                { 3, new[] { btnThongTin } },
                { 4, new[] { btnBackupDatabase } },
                { 5, new[] { btnRestore } },
                { 27, new[] { btnNhomDoiTac } },
                { 31, new[] { btnThietLapHeThong } },
                { 6, new[] { btnSP } },
                { 7, new[] { btnLoaiSP } },
                { 8, new[] { btnLoaiThuChi } },
                { 9, new[] { btnNCC } },
                { 10, new[] { btnKhachHang } },
                { 11, new[] { btnNV } },
                { 35, new[] { btnDonViTinh } },
                { 36, new[] { btnNhomSP } },
                { 42, new[] { btnDonGiaNCC } },
                { 43, new[] { btnDonMuaHang } },
                { 44, new[] { btnTheoDoiDonHang } },
                { 12, new[] { btnPhieuNhapKho } },
                { 13, new[] { btnNhapDieuChinh } },
                { 14, new[] { btnXuatBan } },
                { 15, new[] { btnPhieuXuatDieuChinh } },
                { 16, new[] { btnQuanLyGiaBan } },
                { 17, new[] { btnChiecKhau } },
                { 18, new[] { btnPhieuThu } },
                { 19, new[] { btnPhieuChi } },
                { 30, new[] { btnBaoGia } },
                { 32, new[] { btnChiecKhauDoiTuong } },
                { 33, new[] { btnTienChiecKhau } },
                { 37, new[] { btnDonDatHang } },
                { 38, new[] { btnDinhMuc } },
                { 39, new[] { btnDinhMucDonHang } },
                { 40, new[] { btnXuatNguyenLieu } },
                { 41, new[] { btnNhapMuaHang } },
                { 20, new[] { btnTheKho } },
                { 21, new[] { btnTonKho } },
                { 22, new[] { btnBaoCaoKD } },
                { 23, new[] { btnBaoCaoChiPhi } },
                { 24, new[] { btnBaoCaoDongTien } },
                { 26, new[] { btnBaoCaoNhapHang } },
                { 28, new[] { btnDoanhThuKhachHang } },
                { 29, new[] { btnNhapHangTheoNCC } },
                { 34, new[] { btnThongKeBanHang } },
                { 45, new[] { btnHoaHongNhanVien } },
                { 46, new[] { btnTinhThanh } },
                { 47, new[] { btnTrinhDo } },
                { 48, new[] { btnDanToc } },
                { 49, new[] { btnTonGiao } },
                { 50, new[] { btnPhongBan } },
                { 51, new[] { btnChucVu } },
                { 52, new[] { btnNganHang } },
                { 53, new[] { btnLoaiNghiViec } },
                { 54, new[] { btnQuanHe } },
                { 55, new[] { btnLoaiHopDong } },
                { 56, new[] { btnLoaiNghiPhep } },
                { 57, new[] { btnNhanVienNghiViec } },
                { 58, new[] { btnNguoiPhuThuoc } },
                { 59, new[] { btnHopDongLaoDong } },
                { 60, new[] { btnPhepNam } },
                { 61, new[] { btnNghiPhepVeSomCongTac } },
                { 62, new[] { btnDangKyTangCa } },
                { 63, new[] { btnChamCong } },
                { 64, new[] { btnBoSungCong } },
                { 65, new[] { btnChamCongChiTiet } },
                { 66, new[] { btnTongHopChamCong } },
                { 67, new[] { btnMucLuong } },
                { 68, new[] { btnPhuCap } },
                { 69, new[] { btnUngLuong } },
                { 70, new[] { btnBangLuong } },
            };

            // Duyệt qua từng dòng và đặt thuộc tính Enabled cho các BarButtonItem tương ứng
            foreach (DataRow row in data.Rows)
            {
                int menuId = Convert.ToInt32(row["Menu"]);
                bool isEnabled = Convert.ToBoolean(row["Them"]);

                if (menuButtonMap.ContainsKey(menuId))
                {
                    foreach (var button in menuButtonMap[menuId])
                    {
                        button.Enabled = isEnabled;
                    }
                }
            }
        }


        private void OnNext(MessageBroker value)
        {
            if (value.task == "login")
            {
                barStaticItem1.Caption = "Xin chào: " + strHoTen;
                lblTenDangNhap.Caption ="Tên đăng nhập: " + TaiKhoanLogin.ToUpper();
                lblThoiGian.Caption = "Thời gian: " + DateTime.Now.ToString();
                //Kiểm tra phân quyền
                GetPhanQuyen();
                OpenForm(typeof(frmBanLamViec));

            }
        }

        [Obsolete]
        //private void InstallAutoUpdate()
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        //    AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
        //    AutoUpdater.DownloadPath = "update";
        //}

        //private void CheckVersionApp()
        //{
        //    AutoUpdater.Start("http://125.212.221.113/update/update_qlbh.xml");
        //}

        //private void UpdateApp(UpdateInfoEventArgs args)
        //{
        //    try
        //    {
        //        Flag_Login = false;
        //        if (AutoUpdater.DownloadUpdate(args))
        //        {
        //            Application.Exit();
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private bool FlagUpdateAppNow = false;
        //[Obsolete]
        //private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        //{
        //    if (args.IsUpdateAvailable)
        //    {
        //        if (FlagUpdateAppNow)
        //        {
        //            FlagUpdateAppNow = false;
        //            UpdateApp(args);
        //        }
        //        var dialogResult = XtraMessageBox.Show($"Phần mềm có phiên bản mới {args.CurrentVersion}. Phiên bản bạn đang sử dụng hiện tại  {args.InstalledVersion}. Bạn có muốn cập nhật phần mềm không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //        if (dialogResult == DialogResult.Yes)
        //        {
        //            UpdateApp(args);
        //        }
        //        else
        //        {
        //            if (args.Mandatory.Value)
        //            {
        //                //Flag_Login = false;
        //                var dgrResult = XtraMessageBox.Show("Vui lòng cập nhật phần mềm. Nhấn Yes để thực hiện.", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //                if (dgrResult == DialogResult.Yes)
        //                    UpdateApp(args);
        //                else
        //                    Application.Exit();
        //            }
        //        }
        //    }            
        //}

        //lưu ý chổ documentManager1 >property>Mdiparent >Form_Template tức tên from main sẽ hiển thị dạng tabcontrol khi mở from khác.
        private void Form_Template_Load(object sender, EventArgs e)
        {
            //CheckVersionApp();
            Text = string.Format($"Quản Lý Bán Hàng - Version - {Assembly.GetExecutingAssembly().GetName().Version}");
            this.Subscribe<MessageBroker>(OnNext);
            using (Form_Login frm = new Form_Login())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
         
        }
        private void Btnnguoidung_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_Nguoidung));
        }
        private void Btnnhanvien_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_Nhanvien));
        }
        private void Btnkhachang_ItemClick(object sender, ItemClickEventArgs e)
        {          
            OpenForm(typeof(Form_Khachhang));
        }
        private void Btnsanpham_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_Sanpham));
        }
        private void Btnnhaphang_ItemClick(object sender, ItemClickEventArgs e)
        {
            strLoaiPhieu = "PN";           
            OpenForm(typeof(Form_Phieunhap));
        }
        private void Btnxuathang_ItemClick(object sender, ItemClickEventArgs e)
        {
            strLoaiPhieu = "XB";           
            OpenForm(typeof(Form_Phieuxuat));
        }
        private void Btnnhacungcap_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_Nhacungcap));
        }

       
        private void Btndangnhap_ItemClick(object sender, ItemClickEventArgs e)
        {          
            using (Form_Login frm = new Form_Login())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void Btnloaisanpham_ItemClick(object sender, ItemClickEventArgs e)
        {          
            OpenForm(typeof(Form_Loaisanpham));
        }

        private void btnThongTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {         
            using (Form_ThongTin frm = new Form_ThongTin())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void btnBackupDatabase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            using (Form_Backup frm = new Form_Backup())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void btnLoaiThuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_LoaiThuChi));
        }

        private void btnPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_PhieuChi));
        }

        private void btnBaoCaoNhapHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BaoCaoNhapHang));
        }

        private void btnNhomQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_NhomQuyenHan));
        }

        private void btnDoanhThuKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BaoCaoDoanhThuKhachHang));
        }

        private void btnNhapHangTheoNCC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BaoCaoNhapHangTheoNCC));
        }

        private void btnThietLap_ItemClick(object sender, ItemClickEventArgs e)
        {           
            using (Form_CauHinh frm = new Form_CauHinh())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void btnThietLapHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {            
            using (Form_ThietLapHeThong frm = new Form_ThietLapHeThong())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void BtnBaoGia_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BangBaoGia));
        }

        private void BtnBaoCaoDongTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BaoCaoDongTien));
        }

        private void BtnDonViTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_DonViTinh));
        }

        private void btnDonDatHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_DonDatHang));
        }

        private void BtnDinhMucDonHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_DinhMucDonHang));
        }

        private void btnNhapMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            strLoaiPhieu = "NVL";
            OpenForm(typeof(Form_PhieuNhapNVL));
        }

        private void btnChamCongChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_ChamCongChiTiet));
        }

        private void btnBanLamViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(frmBanLamViec));
        }

        private void btnGiaoViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(FrmGiaoViec));
        }

        private void btnTongHopChamCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_TongHopChamCong));
        }

        private void btnPhuCap_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_PhuCap));
        }

        private void btnUngLuong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_UngLuong));
        }

        private void btnBangLuong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BangLuong));
        }

        private void btnMucLuong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_MucLuong));
        }

        private void btnBoSungCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BoSungCong));
        }

        private void btnChamCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_ChamCong));
        }

        private void btnLoaiHopDong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_LoaiHopDong));
        }

        private void btnNghiPhepVeSomCongTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_NghiPhepVeSomCongTac));
        }

        private void btnLoaiNghiPhep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_LoaiNghiPhep));
        }

        private void btnDangKyTangCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_DangKyTangCa));
        }

        private void btnPhepNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_PhepNam));
        }

        private void btnHopDongLaoDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_HopDongLaoDong));
        }

        private void btnLoaiNghiViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_LoaiNghiViec));
        }

        private void btnQuanHe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_QuanHe));
        }

        private void btnNguoiPhuThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_NguoiPhuThuoc));
        }

        private void btnNhanVienNghiViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_NhanVienNghiViec));
        }

        private void btnXuatNguyenLieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            strLoaiPhieu = "XNL";
            OpenForm(typeof(Form_PhieuXuatNVL));
        }

        private void btnTheoDoiDonHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_TheoDoiMuaHang));
        }

        private void btnNganHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_NganHang));
        }

        private void btnDanToc_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_DanToc));
        }

        private void btnTonGiao_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_TonGiao));
        }

        private void btnChucVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_ChucVu));
        }

        private void btnPhongBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_PhongBan));
        }

        private void btnTrinhDo_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_TrinhDo));
        }

        private void btnLogOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (Form_Login frm = new Form_Login())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void btnTinhThanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_TinhThanh));
        }

        private void btnHoaHongNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_HoaHongNhanVien));
        }

        private void btnDonMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_DonMuaHang));
        }

        private void btnDonGiaNCC_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_GiaBanNCC));
        }

        private void btnLoiNhuan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_LoiNhuan));
        }

        private void btnDinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_DinhMuc));
        }

        private void btnNhomSP_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_NhomSanPham));
        }

        private void btnThongKeBanHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_ThongKeBanHang));
        }

        private void btnTienChiecKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BaoCaoTienChiecKhau));
        }

        private void btnChiecKhauDoiTuong_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_ChiecKhauDoiTuong));
        }

        private void btnNhomDoiTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_NhomDoiTac));
        }

        private void btnBaoCaoChiPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_BaoCaoChiPhi));
        }

        private void btnPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_PhieuThu));
        }

        private void btnTheKho_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_TheKho));
        }

        private void btnPhanQuyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(Form_PhanQuyen));
        }

        private void btnPhieuXuatDieuChinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            strLoaiPhieu = "PXDC";          
            OpenForm(typeof(Form_Phieuxuat_dieuchinh));
        }

        private void btnNhapDieuChinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            strLoaiPhieu = "PNDC";           
            OpenForm(typeof(Form_Phieunhap_dieuchinh));
        }

        private void btnDoiMK_ItemClick(object sender, ItemClickEventArgs e)
        {           
            using (Form_DoiMatKhau frm = new Form_DoiMatKhau())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void btnTonKho_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_TonKho));

        }
        private void btnRestore_ItemClick(object sender, ItemClickEventArgs e)
        {           
            using (form_Restore frm = new form_Restore())
            {
                if (FrmMaskedDialog.ShowDialog(this, frm) == DialogResult.OK)
                {

                }
            }
        }

        private void btnBaoCaoKD_ItemClick(object sender, ItemClickEventArgs e)
        {
            strLoaiPhieu = "XB";
            OpenForm(typeof(Form_BaoCaoDoanhThu));
        }

        private void btnQuanLyGiaBan_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_GiaBan));
        }

        private void btnChiecKhau_ItemClick(object sender, ItemClickEventArgs e)
        {           
            OpenForm(typeof(Form_ChiecKhau));
        }
    }
}
