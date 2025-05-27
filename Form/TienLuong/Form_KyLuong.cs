using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_KyLuong : XtraForm
    {
        public Form_KyLuong()
        {
            InitializeComponent();            
        }
      
        public void GetKyLuong()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_KyLuongSelectAll");
            grcKyLuong.DataSource = dt;
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
            GetKyLuong();
        }        

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lblID.Focus();
            int dem = 0;
            for (var i = 0; i <= gridView1.RowCount; i++)
            {
                var dr = gridView1.GetDataRow(Convert.ToInt32(i));
                if (ReferenceEquals(dr, null))
                {
                    break;
                }
                switch (dr.RowState)
                {
                    case DataRowState.Modified:
                        dem += 1;
                        ExecSQL.ExecProcedureNonData("sp_KyLuongUpdate", new
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            SoCongChuan = Convert.ToDecimal(dr["SoCongChuan"]),
                            SoNgayCtyChoNghi = Convert.ToDecimal(dr["SoNgayCtyChoNghi"]),
                            CongLeTet = Convert.ToDecimal(dr["CongLeTet"]),
                            GhiChu = dr["GhiChu"],
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                        });
                        break;
                }
            }
            if (dem > 0)
            {
                GetKyLuong();
                XtraMessageBox.Show("Cập nhật kỳ lương thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Không có thông tin thay đổi.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_kyluong")
            {
                GetKyLuong();
            }
        }

        private void Form_KyLuong_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKyLuong();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_ThemKyLuong frm = new Form_ThemKyLuong();
            frm.ShowDialog();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            var dgr = XtraMessageBox.Show($"Có muốn xóa kỳ lương này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_KyLuongDelete", new
            {
                Id = Convert.ToInt32(lblID.Text)
            });
            if (data.errcode == 1)
            {
                GetKyLuong();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}