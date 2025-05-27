using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThietLapNgayNghi : XtraForm
    {
        public Form_ThietLapNgayNghi()
        {
            InitializeComponent();            
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_ngaynghi")
            {
                GetNgayNghi();
            }
        }

        public void GetNgayNghi()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NgayNghiSelect");
            grcNgayNghi.DataSource = dt;
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
            GetNgayNghi();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show($"Có muốn xóa ngày nghỉ này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NgayNghiDeleteByID", new
            {
                id = Convert.ToInt32(lblID.Text)
            });
            if (data.errcode == 1)
            {
                GetNgayNghi();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {    
            Form_ThemTT_NgayNghi frm = new Form_ThemTT_NgayNghi();
            frm.ShowDialog();
        }

        private void Form_ThietLapNgayNghi_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNgayNghi();
        }
    }
}