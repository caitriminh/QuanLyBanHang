using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_Nhanvien : XtraForm
    {

        public int option = 1;
        public static List<NhanVien> nhanvien = new List<NhanVien>();
        public Form_Nhanvien()
        {
            InitializeComponent();
            splitContainerControl2.Collapsed = true;
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 11
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;
        }


        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nhanvien")
            {
                GetNhanVien(option);
                //Load Ảnh
                string sourceFilePath = Form_Template.strURLImageNV + @"\" + txtMaNV.Text + ".jpg";
                try
                {
                    pictureEdit1.Image = new Bitmap(sourceFilePath);
                }
                catch (Exception)
                {
                    pictureEdit1.Image = new Bitmap(Application.StartupPath + @"\logo\no_image.jpg");
                }
            }
        }

        private void Form_Nhanvien_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhanVien(option);
        }

        public void GetNhanVien(int option)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienSelect", new
            {
                option
            });
            grcNhanVien.DataSource = dt;
            if (dt.Rows.Count == 0) { return; }
            lblID.DataBindings.Clear();
            lblID.DataBindings.Add("text", dt, "ID");
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhanVien(option);
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DanhSachNhanVien_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcNhanVien.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show("Có muốn xóa tên nhân viên (" + gridView1.GetRowCellValue(i, "TenNhanVien") + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhanVienDeleteByID", new
            {
                ID = Convert.ToInt32(lblID.Text)
            });
            if (data.errcode == 1)
            {
                GetNhanVien(option);
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = true;
            Form_Template.intMaNV = Convert.ToInt32(lblID.Text);
            Form_ThemNhanVien frm = new Form_ThemNhanVien();
            frm.ShowDialog();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intMaNV = 0;
            Form_ThemNhanVien frm = new Form_ThemNhanVien();
            frm.ShowDialog();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienSelect");
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 8;
            var frm = new Form_Report();
            frm.Show();
        }
     
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            try
            {
                nhanvien.Clear();
                nhanvien.Add(new NhanVien()
                {
                    ID = Convert.ToInt32(gridView1.GetRowCellValue(i, "ID")),
                    MaNV = gridView1.GetRowCellValue(i, "MaNV") == DBNull.Value ? "" : gridView1.GetRowCellValue(i, "MaNV").ToString(),
                    TenNhanVien = gridView1.GetRowCellValue(i, "TenNhanVien") == DBNull.Value ? "" : gridView1.GetRowCellValue(i, "TenNhanVien").ToString(),
                    DienThoai = gridView1.GetRowCellValue(i, "DienThoai") == DBNull.Value ? "" : gridView1.GetRowCellValue(i, "DienThoai").ToString(),
                    Email = gridView1.GetRowCellValue(i, "Email") == DBNull.Value ? "" : gridView1.GetRowCellValue(i, "Email").ToString(),
                    NoiSinhText = gridView1.GetRowCellValue(i, "NoiSinhText") == DBNull.Value ? "" : gridView1.GetRowCellValue(i, "NoiSinhText").ToString(),
                    ChucVuText = gridView1.GetRowCellValue(i, "ChucVuText") == DBNull.Value ? "" : gridView1.GetRowCellValue(i, "ChucVuText").ToString(),
                    PhongBanText = gridView1.GetRowCellValue(i, "PhongBanText") == DBNull.Value ? "" : gridView1.GetRowCellValue(i, "PhongBanText").ToString(),
                    NgayNhanViec = Convert.ToDateTime(gridView1.GetRowCellValue(i, "NgayNhanViec")),
                    NgaySinh = Convert.ToDateTime(gridView1.GetRowCellValue(i, "NgaySinh"))
                });
                txtMaNV.Text = nhanvien[0].MaNV;
                txtHoTen.Text = nhanvien[0].TenNhanVien;
                dteNgaySinh.EditValue = nhanvien[0].NgaySinh;
                txtNoiSinh.Text = nhanvien[0].NoiSinhText;
                txtSoDT.Text = nhanvien[0].DienThoai;
                txtEmail.Text = nhanvien[0].Email;
                dteNgayNhanViec.EditValue = nhanvien[0].NgayNhanViec;
                txtPhongBan.Text = nhanvien[0].PhongBanText;
                txtChucVu.Text = nhanvien[0].ChucVuText;
                //Load danh sách người phụ thuộc
                var data = ExecSQL.ExecProcedureDataAsDataTable("sp_NguoiPhuThuocSelectAll", new
                {
                    MaNV = nhanvien[0].ID.ToString()
                });
                grcNguoiPhuThuoc.DataSource = data;
                //Load danh sách mức lương
                var data1 = ExecSQL.ExecProcedureDataAsDataTable("sp_MucLuongSelectAll", new
                {
                    MaNV = nhanvien[0].ID.ToString()
                });
                grcMucLuong.DataSource = data1;
                //Load danh sách hợp đồng lao đồng
                var data2 = ExecSQL.ExecProcedureDataAsDataTable("sp_HopDongLaoDongSelectAll", new
                {
                    MaNV = nhanvien[0].ID.ToString()
                });
                grcHopDongLaoDong.DataSource = data2;
            }
            catch (Exception)
            {
               
               
            }
            
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            using (XtraOpenFileDialog openFileDialog = new XtraOpenFileDialog())
            {
                //openFileDialog.InitialDirectory = @"C:\"; // Thư mục mặc định
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Bộ lọc file
                openFileDialog.Title = "Chọn ảnh để cập nhật";
                try
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureEdit1.Image?.Dispose();
                        // Lấy đường dẫn của hình ảnh được chọn
                        string sourceFilePath = openFileDialog.FileName;
                        // Hiển thị hình ảnh trong PictureBox (tùy chọn)
                        pictureEdit1.Image = new Bitmap(sourceFilePath);

                        // Tạo thư mục nếu chưa tồn tại
                        if (!Directory.Exists(Form_Template.strURLImageNV))
                        {
                            Directory.CreateDirectory(Form_Template.strURLImageNV);
                        }

                        // Đường dẫn lưu ảnh trong thư mục D:/Image
                        string destinationFilePath = Path.Combine(Form_Template.strURLImageNV, txtMaNV.Text + ".jpg");
                        // Sao chép file từ thư mục gốc sang thư mục đích
                        File.Copy(sourceFilePath, destinationFilePath, true);
                        XtraMessageBox.Show("Cập nhật ảnh nhân viên thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }      

        private void txtMaNV_EditValueChanged(object sender, EventArgs e)
        {
            string sourceFilePath = Form_Template.strURLImageNV + @"\" + txtMaNV.Text + ".jpg";
            try
            {
                pictureEdit1.Image = new Bitmap(sourceFilePath);
            }
            catch (Exception)
            {
                pictureEdit1.Image = new Bitmap(Application.StartupPath + @"\logo\no_image.jpg");
            }
        }

        private void chkTatCa_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhanVien(chkTatCa.Checked ? 2 : 1);
        }
    }
}