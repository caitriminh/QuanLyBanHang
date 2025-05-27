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
    public partial class Form_DoanhSoSales : XtraForm
    {
       
        public Form_DoanhSoSales()
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
            BtnIn.Enabled = data.InAn;           
        }

        public void GetBaoCaoDoanhThu(DateTime tungay, DateTime denngay)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstDoanhThu =  ExecSQL.ExecProcedureDataAsDataTable("sp_DoanhSoTheoNhanVienSale", new
            {               
                tungay,
                denngay
            });
            grcBaoCaoDoanhThu.DataSource = lstDoanhThu;
            if (lstDoanhThu.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lbl_MaNV.DataBindings.Clear();
            //lbl_LoaiPhieu.DataBindings.Clear();

            lbl_MaNV.DataBindings.Add("text", lstDoanhThu, "MaNVSale");
            //lbl_LoaiPhieu.DataBindings.Add("text", lstDoanhThu, "MaLoaiPhieu");

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetBaoCaoDoanhThu(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

      
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_DoanhSoTheoNhanVienSale", new
            {
                MaNVSale = lbl_MaNV.Text,
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 26;
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
            xtraSaveFileDialog1.FileName = "HoaHongNhanVien_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcBaoCaoDoanhThu.ExportToXlsx(xtraSaveFileDialog1.FileName);
            // Mở file với ứng dụng mặc định
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            GetBaoCaoDoanhThu(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
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
            GetBaoCaoDoanhThu(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void Form_DoanhSoSales_Load(object sender, EventArgs e)
        {           
            GetKy();
        }
    }
}