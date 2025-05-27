using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_NhanVienNghiViec : XtraForm
    {
        public Form_NhanVienNghiViec()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 57
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;
        }

        private void GetNhanVienNghiViec()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienNghiViecSelectAll");
            grcNhanVienNghiViec.DataSource = data;
            lbl_MaNV.DataBindings.Clear();
            lbl_MaNV.DataBindings.Add("text", data, "MaNV");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nhanvien_nghiviec")
            {
                GetNhanVienNghiViec();
            }
        }
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDNhanVien = Convert.ToInt32(lbl_MaNV.Text);
            Form_Template.Edit = true;
            Form_ThemNVNghiViec frm = new Form_ThemNVNghiViec();
            frm.ShowDialog();
        }
        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show($"Có muốn xóa nhân viên nghỉ việc {gridView1.GetRowCellValue(i,"TenNhanVien")} này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhanVienNghiViecDelete", new
            {
                MaNV= Convert.ToInt32(lbl_MaNV.Text)
            });
            if (data.errcode == 1)
            {
                GetNhanVienNghiViec();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhanVienNghiViec();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDNhanVien = 0;
            Form_Template.Edit = false;
            Form_ThemNVNghiViec frm = new Form_ThemNVNghiViec();
            frm.ShowDialog();
        }

        private void Form_NhanVienNghiViec_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhanVienNghiViec();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "NhanVienNghiViec_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcNhanVienNghiViec.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienNghiViecSelectAll");
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 27;
            var frm = new Form_Report();
            frm.Show();
        }
    }
    
}
