using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemKyLuong : XtraForm
    {
        public Form_ThemKyLuong()
        {
            InitializeComponent();
            dteKyLuong.EditValue = DateTime.Now.Date;
        }

      
       
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dteKyLuong.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào kỳ lương", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteKyLuong.Focus();
                return;
            }
            if (txtSoCongChuan.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập số công chuẩn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoCongChuan.Focus();
                return;
            }
            if (txtCongLeTet.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập số công lễ / tết", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCongLeTet.Focus();
                return;
            }
            var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_KyLuongInsert", new
            {                
                KyLuong = Convert.ToDateTime(dteKyLuong.EditValue),
                SoCongChuan = Convert.ToDecimal(txtSoCongChuan.EditValue),
                GhiChu = txtGhiChu.Text,
                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                CongLeTet = Convert.ToDecimal(txtCongLeTet.EditValue),
                SoNgayCtyChoNghi = Convert.ToDecimal(txtSoNgayCtyChoNghi.EditValue)
            });
            if (dataEdit.errcode == 1)
            {
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "them_kyluong"
                };
                msgBroker.Publish();
                XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dteKyLuong.EditValue = null;
                txtSoCongChuan.EditValue= null;
                txtCongLeTet.EditValue= null;
                txtSoNgayCtyChoNghi.EditValue = null;
                txtGhiChu.EditValue = null;
            }
            else
            {
                XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void Form_ThemKyLuong_KeyDown(object sender, KeyEventArgs e)
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