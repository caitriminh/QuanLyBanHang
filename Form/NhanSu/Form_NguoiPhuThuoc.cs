using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_NguoiPhuThuoc : XtraForm
    {
        public Form_NguoiPhuThuoc()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 58
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;
        }

        private void GetNguoiPhuThuoc()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_NguoiPhuThuocSelectAll");
            grcNguoiPhuThuoc.DataSource = data;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", data, "Id");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nguoiphuthuoc")
            {
                GetNguoiPhuThuoc();
            }
        }
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDNguoiPhuThuoc = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemNguoiPhuThuoc frm = new Form_ThemNguoiPhuThuoc();
            frm.ShowDialog();
        }
        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show($"Có muốn xóa người phụ thuộc {gridView1.GetRowCellValue(i,"NguoiPhuThuoc")} này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NguoiPhuThuocDelete", new
            {
                MaNV= Convert.ToInt32(lbl_Id.Text)
            });
            if (data.errcode == 1)
            {
                GetNguoiPhuThuoc();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNguoiPhuThuoc();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDNguoiPhuThuoc = 0;
            Form_Template.Edit = false;
            Form_ThemNguoiPhuThuoc frm = new Form_ThemNguoiPhuThuoc();
            frm.ShowDialog();
        }

        private void Form_NguoiPhuThuoc_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNguoiPhuThuoc();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "NguoiPhuThuoc_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcNguoiPhuThuoc.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }
    }
    
}
