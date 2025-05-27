using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;
using DevExpress.XtraGrid.Views.Grid;

namespace VisualStudio2019.Form
{
    public partial class Form_ThongKeBanHang : XtraForm
    {
       
        public Form_ThongKeBanHang()
        {
            InitializeComponent();
            gvThongKeBanHang.CustomDrawRowIndicator += (ss, ee) => { GridViewHelper.GridView_CustomDrawRowIndicator(ss, ee, grcThongKeBanHang, gvThongKeBanHang); };
            dteTuNgay.EditValue = DateTime.Now.Date.ToString("01/MM/yyyy");
            dteDenNgay.EditValue = DateTime.Now.Date;
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 34
            });
            if (data == null) { return; }           
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;         
        }

        public void GetTienCK(DateTime tungay, DateTime denngay)
        {
            var y = gvThongKeBanHang.FocusedRowHandle;
            var x = gvThongKeBanHang.TopRowIndex;
            var lstDoanhThu =  ExecSQL.ExecProcedureDataAsDataTable("sp_ThongKeBanHang", new
            {               
                tungay,
                denngay
            });
            grcThongKeBanHang.DataSource = lstDoanhThu;
            if (lstDoanhThu.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }           
            lbl_MaKH.DataBindings.Clear();

            lbl_MaKH.DataBindings.Add("text", lstDoanhThu, "MaKhachHang");

            gvThongKeBanHang.FocusedRowHandle = y;
            gvThongKeBanHang.TopRowIndex = x;
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

            if (gvThongKeBanHang.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các chi tiết sản phẩm danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRows = gvThongKeBanHang.GetSelectedRows();
            var joinId = string.Join(",", from r in selectedRows select gvThongKeBanHang.GetRowCellValue(Convert.ToInt32(r), "Id"));

            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ThongKeBanHang", new
            {                
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue),
                Id = joinId
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 19;
            Form_Template.tungay = Convert.ToDateTime(dteTuNgay.EditValue);
            Form_Template.denngay = Convert.ToDateTime(dteDenNgay.EditValue);
           var frm = new Form_Report();
            frm.Show();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvThongKeBanHang.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "ThongKeBanHang_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcThongKeBanHang.ExportToXlsx(xtraSaveFileDialog1.FileName);
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

        private void Form_ThongKeBanHang_Load(object sender, EventArgs e)
        {
            GetKy();
        }
    }
}