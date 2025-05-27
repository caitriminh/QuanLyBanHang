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
    public partial class Form_BaoCaoTienChiecKhau : XtraForm
    {
       
        public Form_BaoCaoTienChiecKhau()
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
                Menu = 22
            });
            if (data == null) { return; }           
            BtnExcel.Enabled = data.Excel;
            btnOpitonIn.Enabled = data.InAn;         
        }

        public void GetTienCK(DateTime tungay, DateTime denngay)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstDoanhThu =  ExecSQL.ExecProcedureDataAsDataTable("sp_TienChiecKhau", new
            {               
                tungay,
                denngay
            });
            grcBaoCaoTienCK.DataSource = lstDoanhThu;
            if (lstDoanhThu.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lbl_IdCK.DataBindings.Clear();
            lbl_MaKH.DataBindings.Clear();

            lbl_IdCK.DataBindings.Add("text", lstDoanhThu, "IdCK");
            lbl_MaKH.DataBindings.Add("text", lstDoanhThu, "MaKhachHang");

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTienCK(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

      
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TienChiecKhau", new
            {
                MaKH= lbl_MaKH.Text,
                IdCK = lbl_IdCK.Text,
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 18;
            var frm = new Form_Report();
            frm.Show();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BaoCaoTienChiecKhau_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcBaoCaoTienCK.ExportToXlsx(xtraSaveFileDialog1.FileName);
            // Mở file với ứng dụng mặc định
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTienCK(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
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
            GetTienCK(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void Form_BaoCaoTienChiecKhau_Load(object sender, EventArgs e)
        {
            GetKy();
        }

        private void btnInA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TienChiecKhau", new
            {
                MaKH = lbl_MaKH.Text,
                IdCK = lbl_IdCK.Text,
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 32;
            var frm = new Form_Report();
            frm.Show();
        }
    }
}