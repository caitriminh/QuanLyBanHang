using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemHoaHongNhanVien : XtraForm
    {
        public Form_ThemHoaHongNhanVien()
        {
            InitializeComponent();
            dteNgayNhap.EditValue = DateTime.Now.Date.ToString("01/MM/yyyy");
                 
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option = 1
            }) ;
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
            if (dteNgayNhap.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày đăng ký", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayNhap.Focus();
                return;
            }
            
            
            //Chỉnh sửa thông tin
            if (Form_Template.intIDHoaHongNhanVien > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("spHoaHongUpdate", new
                {
                    Id = Form_Template.intIDHoaHongNhanVien,
                    NgayNhap = Convert.ToDateTime(dteNgayNhap.EditValue),
                    DinhMuc = Convert.ToDecimal(txtSoTien.EditValue),
                    GhiChu = txtGhiChu.Text,                 
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_hoahong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDHoaHongNhanVien = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("spHoaHongInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NgayNhap = Convert.ToDateTime(dteNgayNhap.EditValue),                  
                    DinhMuc = Convert.ToDecimal(txtSoTien.EditValue),                   
                    GhiChu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_hoahong"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    txtSoTien.EditValue = null;                  
                    dteNgayNhap.EditValue = null;                  
                    txtGhiChu.EditValue = null;  
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemHoaHongNhanVien_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("spGetHoaHong", new
                {
                    Option= 2,
                    Id = Form_Template.intIDHoaHongNhanVien
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgayNhap.EditValue = Convert.ToDateTime(dt.Rows[0]["NgayNhap"]);
                    txtSoTien.EditValue = dt.Rows[0]["DinhMuc"];
                    cboNhanVien.Properties.ReadOnly = true;
                    dteNgayNhap.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemHoaHongNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDHoaHongNhanVien = 0;
            Form_Template.Edit = false;
        }

        private void Form_ThemHoaHongNhanVien_KeyDown(object sender, KeyEventArgs e)
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
    }
}