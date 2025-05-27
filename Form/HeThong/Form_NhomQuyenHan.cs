using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_NhomQuyenHan : XtraForm
    {
        public Form_NhomQuyenHan()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 25
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;           
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nhomquyen")
            {
                GetNhomQuyen();
            }
        }

        public void GetNhomQuyen()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NhomQuyenSelect");
            grcNhomQuyen.DataSource = dt;
            lblID.DataBindings.Clear();
            lblID.DataBindings.Add("text", dt, "Id");
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetNhomQuyen();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show("Có muốn xóa nhóm quyền (" + gridView1.GetRowCellValue(i, "NhomQuyen") + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NhomQuyenDelete", new
            {
                ID = Convert.ToInt32(lblID.Text)
            });
            if (data.errcode == 1)
            {
                GetNhomQuyen();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDNhomQuyen = 0;
            Form_Template.Edit = false;
            Form_ThemQuyenHan frm = new Form_ThemQuyenHan();
            frm.ShowDialog();
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDNhomQuyen = Convert.ToInt32(lblID.Text);
            Form_Template.Edit = true;
            Form_ThemQuyenHan frm = new Form_ThemQuyenHan();
            frm.ShowDialog();
        }

        private void Form_NhomQuyenHan_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhomQuyen();
        }
    }
}