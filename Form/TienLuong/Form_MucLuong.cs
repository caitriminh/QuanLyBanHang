using DevExpress.XtraEditors;
using System;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Windows.Forms;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_MucLuong : XtraForm
    {
        public Form_MucLuong()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 67
            });
            if (data == null) { return; }
            btnThem.Enabled = data.Luu;  
            BtnSua.Enabled = data.Sua;
            btnXoa.Enabled = data.Xoa;
            BtnExcel.Enabled = data.Excel;           
        }
               

        private void GetMucLuong()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_MucLuongSelectAll");
            grcMucLuong.DataSource = data;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", data, "Id");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_mucluong")
            {
                GetMucLuong();
            }
        }
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDMucLuong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemMucLuong frm = new Form_ThemMucLuong();
            frm.ShowDialog();
        }
       
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetMucLuong();
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
            xtraSaveFileDialog1.FileName = "MucLuong_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcMucLuong.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDMucLuong = 0;
            Form_ThemMucLuong frm = new Form_ThemMucLuong();
            frm.ShowDialog();
        }

        private void Form_MucLuong_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetMucLuong();
        }

        private void grcMucLuong_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.intIDMucLuong = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemMucLuong frm = new Form_ThemMucLuong();
            frm.ShowDialog();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i == -1) return;
            var dgr = XtraMessageBox.Show($"Có muốn xóa phiếu nhập ({gridView1.GetRowCellValue(i,"TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_MucLuongDelete", new
            {
               Id = Convert.ToInt32(lbl_Id.Text)
            });
            if (data.errcode == 1)
            {
                GetMucLuong();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    
}
