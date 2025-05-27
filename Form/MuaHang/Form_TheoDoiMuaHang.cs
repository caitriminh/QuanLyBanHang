using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_TheoDoiMuaHang : XtraForm
    {
       
        public Form_TheoDoiMuaHang()
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
                Menu = 44
            });
            if (data == null) { return; }           
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;           
        }      
        
        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        public void GetTheoDoiDonHang(DateTime tungay, DateTime denngay)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstCTDonDatHang =  ExecSQL.ExecProcedureDataAsDataTable("sp_TheoDoiDonHang", new
            {              
                tungay,
                denngay
            });
            grcTheoDoiDonHang.DataSource = lstCTDonDatHang;
            if (lstCTDonDatHang.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //lbl_MaDonHang.DataBindings.Clear();
            //lbl_MaDonHang.DataBindings.Add("text", lstCTDonDatHang, "MaDonHang");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTheoDoiDonHang(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }
             
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các chi tiết trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinId = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "Id"));

            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TheoDoiDonHang", new
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
            Form_Template.IntReport = 24;
            var frm = new Form_Report();
            frm.Show();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các chi tiết trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinMaDH = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaDonHang"));

            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DonMuaHang_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonMuaHang", new { madh = joinMaDH });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTheoDoiDonHang(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetTheoDoiDonHang(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

      
        private void Form_TheoDoiMuaHang_Load(object sender, EventArgs e)
        {
            GetKy();
        }
    }
}