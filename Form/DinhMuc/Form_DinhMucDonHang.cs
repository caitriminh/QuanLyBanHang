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
    public partial class Form_DinhMucDonHang : XtraForm
    {
       
        public Form_DinhMucDonHang()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 39
            });
            if (data == null) { return; }
            btnIn2.Enabled = data.InAn;         
            BtnExcel.Enabled = data.Excel;                  
        }
       
        public void GetDinhMucDonHang(DateTime tungay, DateTime denngay)        
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            var lstDinhMuc =  ExecSQL.ExecProcedureDataAsDataTable("sp_DinhMucDonHang", new
            {
                tungay, denngay 
            });
            grcChiTietDinhMuc.DataSource = lstDinhMuc;
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDinhMucDonHang(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
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
            xtraSaveFileDialog1.FileName = "DinhMucDonHang_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcChiTietDinhMuc.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }
                      
        private void BtnIn2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            //Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_DinhMucSelectByMaSP", new
            //{
            //    masp = Convert.ToInt32(lbl_MaSanPham.Text),                
            //});
            //if (Form_Template.dtReport.Rows.Count == 0)
            //{
            //    XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //Form_Template.IntReport = 20;
            //var frm = new Form_Report();
            //frm.Show();
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        private void Form_DinhMucDonHang_Load(object sender, EventArgs e)
        {
            GetKy();
        }

        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetDinhMucDonHang(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void btnDatMuaHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phầm cần đặt hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinID = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "Id"));

            var dgrResult = XtraMessageBox.Show("Bạn có muốn đặt mua hàng không?","Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DonMuaHangDinhMuc", new
            {
                Id = joinID,
                mancc = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaNCC")),
                nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
            });
            if (data.errcode == 1) 
            {
                GetDinhMucDonHang(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}