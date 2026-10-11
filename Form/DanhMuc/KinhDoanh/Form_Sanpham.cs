using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using VisualStudio2019.Class;
using ExcelDataReader;
using System.Diagnostics;
using QuanLyBanHang.Class;
using System.Linq;
using System.Drawing;

namespace VisualStudio2019.Form
{
    public partial class Form_Sanpham : XtraForm
    {
        public static string strMaSP = "";     
        public int option = 1;
        public Form_Sanpham()
        {
            InitializeComponent();          
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 6
            });
            if (data == null) { return; }
            btnOptionThem.Enabled = data.Luu;            
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled= data.Sua;
            Excel.Enabled= data.Excel;
            BtnIn.Enabled = data.InAn;
            btnCapNhatChiecKhau.Enabled = data.Luu;
            btnCapNhatGiaBan.Enabled = data.Luu;
            btnXoaAnhSP.Enabled = data.Xoa;
        }
       
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_sanpham")
            {
                GetSanPham(option);
            }
        }

        private void Form_Sanpham_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetSanPham(option);
        }

        public void GetSanPham(int option)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstSP = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelect", new
            {
                option
            });
            grcSanPham.DataSource = lstSP;
            lbl_ID.DataBindings.Clear();
            txtMaSP.DataBindings.Clear();
         
            lbl_ID.DataBindings.Add("text", lstSP, "ID");
            txtMaSP.DataBindings.Add("text", lstSP, "MaSP");            
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetSanPham(option);
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            var dgr = XtraMessageBox.Show("Có muốn xóa tên sản phẩm này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_SanPhamDeleteByID", new
            {
                id = Convert.ToInt32(lbl_ID.Text)
            });
            if (data.errcode==1)
            {
                GetSanPham(option);
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDSanPham = 0;
            Form_ThemSanPham frm = new Form_ThemSanPham();
            frm.ShowDialog();
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = true;
            Form_Template.intIDSanPham = Convert.ToInt32(lbl_ID.Text);
            Form_ThemSanPham frm = new Form_ThemSanPham();
            frm.ShowDialog();
        }

        private void btnXuatExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DanhSachSanPham_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcSanPham.ExportToXlsx(xtraSaveFileDialog1.FileName);
            // Mở file với ứng dụng mặc định
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        public void ImportExcelDataToSQL(string filePath)
        {
            int dem = 0;
            // Đọc dữ liệu từ file Excel           
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Sử dụng dòng đầu tiên làm tiêu đề cột
                        }
                    });
                    DataTable dataTable = result.Tables[0]; // Giả sử dữ liệu nằm ở sheet đầu tiên


                 
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string maSP = ExecSQL.ExecProcedureSacalar("spGetAuToProductCode").ToString();
                        string tenSP = row["TenSanPham"].ToString();
                        string dvt = row["DonViTinh"].ToString();
                        string ghichu = row["GhiChu"].ToString();
                        string LoaiSanPham = row["TenLoai"].ToString();                      
                        string QuyCach = row["QuyCach"].ToString();
                        int VAT = Convert.ToInt32(row["VAT"]);
                        decimal GiaBan = Convert.ToDecimal(row["GiaBan"]);
                        int IDLoaiHang = row["LoaiHang"].ToString()=="Sản xuất" ? 1 : 2;
                        var IDLoaiSP = ExecSQL.ExecProcedureSacalar("sp_LoaiSanPhamSelectByTenLoai", new
                        {
                            TenLoai = LoaiSanPham
                        });
                        var MaDVT = ExecSQL.ExecProcedureSacalar("sp_DonViTinhSelectByTenDVT", new
                        {
                            DonViTinh = dvt
                        });
                        //Cập nhật data
                        var data = ExecSQL.ExecProcedureNonData("spImportSanPham", new
                        {
                            MaSP=maSP,
                            TenSanPham=tenSP,
                            MaDVT,
                            LoaiSanPham=Convert.ToInt32(IDLoaiSP.ToString()),
                            GhiChu=ghichu,
                            QuyCach,
                            IDLoaiHang=IDLoaiHang,
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                            VAT= VAT,
                            GiaSauThue = GiaBan
                        });
                        dem += 1;
                    }
                }
            }
            GetSanPham(option);
            XtraMessageBox.Show("Đã nhập được " + dem.ToString() + " dòng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      
        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraOpenFileDialog1.Filter = "Excel files |*.xlsx";
            xtraOpenFileDialog1.FileName = "Chọn file Excel";
            if (xtraOpenFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            ImportExcelDataToSQL(xtraOpenFileDialog1.FileName);
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelect");
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 4;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnExportDemoExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phẩm trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinID = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "ID"));

            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "MauImportSanPham_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamExportExcel", new { ID = joinID });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void btnCapNhatChiecKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phẩm trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            strMaSP = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "ID"));

            Form_ThemChiecKhauTuSP frm = new Form_ThemChiecKhauTuSP();
            frm.ShowDialog();
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
                        if (!Directory.Exists(Form_Template.strURLImage))
                        {
                            Directory.CreateDirectory(Form_Template.strURLImage);
                        }

                        // Đường dẫn lưu ảnh trong thư mục D:/Image
                        string destinationFilePath = Path.Combine(Form_Template.strURLImage, txtMaSP.Text + ".jpg");

                        // Sao chép file từ thư mục gốc sang thư mục đích
                        File.Copy(sourceFilePath, destinationFilePath, true);
                        var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_SanPhamUpdateImage", new
                        {
                            ID = Convert.ToInt32(lbl_ID.Text),
                            TenHinh = txtMaSP.Text,
                            NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                        });

                        if (data.errcode == 1)
                        {
                            XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtMaSP_TextChanged(object sender, EventArgs e)
        {
            string sourceFilePath = Form_Template.strURLImage + @"\"+ txtMaSP.Text + ".jpg";
            try
            {
                pictureEdit1.Image = new Bitmap(sourceFilePath);
            }
            catch (Exception)
            {
                pictureEdit1.Image = new Bitmap(Application.StartupPath + @"\logo\no_image.jpg");
            }
           
        }

        private void BtnCapNhatGiaBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phẩm trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            strMaSP = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "ID"));

            var dgrResult = XtraMessageBox.Show("Có muốn cập nhật bảng giá không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_GiaBanInsertFromSP", new
            {
                MaSP = strMaSP,
                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
            });
            if(data.errcode == 1)
            {
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ChkTatCa_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            option = chkTatCa.Checked ? 2 : 1;
            GetSanPham(option);
        }

        private void BtnXoaAnhSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sourceFilePath = Form_Template.strURLImage + @"\" + txtMaSP.Text + ".jpg";
            if (File.Exists(sourceFilePath))
            {
                pictureEdit1.Image?.Dispose();
                File.Delete(sourceFilePath);
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_SanPhamUpdateImage", new
                {
                    ID = Convert.ToInt32(lbl_ID.Text),
                    TenHinh = "",
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    XtraMessageBox.Show("Đã xóa ảnh sản phẩm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Xóa ảnh không thành công.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }               
            }
            else
            {
                XtraMessageBox.Show("Không tồn tại đường dẫn ảnh", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GrcSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.Edit = true;
            Form_Template.intIDSanPham = Convert.ToInt32(lbl_ID.Text);
            Form_ThemSanPham frm = new Form_ThemSanPham();
            frm.ShowDialog();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            txtTenSP.EditValue = gridView1.GetRowCellValue(i, "TenSanPham");
            txtQuyCach.EditValue = gridView1.GetRowCellValue(i, "QuyCach");
            txtLoaiSP.EditValue = gridView1.GetRowCellValue(i, "TenLoai");
            txtNhomSP.EditValue = gridView1.GetRowCellValue(i, "NhomSanPham");
        }

        private void BtnImportExcel2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}