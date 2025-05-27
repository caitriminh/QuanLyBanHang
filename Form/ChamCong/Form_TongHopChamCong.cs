using DevExpress.XtraEditors;
using System;
using VisualStudio2019.Class;
using QuanLyBanHang.Class;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_TongHopChamCong : XtraForm
    {
       
        public Form_TongHopChamCong()
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
            BtnExcel.Enabled = data.Excel;           
        }

        private void GetTongHopChamCong(DateTime tungay, DateTime denngay)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_TongHopChamCong", new
            {               
                tungay,
                denngay
            });
            grcTongHopChamCong.DataSource = data;
           
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }
               
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTongHopChamCong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
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
            GetTongHopChamCong(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }             
      

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "TongHopChamCong_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcTongHopChamCong.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

      
        private void Form_TongHopChamCong_Load(object sender, EventArgs e)
        {
            GetKy();
        }
    }

}
