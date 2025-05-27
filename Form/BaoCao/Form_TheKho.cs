using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_TheKho : XtraForm
    {
        
        public Form_TheKho()
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
                Menu = 20
            });
            if (data == null) { return; }
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;
        }

        public void GetTenSP()
        {
            var lstSP = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelect");
            cboTenSanPham.Properties.DataSource = lstSP;
            cboTenSanPham.Properties.DisplayMember = "TenSanPham";
            cboTenSanPham.Properties.ValueMember = "ID";
        }

        public void GetTheKho()
        {
            if (cboTenSanPham.EditValue==null)
            {
                XtraMessageBox.Show("Vui lòng chọn tên sản phẩm.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenSanPham.Focus();
                return;
            }
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstTonKho =  ExecSQL.ExecProcedureDataAsDataTable("sp_TheKho", new
            {                
                tungay=Convert.ToDateTime(dteTuNgay.EditValue),
                denngay=Convert.ToDateTime(dteDenNgay.EditValue),
                masp=Convert.ToInt32(cboTenSanPham.EditValue)
                
            });
            grcTheKho.DataSource = lstTonKho;          
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTheKho();
        }
               
      
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TonKhoSelectAll", new
            {
                Ky = Convert.ToDateTime(dteTuNgay.EditValue).ToString("yyyyMM01")
            }); 
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 6;
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
            xtraSaveFileDialog1.FileName = "TheKho_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcTheKho.ExportToXlsx(xtraSaveFileDialog1.FileName);            
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });           
        }

        private void Form_TheKho_Load(object sender, EventArgs e)
        {
            GetTenSP();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            GetTheKho();
        }
    }
}