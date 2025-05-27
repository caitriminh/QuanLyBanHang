using DevExpress.Charts.Native;
using DevExpress.XtraEditors;
using OfficeOpenXml;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_Phieuxuat : XtraForm
    {
        int option = 1;
        public Form_Phieuxuat()
        {
            InitializeComponent();
            dteTuNgay.EditValue = DateTime.Now.Date.ToString("01/MM/yyyy");
            dteDenNgay.EditValue = DateTime.Now.Date;
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 14
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            btnOptionExcel.Enabled = data.Excel;
            btnOptionIn.Enabled = data.InAn;
            colPhieuThu.Visible = data.Luu;
        }

        /// <summary>
        /// Load danh sách hóa đơn khi có sự thay đổi form_themhoadon
        /// </summary>
        /// <param name="value"></param>
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_phieuxuat" || value.task == "them_phieuthu")
            {
                GetHoaDon(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        private void Form_Phieuxuat_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        public void GetHoaDon(int option, DateTime tungay, DateTime denngay)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstHoaDon =  ExecSQL.ExecProcedureDataAsDataTable("sp_HoaDonSelectAll", new
            {
                option,
                tungay,
                denngay,
                loaiphieu = Form_Template.strLoaiPhieu
            });
            grcPhieuXuat.DataSource = lstHoaDon;
            if (lstHoaDon.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lbl_MaHoaDon.DataBindings.Clear();
            lbl_MaDonHang.DataBindings.Clear();

            lbl_MaHoaDon.DataBindings.Add("text", lstHoaDon, "MaHoaDon");
            lbl_MaDonHang.DataBindings.Add("text", lstHoaDon, "MaDonHang");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetHoaDon(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strLoaiPhieu = "XB";
            Form_ThemPhieuXuat frm = new Form_ThemPhieuXuat();
            frm.Show();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }         
            var lstCTHoaDon = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new
            {
                mahd = gridView1.GetRowCellValue(i, "MaHoaDon").ToString()
            });
            grcChiTietPhieuXuat.DataSource = lstCTHoaDon;
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa hóa đơn (" + lbl_MaHoaDon.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureSacalar("sp_HoaDonDelete", new
            {
                mahd = lbl_MaHoaDon.Text
            }); ;
            if (data.ToString() == "1")
            {
                GetHoaDon(1, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show("Xóa hóa đơn thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Xóa hóa đơn không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.strMaHoaDon = lbl_MaHoaDon.Text;
            Form_Template.Edit = true;
            Form_Template.strLoaiPhieu = "XB";
            Form_ThemPhieuXuat frm = new Form_ThemPhieuXuat();
            frm.ShowDialog();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_InHoaDonByMaHD", new
            {
                mahd = lbl_MaHoaDon.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 1;
            var frm = new Form_Report();
            frm.Show();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các hóa đơn trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinMaDH = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaHoaDon"));

            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuXuatKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new { mahd = joinMaDH });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void BtnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            option = 2;
            GetHoaDon(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void GrcPhieuXuat_DoubleClick(object sender, EventArgs e)
        {
            if(BtnSua.Enabled == false) return;
            Form_Template.strMaHoaDon = lbl_MaHoaDon.Text;
            Form_Template.Edit = true;
            Form_Template.strLoaiPhieu = "XB";
            Form_ThemPhieuXuat frm = new Form_ThemPhieuXuat();
            frm.ShowDialog();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if(e.Column == colPhieuThu)
            {
                Form_Template.strMaPhieuThu = "";
                Form_Template.intIDMaKH = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaKhachHang"));
                Form_Template.strMaHoaDon = lbl_MaHoaDon.Text;
                Form_ThemPhieuThu frm = new Form_ThemPhieuThu();
                frm.ShowDialog();
            }
        }

        private void CboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            option = 2;
            GetHoaDon(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnInA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_InHoaDonByMaHD", new
            {
                mahd = lbl_MaHoaDon.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 22;
            var frm = new Form_Report();
            frm.Show();
        }

        private void BtnCapNhatDonHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.strMaDonDatHang = lbl_MaDonHang.Text;
            Form_Template.Edit = true;
            Form_ThemDonDatHang frm = new Form_ThemDonDatHang();
            frm.ShowDialog();
        }

        private void BtnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string templatePath = Application.StartupPath + "\\config\\BkavEHD.xlsx";
           
            FileInfo fileInfo = new FileInfo(templatePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Get the first worksheet in the workbook
                if (package.Workbook.Worksheets.Count == 0)
                {
                    XtraMessageBox.Show("Không tồn tại file Excel.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Thông tin tạo Hóa đơn"];

                var selectedRows = gridView1.GetSelectedRows();
                var joinMaDH = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaHoaDon"));

                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new { mahd = joinMaDH });

                // Simulate your DataTable (replace with your actual DataTable)
            

                // Define the start row where data should be inserted
                int startRow = 6; // Assuming the first row is headers in Excel

                int stt = 1;
                // Loop through the DataTable and insert values into the Excel template
                foreach (DataRow row in dataTable.Rows)
                {
                    worksheet.Cells[startRow, 1].Value = stt;   
                    worksheet.Cells[startRow, 2].Value = row["MaHoaDon"];  
                    worksheet.Cells[startRow, 3].Value = row["TenSanPham"];
                    worksheet.Cells[startRow, 4].Value = row["DonViTinh"];
                    worksheet.Cells[startRow, 5].Value = Convert.ToInt32(row["Soluong"]);
                    worksheet.Cells[startRow, 6].Value = Convert.ToDecimal(row["GiaTruocThue2"]);
                    worksheet.Cells[startRow, 7].Value = Convert.ToInt32(row["ThanhTien2"]);
                    worksheet.Cells[startRow, 8].Value = Convert.ToDecimal(row["ThueSuat"]);
                    worksheet.Cells[startRow, 9].Value = Convert.ToInt32(row["TienVAT2"]);
                    worksheet.Cells[startRow, 10].Value = Convert.ToDateTime( row["NgayLap"]).ToString("dd/MM/yyyy");
                    worksheet.Cells[startRow, 11].Value = ""; //Họ tên người mua
                    worksheet.Cells[startRow, 12].Value = row["TenKhachHang"];
                    worksheet.Cells[startRow, 13].Value = row["MaSoThue"];
                    worksheet.Cells[startRow, 14].Value = row["DiaChi"];
                    worksheet.Cells[startRow, 15].Value = "";//Số tài khoản
                    worksheet.Cells[startRow, 16].Value = "TM/CK";                   
                    startRow++;
                    stt++;
                }

                //string newFilePath = Application.StartupPath + "\\config\\HoaDon\\BkavEHD" + DateTime.Now.ToString("ddMMyyyhHHmmss") + ".xlsx";

                xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
                xtraSaveFileDialog1.FileName = "HoaDon" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                FileInfo newFile = new FileInfo(xtraSaveFileDialog1.FileName);
                package.SaveAs(newFile);
                Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
            }

        }

        private void BtnExportExcelHD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string templatePath = Application.StartupPath + "\\config\\BkavEHDv2.xlsx";

            FileInfo fileInfo = new FileInfo(templatePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Get the first worksheet in the workbook
                if (package.Workbook.Worksheets.Count == 0)
                {
                    XtraMessageBox.Show("Không tồn tại file Excel.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Thông tin tạo Hóa đơn"];

                var selectedRows = gridView1.GetSelectedRows();
                var joinMaDH = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaHoaDon"));

                var dataTable = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new { mahd = joinMaDH });

                // Simulate your DataTable (replace with your actual DataTable)


                // Define the start row where data should be inserted
                int startRow = 6; // Assuming the first row is headers in Excel

                int stt = 1;
                // Loop through the DataTable and insert values into the Excel template
                foreach (DataRow row in dataTable.Rows)
                {
                    worksheet.Cells[startRow, 1].Value = stt;
                    worksheet.Cells[startRow, 2].Value = row["madh2"];
                    worksheet.Cells[startRow, 3].Value = row["TenSanPham2"];
                    worksheet.Cells[startRow, 4].Value = row["DonViTinh"];
                    worksheet.Cells[startRow, 5].Value = Convert.ToInt32(row["Soluong"]);
                    worksheet.Cells[startRow, 6].Value = Convert.ToDecimal(row["GiaTruocThue2"]);
                    worksheet.Cells[startRow, 7].Value = Convert.ToInt32(row["ThanhTien2"]);
                    worksheet.Cells[startRow, 8].Value = Convert.ToDecimal(row["ThueSuat"]);
                    worksheet.Cells[startRow, 9].Value = Convert.ToInt32(row["TienVAT2"]);
                    worksheet.Cells[startRow, 10].Value = Convert.ToDateTime(DateTime.Now.Date).ToString("dd/MM/yyyy");
                    worksheet.Cells[startRow, 11].Value = ""; //Họ tên người mua
                    worksheet.Cells[startRow, 12].Value = row["TenKhachHang"];
                    worksheet.Cells[startRow, 13].Value = row["MaSoThue"];
                    worksheet.Cells[startRow, 14].Value = row["DiaChi"];
                    worksheet.Cells[startRow, 15].Value = "";//Số tài khoản
                    worksheet.Cells[startRow, 16].Value = "TM/CK";
                    startRow++;
                    stt++;
                }

                //string newFilePath = Application.StartupPath + "\\config\\HoaDon\\BkavEHD" + DateTime.Now.ToString("ddMMyyyhHHmmss") + ".xlsx";

                xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
                xtraSaveFileDialog1.FileName = "HoaDon" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                FileInfo newFile = new FileInfo(xtraSaveFileDialog1.FileName);
                package.SaveAs(newFile);
                Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
            }
        }


        private void BtnUpdatePayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các hóa đơn trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            Form_Template.strMaHoaDon = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaHoaDon"));
            Form_Template.tungay = Convert.ToDateTime(dteTuNgay.EditValue);
            Form_Template.denngay = Convert.ToDateTime(dteDenNgay.EditValue);

            Form_UpdateThanhToan frm = new Form_UpdateThanhToan();
            frm.ShowDialog();
        }
    }
}