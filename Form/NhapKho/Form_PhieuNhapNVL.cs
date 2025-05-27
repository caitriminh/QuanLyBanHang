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
    public partial class Form_PhieuNhapNVL : XtraForm
    {
        int option = 1;
        public Form_PhieuNhapNVL()
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
                Menu = 41
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnOptionIn.Enabled = data.InAn;
            colPhieuChi.Visible = data.Luu;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_phieunhap_nvl" || value.task == "them_phieuchi")
            {
                GetPhieuNhap(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
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

        public void GetPhieuNhap(int option, DateTime tungay, DateTime denngay)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstPhieuNhap =  ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuNhapMuaHangSelectAll", new
            {
                option,
                tungay,
                denngay,
                loaiphieu = Form_Template.strLoaiPhieu
            });
            grcPhieuNhap.DataSource = lstPhieuNhap;
            if (lstPhieuNhap.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lbl_MaPhieuNhap.DataBindings.Clear();
            lbl_MaPhieuNhap.DataBindings.Add("text", lstPhieuNhap, "MaPhieuNhap");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhieuNhap(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strLoaiPhieu = "NVL";
            Form_ThemPhieuNhapNVL frm = new Form_ThemPhieuNhapNVL();
            frm.ShowDialog();
        }

       
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }         
            var lstCTPhieuNhap = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietPhieuNhapByMaHD", new
            {
                mapn = gridView1.GetRowCellValue(i, "MaPhieuNhap").ToString()
            });
            grcChiTietPhieuNhap.DataSource = lstCTPhieuNhap;
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa phiếu nhập (" + lbl_MaPhieuNhap.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhieuNhapDelete", new
            {
                mapn = lbl_MaPhieuNhap.Text
            });
            if (data.errcode==1)
            {
                GetPhieuNhap(1, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.strMaPhieuNhap = lbl_MaPhieuNhap.Text;
            Form_Template.Edit = true;
            Form_Template.strLoaiPhieu = "NVL";
            Form_ThemPhieuNhapNVL frm = new Form_ThemPhieuNhapNVL();
            frm.ShowDialog();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_InPhieuNhapByMaHD", new
            {
                mapn = lbl_MaPhieuNhap.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 5;
            var frm = new Form_Report();
            frm.Show();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các phiếu nhập trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinMaPN = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaPhieuNhap"));

            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuNhapKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietPhieuNhapByMaHD", new { mapn = joinMaPN });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            GetPhieuNhap(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void grcPhieuNhap_DoubleClick(object sender, EventArgs e)
        {
            if(BtnSua.Enabled == false) { return; }
            Form_Template.strMaPhieuNhap = lbl_MaPhieuNhap.Text;
            Form_Template.Edit = true;
            Form_Template.strLoaiPhieu = "NVL";
            Form_ThemPhieuNhapNVL frm = new Form_ThemPhieuNhapNVL();
            frm.ShowDialog();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
             var i = gridView1.FocusedRowHandle;
            if(e.Column == colPhieuChi)
            {
                Form_Template.strMaPhieuThu = "";
                Form_Template.intIDMaNCC = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaNhaCungCap"));
                Form_Template.strMaPhieuNhap = lbl_MaPhieuNhap.Text;
                Form_ThemPhieuChi frm = new Form_ThemPhieuChi();
                frm.ShowDialog();
            }
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetPhieuNhap(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void btnPrintA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_InPhieuNhapByMaHD", new
            {
                mapn = lbl_MaPhieuNhap.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 34;
            var frm = new Form_Report();
            frm.Show();
        }
    }
}