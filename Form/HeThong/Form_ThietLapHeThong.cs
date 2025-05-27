using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using VisualStudio2019.Class;
using QuanLyBanHang.Class;
using System.Data;

namespace VisualStudio2019.Form
{
    public partial class Form_ThietLapHeThong : XtraForm
    {
        public Form_ThietLapHeThong()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 2
            });
            if (data == null) { return; }           
            btnLuu.Enabled = data.Luu;
        }

        private void GetThietLap()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_CauHinhSelect");
            grcThietLap.DataSource = data;
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lblTenDangNhap.Focus();
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
                        ExecSQL.ExecProcedureNonData("sp_CauHinhUpdate", new
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            KyHieu = dr["KyHieu"].ToString(),
                            ThangNam = dr["ThangNam"].ToString(),
                            Kieu = dr["Kieu"].ToString(),                           
                            STT = Convert.ToInt32(dr["STT"]),                          
                            NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                        });
                        break;
                }
            }
            if (dem > 0)
            {
                GetThietLap();
                XtraMessageBox.Show("Cập nhật thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Không có thông tin thay đổi.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
       

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetThietLap();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void Form_ThietLapHeThong_Load(object sender, EventArgs e)
        {
            GetThietLap();
        }
    }
    }
