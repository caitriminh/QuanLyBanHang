using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_BaoCaoChiPhi : XtraForm
    {
       
        public Form_BaoCaoChiPhi()
        {
            InitializeComponent();
            dteThang.EditValue = DateTime.Now.Date.ToString("MM/yyyy");          
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 23
            });
            if (data == null) { return; }           
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;         
        }

        public void GetTongHopChiPhi()
        {
            var y = grvViewTongHopChiPhi.FocusedRowHandle;
            var x = grvViewTongHopChiPhi.TopRowIndex;
            var lstChiPhi = ExecSQL.ExecProcedureDataAsDataTable("sp_TongHopChiPhi", new
            {
                Thang = Convert.ToDateTime(dteThang.EditValue).Month,
                Nam = Convert.ToDateTime(dteThang.EditValue).Year
            });
            grcTongHopChiPhi.DataSource = lstChiPhi;
            
            grvViewTongHopChiPhi.FocusedRowHandle = y;
            grvViewTongHopChiPhi.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTongHopChiPhi();
        }

      
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvViewTongHopChiPhi.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "TongHopChiPhi_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcTongHopChiPhi.ExportToXlsx(xtraSaveFileDialog1.FileName);           
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTongHopChiPhi();
        }

         
        private void Form_BaoCaoChiPhi_Load(object sender, EventArgs e)
        {
            GetTongHopChiPhi();
        }

        private void BtnIn_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {  
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TongHopChiPhi", new
            {
                Thang = Convert.ToDateTime(dteThang.EditValue).Month,
                Nam = Convert.ToDateTime(dteThang.EditValue).Year
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 12;
            var frm = new Form_Report();
            frm.Show();
        }
    }
}