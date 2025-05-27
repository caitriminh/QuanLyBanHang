using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemDKCongTac : XtraForm
    {
        public Form_ThemDKCongTac()
        {
            InitializeComponent();
            dteTuNgay.EditValue = DateTime.Now.Date;
            dteDenNgay.EditValue = DateTime.Now.Date;            
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

        public void GetLoaiNghiPhep()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiNghiPhepSelect");
            cboLoaiNghiPhep.Properties.DataSource = lstData;
            cboLoaiNghiPhep.Properties.DisplayMember = "LoaiNghiPhep";
            cboLoaiNghiPhep.Properties.ValueMember = "Id";
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
            if (dteTuNgay.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày đăng ký", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteTuNgay.Focus();
                return;
            }
            if (dteDenNgay.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày đăng ký", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteDenNgay.Focus();
                return;
            }           
            if (cboLoaiNghiPhep.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn lý do.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiNghiPhep.Focus();
                return;
            }
            if (cboLoaiCongTac.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn loại công tác.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiCongTac.Focus();
                return;
            }
            if (cboPhuongTien.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn phương tiện đi công tác.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPhuongTien.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDNghiPhep_VS_CT > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_CongTacUpdate", new
                {
                    Id = Form_Template.intIDNghiPhep_VS_CT,                  
                    TuNgay = Convert.ToDateTime(dteTuNgay.EditValue),
                    DenNgay = Convert.ToDateTime(dteDenNgay.EditValue),
                    SoNgay = Convert.ToDecimal(txtSoNgay.EditValue),
                    LyDo = Convert.ToInt32(cboLoaiNghiPhep.EditValue),
                    GhiChu = txtGhiChu.Text,
                    LoaiCongTac = cboLoaiCongTac.Text,
                    PhuongTien = cboPhuongTien.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_congtac"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDNghiPhep_VS_CT = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_CongTacInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    TuNgay = Convert.ToDateTime(dteTuNgay.EditValue),
                    DenNgay = Convert.ToDateTime(dteDenNgay.EditValue),
                    SoNgay = Convert.ToDecimal(txtSoNgay.EditValue),
                    LyDo = Convert.ToInt32(cboLoaiNghiPhep.EditValue),
                    GhiChu = txtGhiChu.Text,
                    LoaiCongTac = cboLoaiCongTac.Text,
                    PhuongTien = cboPhuongTien.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_congtac"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
                    txtSoNgay.EditValue = null;
                    cboLoaiNghiPhep.EditValue = null;
                    dteTuNgay.EditValue = null;
                    dteDenNgay.EditValue= null;
                    txtGhiChu.EditValue = null;
                    cboPhuongTien.SelectedIndex = 0;
                    cboLoaiCongTac.SelectedIndex = 0;
                    cboNhanVien.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void Form_ThemDKCongTac_KeyDown(object sender, KeyEventArgs e)
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

        private void Form_ThemDKCongTac_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDNghiPhep_VS_CT = 0;
            Form_Template.Edit = false;
        }

        private void Form_ThemDKCongTac_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            GetLoaiNghiPhep();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_NghiPhepVeSomCongTacSelectID", new
                {
                    Id = Form_Template.intIDNghiPhep_VS_CT
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteTuNgay.EditValue = Convert.ToDateTime(dt.Rows[0]["TuNgay"]);
                    dteDenNgay.EditValue = Convert.ToDateTime(dt.Rows[0]["DenNgay"]);
                    txtSoNgay.EditValue = dt.Rows[0]["SoNgay"];
                    cboLoaiNghiPhep.EditValue = Convert.ToInt32(dt.Rows[0]["LyDo"]);
                    cboLoaiCongTac.EditValue = dt.Rows[0]["LoaiCongTac"];
                    cboPhuongTien.EditValue = dt.Rows[0]["PhuongTien"];
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }
    }
}