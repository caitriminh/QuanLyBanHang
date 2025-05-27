using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_Phieuxuat_dieuchinh : XtraForm
    {
        int option = 1;
        public Form_Phieuxuat_dieuchinh()
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
                Menu = 15
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
            if (value.task == "them_phieuxuat")
            {
                GetHoaDon(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
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
                loaiphieu=Form_Template.strLoaiPhieu
            });
            grcPhieuXuat.DataSource = lstHoaDon;
            if (lstHoaDon.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lbl_MaHoaDon.DataBindings.Clear();
            lbl_MaHoaDon.DataBindings.Add("text", lstHoaDon, "MaHoaDon");
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
            Form_Template.strLoaiPhieu = "PXDC";
            Form_ThemPhieuXuat frm = new Form_ThemPhieuXuat();
            frm.Show();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
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
            Form_Template.strLoaiPhieu = "PXDC";
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

        private void grcPhieuXuat_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.strMaHoaDon = lbl_MaHoaDon.Text;
            Form_Template.Edit = true;
            Form_Template.strLoaiPhieu = "PXDC";
            Form_ThemPhieuXuat frm = new Form_ThemPhieuXuat();
            frm.ShowDialog();
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }


        private void Form_Phieuxuat_dieuchinh_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            option = 2;
            GetHoaDon(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }
    }
}