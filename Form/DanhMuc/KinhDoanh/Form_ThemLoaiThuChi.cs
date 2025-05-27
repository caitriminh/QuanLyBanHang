using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemLoaiThuChi : XtraForm
    {
        public Form_ThemLoaiThuChi()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(cboLoaiPhieu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên loại phiếu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhieu.Focus();
                return;
            }
            if (txtLoaiThuChi.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên loại thu chi", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoaiThuChi.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDLoaiThuChi > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiThuChi", new
                {
                    id = Form_Template.intIDLoaiThuChi,
                    LoaiThuChi = txtLoaiThuChi.Text,
                    LoaiPhieu = cboLoaiPhieu.Text,                  
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                }) ;
                if (dataEdit== null) 
                {
                    XtraMessageBox.Show("Cập nhật loại thu chi không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loaithuchi"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDLoaiThuChi = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_LoaiThuChi", new
                {
                    id = 0,
                    LoaiThuChi = txtLoaiThuChi.Text,
                    LoaiPhieu = cboLoaiPhieu.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới loại thu chi không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_loaithuchi"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLoaiThuChi.Text = ""; 
                    txtLoaiThuChi.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemLoaiThuChi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDLoaiThuChi = 0;
        }

        private void Form_ThemLoaiThuChi_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiThuChiSelectByID", new
                {
                    ID = Form_Template.intIDLoaiThuChi
                });
                if (dt.Rows.Count > 0)
                {
                    txtLoaiThuChi.EditValue = dt.Rows[0]["LoaiThuChi"];
                    cboLoaiPhieu.Text = dt.Rows[0]["LoaiPhieu"].ToString();                   
                }
            }
        }

        private void Form_ThemLoaiThuChi_KeyDown(object sender, KeyEventArgs e)
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