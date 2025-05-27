using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_Nhacungcap : XtraForm
    {
        public Form_Nhacungcap()
        {
            InitializeComponent();
            gvNCC.CustomDrawRowIndicator += (ss, ee) => { GridViewHelper.GridView_CustomDrawRowIndicator(ss, ee, grcNCC, gvNCC); };
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 9
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled= data.Excel;
            BtnIn.Enabled = data.InAn;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_ncc")
            {
                GetNhaCungCap();
            }
        }

        public async void GetNhaCungCap()
        {
            var y = gvNCC.FocusedRowHandle;
            var x = gvNCC.TopRowIndex;
            var lstNCC = await ExecSQL.ExecProcedureDataAsyncAsDataTable("sp_NhaCungCapSelect");
            grcNCC.DataSource = lstNCC;
            lblMaNCC.DataBindings.Clear();
            lblMaNCC.DataBindings.Add("text", lstNCC, "ID");
          
            gvNCC.FocusedRowHandle = y;
            gvNCC.TopRowIndex = x;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhaCungCap();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDMaNCC = 0;
            Form_ThemNhaCungCap frm = new Form_ThemNhaCungCap();
            frm.ShowDialog();
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = true;
            Form_Template.intIDMaNCC = Convert.ToInt32(lblMaNCC.Text);
            Form_ThemNhaCungCap frm = new Form_ThemNhaCungCap();
            frm.ShowDialog();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gvNCC.FocusedRowHandle;
            var dgr = XtraMessageBox.Show("Có muốn xóa tên nhà cung cấp (" + gvNCC.GetRowCellValue(i,"TenNhaCungCap") + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhaCungCapDeleteByID", new
            {
                ID = Convert.ToInt32(lblMaNCC.Text)
            });
            if (data.errcode==1)
            {
                GetNhaCungCap();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvNCC.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DanhSachNCC_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcNCC.ExportToXlsx(xtraSaveFileDialog1.FileName);         
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });

        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvNCC.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các nhà cung cấp trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gvNCC.GetSelectedRows();
            var joinIdx = string.Join(",", from r in selectedRows select gvNCC.GetRowCellValue(Convert.ToInt32(r), "ID"));

            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_NhaCungCapSelect", new {ID = joinIdx});
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 7;
            var frm = new Form_Report();
            frm.Show();
        }

        private void Form_Nhacungcap_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhaCungCap();
        }

        private void grcNCC_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.Edit = true;
            Form_Template.intIDMaNCC = Convert.ToInt32(lblMaNCC.Text);
            Form_ThemNhaCungCap frm = new Form_ThemNhaCungCap();
            frm.ShowDialog();
        }
    }
}