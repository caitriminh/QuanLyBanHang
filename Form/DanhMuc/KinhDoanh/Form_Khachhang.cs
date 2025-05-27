using DevExpress.XtraEditors;
using ExcelDataReader;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_Khachhang : XtraForm
    {
        public Form_Khachhang()
        {
            InitializeComponent();
            gvKhachHang.CustomDrawRowIndicator += (ss, ee) => { GridViewHelper.GridView_CustomDrawRowIndicator(ss, ee, grcKhachHang, gvKhachHang); };
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 10
            });
            if (data == null) { return; }
            btnOptionThem.Enabled = data.Luu;           
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            btnOptionExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_khachhang")
            {
                GetKhachHang();
            }
        }

        private void Form_Khachhang_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKhachHang();
        }

        public async void GetKhachHang()
        {
            var y = gvKhachHang.FocusedRowHandle;
            var x = gvKhachHang.TopRowIndex;
            var lstKhachHang = await ExecSQL.ExecProcedureDataAsyncAsDataTable("sp_KhachHangSelect");
            grcKhachHang.DataSource = lstKhachHang;
            lblMaKH.DataBindings.Clear();
            lblMaKH.DataBindings.Add("text", lstKhachHang, "ID");
          
            gvKhachHang.FocusedRowHandle = y;
            gvKhachHang.TopRowIndex = x;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {          
            GetKhachHang();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemKhachhang frm = new Form_ThemKhachhang();
            frm.ShowDialog();
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = true;
            Form_Template.intIDMaKH = Convert.ToInt32(lblMaKH.Text);
            Form_ThemKhachhang frm = new Form_ThemKhachhang();
            frm.ShowDialog();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gvKhachHang.FocusedRowHandle;
            var dgr = XtraMessageBox.Show("Có muốn xóa tên khách hàng (" + gvKhachHang.GetRowCellValue(i,"TenKhachHang") + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureSacalar("sp_KhachHangDeleteByID", new
            {
                ID = Convert.ToInt32(lblMaKH.Text)
            });
            if (data.ToString() == "1")
            {               
                GetKhachHang();
                XtraMessageBox.Show("Xóa khách hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Xóa khách hàng không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvKhachHang.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DanhSachKhachHang_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcKhachHang.ExportToXlsx(xtraSaveFileDialog1.FileName);         
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });

        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvKhachHang.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các hóa đơn trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gvKhachHang.GetSelectedRows();
            var joinIdx = string.Join(",", from r in selectedRows select gvKhachHang.GetRowCellValue(Convert.ToInt32(r), "ID"));

            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect", new {ID = joinIdx});
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 2;
            var frm = new Form_Report();
            frm.Show();

        }

        private void grcKhachHang_DoubleClick(object sender, EventArgs e)
        {
            Form_Template.Edit = true;
            Form_Template.intIDMaKH = Convert.ToInt32(lblMaKH.Text);
            Form_ThemKhachhang frm = new Form_ThemKhachhang();
            frm.ShowDialog();
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
                        string TenKH = row["TenKhachHang"].ToString();
                        string DiaChi = row["DiaChi"].ToString();
                        string DienThoai1 = row["DienThoai1"].ToString();
                        string DienThoai2 = row["DienThoai2"].ToString();
                        string EmailKH = row["EmailKH"].ToString();
                        string MST = row["MaSoThue"].ToString();
                        string ghichu = row["Ghichu"].ToString();
                        string TenNhom = row["TenNhom"].ToString();
                        var IDNhomKH = ExecSQL.ExecProcedureSacalar("sp_NhomKhachHangSelectByTenNhom", new
                        {
                            TenNhom = TenNhom
                        });
                        //Cập nhật data
                        var data = ExecSQL.ExecProcedureNonData("sp_CreateKhachHang", new
                        {                            
                            TenKhachHang = TenKH,
                            DiaChi = DiaChi,
                            DienThoai1= DienThoai1,
                            DienThoai2= DienThoai2,
                            EmailKH= EmailKH,
                            MaSoThue= MST,
                            Nhomkhachhang = Convert.ToInt32(IDNhomKH.ToString()),
                            GhiChu = ghichu,
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                        });
                        dem += 1;
                    }
                }
            }
            GetKhachHang();
            XtraMessageBox.Show("Đã nhập được " + dem.ToString() + " dòng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNhapTuExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraOpenFileDialog1.Filter = "Excel files |*.xlsx";
            xtraOpenFileDialog1.FileName = "Chọn file Excel";
            if (xtraOpenFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            ImportExcelDataToSQL(xtraOpenFileDialog1.FileName);
        }

        private void btnXuatMauExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvKhachHang.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các khách hàng danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gvKhachHang.GetSelectedRows();
            var joinID = string.Join(",", from r in selectedRows select gvKhachHang.GetRowCellValue(Convert.ToInt32(r), "ID"));

            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "MauImportKhachHang_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangExportExcel", new { ID = joinID });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }
    }
}