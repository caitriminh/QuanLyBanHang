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
    public partial class Form_GiaBan : XtraForm
    {

        public static string strIDGiaBan = "";
        int option = 1;
        public Form_GiaBan()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 16
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            btnImportExcel.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;           
            btnOptionExcel.Enabled = data.Excel;
            btnIn.Enabled = data.InAn;
        }

        /// <summary>
        /// Load danh sách hóa đơn khi có sự thay đổi form_themhoadon
        /// </summary>
        /// <param name="value"></param>
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_giaban")
            {
                GetGiaBan(option);
            }
        }

        public void GetGiaBan(int option)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstGiaBan =  ExecSQL.ExecProcedureDataAsDataTable("sp_GiaBanSelectAll", new
            {
                option
            });
            grcGiaBan.DataSource = lstGiaBan;
            lbl_ID.DataBindings.Clear();
            lbl_ID.DataBindings.Add("text", lstGiaBan, "ID");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetGiaBan(option);
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemGiaBan frm = new Form_ThemGiaBan();
            frm.ShowDialog();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa giá bán này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_GiaBanDelete", new
            {
                ID = lbl_ID.Text
            });
            if (data.errcode ==1)
            {
                GetGiaBan(option);
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDGiaBan =Convert.ToInt32(lbl_ID.Text);
            Form_Template.Edit = true;
            Form_ThemGiaBan frm = new Form_ThemGiaBan();
            frm.ShowDialog();
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
            xtraSaveFileDialog1.FileName = "GiaBan_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcGiaBan.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void Form_GiaBan_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetGiaBan(option);
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //var i = gridView1.FocusedRowHandle;
            //if (e.Column == colCopy)
            //{
            //    var dgr = XtraMessageBox.Show("Có muốn sao chép giá bán " + gridView1.GetRowCellValue(i,"TenSanPham") + " này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dgr != DialogResult.Yes) { return; }
            //    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_GiaBanCopy", new
            //    {
            //        ID = Convert.ToInt32(lbl_ID.Text)
            //    });
            //    if (data.errcode == 1)
            //    {
            //        GetGiaBan();
            //        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void grcGiaBan_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.intIDGiaBan = Convert.ToInt32(lbl_ID.Text);
            Form_Template.Edit = true;
            Form_ThemGiaBan frm = new Form_ThemGiaBan();
            frm.ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phẩm trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinID = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "ID"));

            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "MauImportGiaBan_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_GiaBanExportExcel", new { ID = joinID });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
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
                    DataTable dataTable = result.Tables[0]; 


                    foreach (DataRow row in dataTable.Rows)
                    {
                        string maSP = row["MaSP"].ToString();
                        decimal giaTruocThue = row["GiaTruocThue"] == null ? 0 :  Convert.ToDecimal(row["GiaTruocThue"]);
                        DateTime ngayApDung =Convert.ToDateTime(row["NgayApDung"]);
                        int vat =Convert.ToInt32( row["VAT"]);
                        decimal giaSauThue = row["GiaSauThue"] == null ? 0 : Convert.ToDecimal(row["GiaSauThue"]);

                        var data = ExecSQL.ExecProcedureNonData("sp_GiaBanInsert", new
                        {
                            MaSP = maSP,
                            GiaBan = giaTruocThue,
                            NgayApDung = ngayApDung,
                            VAT = vat,
                            GiaSauThue = giaSauThue,
                            Ghichu ="",
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                        });
                        dem += 1;
                    }
                }
            }
            GetGiaBan(option);
            XtraMessageBox.Show("Đã nhập được " + dem.ToString() + " dòng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraOpenFileDialog1.Filter = "Excel files |*.xlsx";
            xtraOpenFileDialog1.FileName = "Chọn file Excel";
            if (xtraOpenFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            ImportExcelDataToSQL(xtraOpenFileDialog1.FileName);
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phẩm trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinID = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "ID"));

            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_GiaBanExportExcel", new
            {
                ID = joinID
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 15;
            var frm = new Form_Report();
            frm.Show();
        }

        private void chkTatCa_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            option = chkTatCa.Checked ? 2 : 1;
            GetGiaBan(option);
        }

        private void btnBaoGiaKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phẩm trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            strIDGiaBan = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "ID"));
            Form_ThemBangBaoGia frm = new Form_ThemBangBaoGia();
            frm.Show();

        }
    }
}