using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemMucLuong : XtraForm
    {
        public Form_ThemMucLuong()
        {
            InitializeComponent();
            dteNgayApDung.EditValue = DateTime.Now.Date;
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
            if (txtMucLuong.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập mức lương nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMucLuong.Focus();
                return;
            }
            if (txtLuongCoBan.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập lương cơ bản nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongCoBan.Focus();
                return;
            }
            if (txtLuongTrachNhiem.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập lương trách nhiệm nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongTrachNhiem.Focus();
                return;
            }
            if (txtLuongTangCa.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập lương tăng ca nhân viên", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongTangCa.Focus();
                return;
            }
            if (dteNgayApDung.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày áp dụng mức lương", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayApDung.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDMucLuong > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_MucLuong", new
                {
                    Id = Form_Template.intIDMucLuong,
                    ngayapdung = Convert.ToDateTime(dteNgayApDung.EditValue),
                    mucluong = Convert.ToDecimal(txtMucLuong.EditValue),                   
                    ghichu = txtGhiChu.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper(),
                    luongcoban = Convert.ToDecimal(txtLuongCoBan.EditValue),
                    luongtrachnhiem = Convert.ToDecimal(txtLuongTrachNhiem.EditValue),
                    luongtangca = Convert.ToDecimal(txtLuongTangCa.EditValue)
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_mucluong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDMucLuong = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_MucLuong", new                
                {           
                    manv = Convert.ToInt32(cboNhanVien.EditValue),
                    ngayapdung = Convert.ToDateTime(dteNgayApDung.EditValue),
                    mucluong = Convert.ToDecimal(txtMucLuong.EditValue),
                    ghichu = txtGhiChu.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper(),
                    luongcoban = Convert.ToDecimal(txtLuongCoBan.EditValue),
                    luongtrachnhiem = Convert.ToDecimal(txtLuongTrachNhiem.EditValue),
                    luongtangca = Convert.ToDecimal(txtLuongTangCa.EditValue)
                });
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_mucluong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    txtMucLuong.EditValue = null;
                    dteNgayApDung.EditValue = null;
                    txtGhiChu.EditValue = null;
                    txtLuongCoBan.EditValue = null;
                    txtLuongTrachNhiem.EditValue = null;
                    txtLuongTangCa.EditValue = null;
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
       
        private void Form_ThemMucLuong_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_MucLuongSelectById", new
                {
                    Id = Form_Template.intIDMucLuong
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtMucLuong.EditValue = dt.Rows[0]["MucLuong"];
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    txtLuongCoBan.EditValue = dt.Rows[0]["LuongCoBan"];
                    dteNgayApDung.EditValue = dt.Rows[0]["NgayApDung"];
                    txtLuongTrachNhiem.EditValue = dt.Rows[0]["LuongTrachNhiem"];
                    txtLuongTangCa.EditValue = dt.Rows[0]["LuongTangCa"];
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemMucLuong_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemMucLuong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDMucLuong = 0;
            Form_Template.Edit = false;
        }
    }
}