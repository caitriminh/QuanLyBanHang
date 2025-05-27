using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_DanToc : XtraForm
    {
        public Form_DanToc()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 48
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;           
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_dantoc")
            {
                GetDanToc();
            }
        }

        public void GetDanToc()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_DanTocSelect");
            grcDanToc.DataSource = dt;
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
            GetDanToc();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show($"Có muốn xóa dân tộc ({gridView1.GetRowCellValue(i, "DanToc")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DanTocDeleteByID", new
            {
                ID = Convert.ToInt32(lblID.Text)
            });
            if (data.errcode == 1)
            {
                GetDanToc();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDDanToc = 0;
            Form_Template.Edit = false;
            Form_ThemDanToc frm = new Form_ThemDanToc();
            frm.ShowDialog();
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDPhongBan = Convert.ToInt32(lblID.Text);
            Form_Template.Edit = true;
            Form_ThemDanToc frm = new Form_ThemDanToc();
            frm.ShowDialog();
        }

        private void Form_DanToc_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetDanToc();
        }
    }
}