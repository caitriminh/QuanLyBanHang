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
    public partial class Form_GiaBanNCC : XtraForm
    {

        public static string strIDGiaBan = "";
        int option = 1;
        public Form_GiaBanNCC()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 42
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;         
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;         
            btnIn.Enabled = data.InAn;
            colCopy.Visible = data.Luu;
        }

        /// <summary>
        /// Load danh sách hóa đơn khi có sự thay đổi form_themhoadon
        /// </summary>
        /// <param name="value"></param>
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_giaban_ncc")
            {
                GetGiaBan(option);
            }
        }

        public void GetGiaBan(int option)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstGiaBan =  ExecSQL.ExecProcedureDataAsDataTable("sp_GiaBanNCCSelectAll", new
            {
                option
            });
            grcGiaBan.DataSource = lstGiaBan;
            lbl_ID.DataBindings.Clear();
            lbl_ID.DataBindings.Add("text", lstGiaBan, "ID");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetGiaBan(option);
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemGiaBanNCC frm = new Form_ThemGiaBanNCC();
            frm.ShowDialog();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa giá bán này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_GiaBanNCCDelete", new
            {
                ID = lbl_ID.Text
            });
            if (data.errcode ==1)
            {
                GetGiaBan(option);
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lbl_ID.Text.Length >0)
            {
                Form_Template.intIDGiaBanNCC = Convert.ToInt32(lbl_ID.Text);
                Form_Template.Edit = true;
                Form_ThemGiaBanNCC frm = new Form_ThemGiaBanNCC();
                frm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            xtraSaveFileDialog1.FileName = "GiaBanNCC_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcGiaBan.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void Form_GiaBan_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetGiaBan(option);
        }

        private void grcGiaBan_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.intIDGiaBanNCC = Convert.ToInt32(lbl_ID.Text);
            Form_Template.Edit = true;
            Form_ThemGiaBanNCC frm = new Form_ThemGiaBanNCC();
            frm.ShowDialog();
        }
       
        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các sản phẩm trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinID = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "ID"));

            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_GiaBanNCCSelectAll", new
            {
                option = 1,
                ID = joinID
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 25;
            var frm = new Form_Report();
            frm.Show();
        }

        private void chkTatCa_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            option = chkTatCa.Checked ? 2 : 1;
            GetGiaBan(option);
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colCopy)
            {
                var dgrResult = XtraMessageBox.Show("Bạn có muốn sao chép đơn giá nguyên liệu này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_GiaBanNCCCopy", new
                {
                    Id = Convert.ToInt32(lbl_ID.Text),
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if(data.errcode == 1)
                {
                    GetGiaBan(option);
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}