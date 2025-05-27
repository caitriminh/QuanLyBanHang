using DevExpress.XtraEditors;
using System;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_ChamCong : XtraForm
    {
       
        public Form_ChamCong()
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
            if (data == null) { return; }              
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;           
        }

        private void GetChamCong(DateTime tungay, DateTime denngay)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_ChamCongSelectAll", new
            {
                tungay,
                denngay
            });
            grcChamCong.DataSource = data;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", data, "Id");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_chamcong")
            {
                GetChamCong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDChamCong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemChamCong frm = new Form_ThemChamCong();
            frm.ShowDialog();
        }
       
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetChamCong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }
      
        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetChamCong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }
              
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemChamCong frm = new Form_ThemChamCong();
            frm.ShowDialog();
        }
           

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "NhatKyChamCong_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcChamCong.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void Form_ChamCong_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        private void grcChamCong_DoubleClick(object sender, EventArgs e)
        {
            if(BtnSua.Enabled == false) { return; }
            Form_Template.intIDChamCong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemChamCong frm = new Form_ThemChamCong();
            frm.ShowDialog();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) return;
            var dgr = XtraMessageBox.Show($"Bạn có muốn chấm công của nhân viên ({gridView1.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) return;
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChamCongDeleteById", new
            {
                Id = Convert.ToInt32(lbl_Id.Text)
            });
            if (data.errcode == 1)
            {
                GetChamCong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThietLapNgayNghi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_ThietLapNgayNghi frm = new Form_ThietLapNgayNghi();
            frm.ShowDialog();
        }

        private void btnQuyDoiChamCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_QuyDoiGioVaoRa frm = new Form_QuyDoiGioVaoRa();
            frm.ShowDialog();
        }

        private void btnCauHinhThamSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_CauHinhThamSo frm = new Form_CauHinhThamSo();
            frm.ShowDialog();
        }
    }

}
