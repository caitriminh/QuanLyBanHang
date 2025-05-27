using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_QuyDoiGioVaoRa : XtraForm
    {
        public Form_QuyDoiGioVaoRa()
        {
            InitializeComponent();            
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_quydoi")
            {
                GetQuyDoi();
            }
        }

        public void GetQuyDoi()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_QuyDoiGioVaoRaSelect");
            grcQuyDoi.DataSource = dt;
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
            GetQuyDoi();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show($"Có muốn xóa thông tin này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_QuyDoiGioVaoRaDeleteByID", new
            {
                id = Convert.ToInt32(lblID.Text)
            });
            if (data.errcode == 1)
            {
                GetQuyDoi();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_ThemQuyDoi frm = new Form_ThemQuyDoi();
            frm.ShowDialog();
        }       

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = true;
            Form_Template.intIDQuyDoi = Convert.ToInt32(lblID.Text);
            Form_ThemQuyDoi frm = new Form_ThemQuyDoi();
            frm.ShowDialog();
        }

        private void Form_QuyDoiGioVaoRa_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetQuyDoi();
        }

        private void grcQuyDoi_DoubleClick(object sender, EventArgs e)
        {
            if(btnSua.Enabled == false) { return; }
            Form_Template.Edit = true;
            Form_Template.intIDQuyDoi = Convert.ToInt32(lblID.Text);
            Form_ThemQuyDoi frm = new Form_ThemQuyDoi();
            frm.ShowDialog();
        }
    }
}