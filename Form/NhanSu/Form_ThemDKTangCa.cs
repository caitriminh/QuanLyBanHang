using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemDKTangCa : XtraForm
    {
        public Form_ThemDKTangCa()
        {
            InitializeComponent();
            txtTuGio.EditValue = DateTime.Now.Date;
            txtDenGio.EditValue = DateTime.Now.Date;
            dteNgayTangCa.EditValue = DateTime.Now.Date;
        }

        public void GetNhanVien()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall", new
            {
                option= Form_Template.intIDNhanVien > 0 ? 2 : 1
            });
            cboNhanVien.Properties.DataSource = lstData;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
        }

        public void GetLoaiTangCa()
        {
            var lstData = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiTangCaSelectAll");
            cboLoaiTangCa.Properties.DataSource = lstData;
            cboLoaiTangCa.Properties.DisplayMember = "LoaiTangCa";
            cboLoaiTangCa.Properties.ValueMember = "Id";
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
            if (dteNgayTangCa.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập ngày tăng ca", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayTangCa.Focus();
                return;
            }
            if (txtTuGio.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập giờ tăng ca", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuGio.Focus();
                return;
            }
            if (txtDenGio.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập giờ tăng ca", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDenGio.Focus();
                return;
            }
            if (cboLoaiTangCa.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn loại tăng ca", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiTangCa.Focus();
                return;
            }
            string TuGio = Convert.ToDateTime(dteNgayTangCa.EditValue).ToString("yyyyMMdd") + " " + Convert.ToDateTime(txtTuGio.EditValue).ToString("HH:mm");
            string DenGio = Convert.ToDateTime(dteNgayTangCa.EditValue).ToString("yyyyMMdd") + " " + Convert.ToDateTime(txtDenGio.EditValue).ToString("HH:mm");
            //Chỉnh sửa thông tin
            if (Form_Template.intIDDKTangCa > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyTangCaUpdate", new
                {
                    Id = Form_Template.intIDDKTangCa,
                    TuGio,
                    DenGio,
                    LoaiTangCa = Convert.ToInt32(cboLoaiTangCa.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (dataEdit.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_dangky_tangca"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDDKTangCa = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show(dataEdit.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyTangCaInsert", new
                {                     
                    MaNV = Convert.ToInt32(cboNhanVien.EditValue),
                    NgayTangCa = Convert.ToDateTime(dteNgayTangCa.EditValue),
                    TuGio,
                    DenGio,
                    LoaiTangCa = Convert.ToInt32(cboLoaiTangCa.EditValue),
                    GhiChu = txtGhiChu.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });               
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_dangky_tangca"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboNhanVien.EditValue = null;
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

        private void Form_ThemDKTangCa_Load(object sender, EventArgs e)
        {
            GetNhanVien();
            GetLoaiTangCa();
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_DangKyTangCaSelectID", new
                {
                    Id = Form_Template.intIDDKTangCa
                });
                if (dt.Rows.Count > 0)
                {
                    cboNhanVien.EditValue = Convert.ToInt32(dt.Rows[0]["MaNV"]);
                    txtGhiChu.EditValue = dt.Rows[0]["GhiChu"];
                    dteNgayTangCa.EditValue = Convert.ToDateTime(dt.Rows[0]["NgayTangCa"]);
                    txtTuGio.EditValue = dt.Rows[0]["TuGio"];
                    txtDenGio.EditValue = dt.Rows[0]["DenGio"];
                    cboLoaiTangCa.EditValue = Convert.ToInt32(dt.Rows[0]["LoaiTangCa"]);
                    cboNhanVien.Properties.ReadOnly = true;
                }
            }
        }

        private void Form_ThemDKTangCa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.intIDDKTangCa = 0;
            Form_Template.Edit = false;
        }

        private void Form_ThemDKTangCa_KeyDown(object sender, KeyEventArgs e)
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