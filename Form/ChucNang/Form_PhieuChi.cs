using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_PhieuChi : XtraForm
    {
       
        public Form_PhieuChi()
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
                Menu = 18
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnOptionIn.Enabled = data.InAn;
        }

         private void OnNext(MessageBroker value)
        {
            if (value.task == "them_phieuchi")
            {
                GetPhieuChi();
            }
        }

        public void GetPhieuChi()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstPhieuChi =  ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuChiSelect", new
            {
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            grcPhieuChi.DataSource = lstPhieuChi;
            if (lstPhieuChi.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lbl_MaPhieu.DataBindings.Clear();
            lbl_MaPhieu.DataBindings.Add("text", lstPhieuChi, "MaPhieu");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhieuChi();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strMaPhieuThu = "";
            Form_ThemPhieuChi frm = new Form_ThemPhieuChi();
            frm.ShowDialog();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show($"Có muốn xóa phiếu chi ({lbl_MaPhieu.Text}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhieuChiDelete", new
            {
                MaPhieu = lbl_MaPhieu.Text
            });
            if (data.errcode == 1)
            {
                GetPhieuChi();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.strMaPhieuThu = lbl_MaPhieu.Text;
            Form_Template.Edit = true;
            Form_ThemPhieuChi frm = new Form_ThemPhieuChi();
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
            xtraSaveFileDialog1.FileName = "PhieuChi_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcPhieuChi.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        private void Form_PhieuThu_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();          
        }

        private void grcPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.strMaPhieuThu = lbl_MaPhieu.Text;
            Form_Template.Edit = true;
            Form_ThemPhieuChi frm = new Form_ThemPhieuChi();
            frm.ShowDialog();
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuChiByMaPhieu", new
            {
                MaPhieu = lbl_MaPhieu.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 10;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhieuChi();
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetPhieuChi();
        }

        private void btnPrintA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuChiByMaPhieu", new
            {
                MaPhieu = lbl_MaPhieu.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 35;
            var frm = new Form_Report();
            frm.Show();
        }

        private void rptChiTietPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các phiếu chi trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRows = gridView1.GetSelectedRows();
            var joinMaPhieu = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaPhieu"));

            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_PhieuChiReport", new
            {
                MaPhieu = joinMaPhieu
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 41;
            var frm = new Form_Report();
            frm.Show();
        }
    }
}