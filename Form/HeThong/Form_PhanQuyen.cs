using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using VisualStudio2019.Class;
using SimpleBroker;
using System.Data;
using QuanLyBanHang.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_PhanQuyen : XtraForm
    {
        public Form_PhanQuyen()
        {
            InitializeComponent();
            GetPhanQuyen2();
        }

        public void GetPhanQuyen2()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 25
            });
            if (data == null) { return; }
            btnLuu.Enabled = data.Sua;
            btnCapQuyen.Enabled = data.Sua;           
        }

        public void GetNhomQuyen()
        {
            var lstNhomQuyen = ExecSQL.ExecProcedureDataAsDataTable("sp_NhomQuyenSelect");
            cboQuyenHan2.DataSource = lstNhomQuyen;
            cboQuyenHan2.DisplayMember = "NhomQuyen";
            cboQuyenHan2.ValueMember = "Id";
        }

        public void GetPhanQuyen(int NhomQuyen)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;
            DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_PhanQuyenSelect", new
            {
                NhomQuyen
            });
            grcQuyenHan.DataSource = dt;
            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nguoidung")
            {
                GetNhomQuyen();
            }
        }
      
      
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cboNhomQuyen.EditValue != null)
            {
                GetPhanQuyen(Convert.ToInt32(cboNhomQuyen.EditValue));
            }
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                        ExecSQL.ExecProcedureNonData("sp_PhanQuyenUpdate", new
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Them = Convert.ToBoolean(dr["Them"]),
                            Luu = Convert.ToBoolean(dr["Luu"]),
                            Xoa = Convert.ToBoolean(dr["Xoa"]),
                            Sua = Convert.ToBoolean(dr["Sua"]),
                            Excel = Convert.ToBoolean(dr["Excel"]),
                            InAn = Convert.ToBoolean(dr["InAn"]),
                            NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                        });
                        break;
                }               
            }           
            if (dem > 0)
            {
                GetPhanQuyen(Convert.ToInt32(cboNhomQuyen.EditValue));
                XtraMessageBox.Show("Cập nhật quyền hạn thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Không có thông tin thay đổi.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

      
        private void Form_PhanQuyen_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNhomQuyen();
        }

        private void cboNhomQuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (cboNhomQuyen.EditValue != null)
            {
                GetPhanQuyen(Convert.ToInt32(cboNhomQuyen.EditValue));
            }
        }

        private void btnCapQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cboNhomQuyen.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn quyền hạn để thực hiện.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhanQuyenInsert", new
            {
                NhomQuyen = Convert.ToInt32(cboNhomQuyen.EditValue),
                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
            });
            if(data.errcode == 1)
            {
                GetPhanQuyen(Convert.ToInt32(cboNhomQuyen.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
   }
