using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemQuyDoi : XtraForm
    {
        public Form_ThemQuyDoi()
        {
            InitializeComponent();          
        }

        public void GetLoaiNgayNghi()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiNgayNghiSelect");
            cboLoaiNgay.Properties.DataSource = lstData;
            cboLoaiNgay.Properties.DisplayMember = "LoaiNgayNghi";
            cboLoaiNgay.Properties.ValueMember = "Id";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboVaoRa.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập loại giờ vào / ra.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVaoRa.Focus();
                return;
            }
            if (cboLoaiNgay.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào loại ngày nghỉ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiNgay.Focus();
                return;
            }
            if (txtTuGio.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào giờ chấm công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuGio.Focus();
                return;
            }
            if (txtDenGio.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào giờ chấm công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDenGio.Focus();
                return;
            }
            if (txtQuyDoi.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào giờ quy đổi", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuyDoi.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDQuyDoi > 0)
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_QuyDoiGioVaoRa", new
                {
                    Id = Form_Template.intIDQuyDoi,
                    TuGio = Convert.ToInt32(txtTuGio.EditValue),
                    DenGio = Convert.ToInt32(txtDenGio.EditValue),
                    QuyDoi = Convert.ToInt32(txtQuyDoi.EditValue),
                    GioNghi = Convert.ToInt32(txtNghiTrua.EditValue),
                    Loai = Convert.ToInt32(cboLoaiNgay.EditValue),
                    VaoRa = cboVaoRa.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_quydoi"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDQuyDoi = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_QuyDoiGioVaoRa", new
                {
                    TuGio = Convert.ToInt32(txtTuGio.EditValue),
                    DenGio = Convert.ToInt32(txtDenGio.EditValue),
                    QuyDoi = Convert.ToInt32(txtQuyDoi.EditValue),
                    GioNghi = Convert.ToInt32(txtNghiTrua.EditValue),
                    Loai = Convert.ToInt32(cboLoaiNgay.EditValue),
                    VaoRa = cboVaoRa.Text,
                    nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_quydoi"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTuGio.EditValue = null;
                    txtDenGio.EditValue = null;
                    txtQuyDoi.EditValue = null;
                    txtNghiTrua.EditValue = null;
                    txtTuGio.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void Form_ThemQuyDoi_Load(object sender, EventArgs e)
        {
            GetLoaiNgayNghi();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_QuyDoiGioVaoRaSelectID", new
                {
                    Id = Form_Template.intIDQuyDoi
                });
                if (dt.Rows.Count > 0)
                {
                    cboLoaiNgay.EditValue = Convert.ToInt32(dt.Rows[0]["Loai"]);
                    cboVaoRa.EditValue = dt.Rows[0]["VaoRa"];
                    txtTuGio.EditValue = dt.Rows[0]["TuGio"];
                    txtDenGio.EditValue = dt.Rows[0]["DenGio"];
                    txtQuyDoi.EditValue = dt.Rows[0]["QuyDoi"];
                    txtNghiTrua.EditValue = dt.Rows[0]["GioNghi"];
                }
            }
        }

        private void Form_ThemQuyDoi_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemQuyDoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDQuyDoi = 0;
        }
    }
}