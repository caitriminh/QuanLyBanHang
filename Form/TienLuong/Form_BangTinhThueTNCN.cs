using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_BangTinhThueTNCN : XtraForm
    {
        public Form_BangTinhThueTNCN()
        {
            InitializeComponent();            
        }
      
        public void GetBangTinhThue()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_BangTinhThueSelectAll");
            grcBangTinhThueTNCN.DataSource = dt;
            lblID.DataBindings.Clear();
            lblID.DataBindings.Add("text", dt, "ID");
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetBangTinhThue();
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
                        ExecSQL.ExecProcedureNonData("sp_BangTinhThueUpdate", new
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            ThuNhapTu = Convert.ToDecimal(dr["ThuNhapTu"]),
                            ThuNhapDen = Convert.ToDecimal(dr["ThuNhapDen"]),
                            Thue = Convert.ToDecimal(dr["Thue"]),
                            SoTien = dr["SoTien"],
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                        });
                        break;
                }
            }
            if (dem > 0)
            {
                GetBangTinhThue();
                XtraMessageBox.Show("Cập nhật bảng tính thuế thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                GetBangTinhThue();
            }
        }

        private void Form_KyLuong_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetBangTinhThue();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_ThemKyLuong frm = new Form_ThemKyLuong();
            frm.ShowDialog();
        }

      
    }
}