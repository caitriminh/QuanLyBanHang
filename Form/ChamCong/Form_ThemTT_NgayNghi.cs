using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemTT_NgayNghi : XtraForm
    {
        public Form_ThemTT_NgayNghi()
        {
            InitializeComponent();
            dteNgayNghi.EditValue = DateTime.Now.Date;
        }

        public void GetLoaiNgayNghi()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiNgayNghiSelect");
            cboLoaiNgayNghi.Properties.DataSource = lstData;
            cboLoaiNgayNghi.Properties.DisplayMember = "LoaiNgayNghi";
            cboLoaiNgayNghi.Properties.ValueMember = "Id";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dteNgayNghi.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào ngày nghỉ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayNghi.Focus();
                return;
            }
            if (cboLoaiNgayNghi.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào loại ngày nghỉ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiNgayNghi.Focus();
                return;
            }
            //Chỉnh sửa thông tin
                       
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_NgayNghi", new
                {
                    NgayNghi = Convert.ToDateTime(dteNgayNghi.EditValue),
                    Loai = Convert.ToInt32(cboLoaiNgayNghi.EditValue),                  
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_ngaynghi"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dteNgayNghi.EditValue = null;
                cboLoaiNgayNghi.EditValue = null;
                    dteNgayNghi.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            
           
        }

        private void Form_ThemTT_NgayNghi_Load(object sender, EventArgs e)
        {
            GetLoaiNgayNghi();
        }

        private void Form_ThemTT_NgayNghi_KeyDown(object sender, KeyEventArgs e)
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