using DevExpress.XtraEditors;
using System;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;

namespace VisualStudio2019.Form
{
    public partial class Form_UngLuong : XtraForm
    {
        public Form_UngLuong()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 69
            });
            if (data == null) { return; }
            btnThem.Enabled = data.Luu;  
            BtnSua.Enabled = data.Sua;
            btnXoa.Enabled = data.Xoa;
            BtnExcel.Enabled = data.Excel;           
        }
               

        private void GetUngLuong(DateTime tungay, DateTime denngay)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_UngLuongSelectAll", new
            {
                tungay,
                denngay
            });
            grcUngLuong.DataSource = data;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", data, "Id");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_ungluong")
            {
                GetUngLuong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.intIDUngLuong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemUngLuong frm = new Form_ThemUngLuong();
            frm.ShowDialog();
        }
       
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetUngLuong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
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
            xtraSaveFileDialog1.FileName = "UngLuong_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcUngLuong.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDUngLuong = 0;
            Form_ThemUngLuong frm = new Form_ThemUngLuong();
            frm.ShowDialog();
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i == -1) return;
            var dgr = XtraMessageBox.Show($"Có muốn xóa phiếu nhập ({gridView1.GetRowCellValue(i,"TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_UngLuongDelete", new
            {
               Id = Convert.ToInt32(lbl_Id.Text)
            });
            if (data.errcode == 1)
            {
                GetUngLuong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetUngLuong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void grcUngLuong_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.intIDUngLuong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemUngLuong frm = new Form_ThemUngLuong();
            frm.ShowDialog();
        }

        private void Form_UngLuong_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }
    }
    
}
