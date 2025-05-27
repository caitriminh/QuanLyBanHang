using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_CauHinhThamSo : XtraForm
    {
        public Form_CauHinhThamSo()
        {
            InitializeComponent();            
        }
      
        public void GetThamSo()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ThamSoSelectAll");
            grcThamSo.DataSource = dt;           
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetThamSo();
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
                        ExecSQL.ExecProcedureNonData("sp_ThamSoUpdate", new
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            GiaTri = Convert.ToDecimal(dr["GiaTri"]),                           
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                        });
                        break;
                }
            }
            if (dem > 0)
            {
                GetThamSo();
                XtraMessageBox.Show("Cập nhật cấu hình tham số thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Không có thông tin thay đổi.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void Form_CauHinhThamSo_Load(object sender, EventArgs e)
        {
            GetThamSo();
        }
    }
}