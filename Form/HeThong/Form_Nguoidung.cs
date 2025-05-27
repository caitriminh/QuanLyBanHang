using DevExpress.XtraEditors;
using System;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_Nguoidung : XtraForm
    {
        public Form_Nguoidung()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 2
            });

            // Sử dụng null-conditional operator để tránh lỗi nếu data là null
            BtnThem.Enabled = data?.Luu ?? false;
            BtnXoa.Enabled = data?.Xoa ?? false;
            BtnSua.Enabled = data?.Sua ?? false;
            BtnExcel.Enabled = data?.Excel ?? false;
            BtnIn.Enabled = data?.InAn ?? false;
        }


        private void GetTaiKhoan()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_DangNhapSelectAll");
            grcNguoiDung.DataSource = data;
            lblTenDangNhap.DataBindings.Clear();
            lblTenDangNhap.DataBindings.Add("text", data, "TenDangNhap");
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nguoidung")
            {
                GetTaiKhoan();
            }
        }

        private void Form_Nguoidung_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetTaiKhoan();
        }
      
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.TenDangNhap = lblTenDangNhap.Text;
            Form_Template.Edit = true;
            Form_ThemNguoiDung frm = new Form_ThemNguoiDung();
            frm.ShowDialog();
        }
        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa tên tài khoản này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangNhapDelete", new
            {
                TenDangNhap=lblTenDangNhap.Text,
            });
            if (data.errcode == 1)
            {
                GetTaiKhoan();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTaiKhoan();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.TenDangNhap = "";
            Form_Template.Edit = false;
            Form_ThemNguoiDung frm = new Form_ThemNguoiDung();
            frm.ShowDialog();
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {           
            if (e.Column == colReset)
            {               
                var dgr = XtraMessageBox.Show($"Bạn có muốn khôi phục mật khẩu ({lblTenDangNhap.Text}) không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ResetPass", new
                {
                    tendn = lblTenDangNhap.Text,
                    matkhau = Ultis.GetMd5Hash(lblTenDangNhap.Text),
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "NguoiDung_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcNguoiDung.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }
    }
  }
