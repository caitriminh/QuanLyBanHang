using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemUngLuong : XtraForm
    {
        public Form_ThemUngLuong()
        {
            InitializeComponent();
            dteNgayNhap.EditValue = DateTime.Now.Date;
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option= 1
            });
            cboNhanVien.Properties.DataSource = lstData;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
        }
       
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.EditValue ==null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }
            if (txtUngLuong.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập số tiền", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUngLuong.Focus();
                return;
            }            
            if (dteNgayNhap.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày nhập", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayNhap.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDUngLuong > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_UngLuong", new
                {
                    Id = Form_Template.intIDUngLuong,
                    ngaynhap = Convert.ToDateTime(dteNgayNhap.EditValue),
                    ungluong = Convert.ToDecimal(txtUngLuong.EditValue),                   
                    ghichu = txtGhiChu.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_ungluong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDUngLuong = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_UngLuong", new                
                {           
                    manv = Convert.ToInt32(cboNhanVien.EditValue),
                    ngaynhap = Convert.ToDateTime(dteNgayNhap.EditValue),
                    ungluong = Convert.ToDecimal(txtUngLuong.EditValue),
                    ghichu = txtGhiChu.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()                   
                });
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_ungluong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    txtUngLuong.EditValue = null;
                    dteNgayNhap.EditValue = null;
                    txtGhiChu.EditValue = null;                   
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form_ThemUngLuong_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_UngLuongSelectById", new
                {
                    Id = Form_Template.intIDUngLuong
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtUngLuong.EditValue = dt.Rows[0]["UngLuong"];
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgayNhap.EditValue = dt.Rows[0]["NgayNhap"];
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemUngLuong_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnLuu_Click(sender, e);
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void Form_ThemUngLuong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDUngLuong = 0;
            Form_Template.Edit = false;
        }
    }
}