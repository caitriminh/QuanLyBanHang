using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemDoiTuongChiecKhau : XtraForm
    {
        public Form_ThemDoiTuongChiecKhau()
        {
            InitializeComponent();
            dteNgayApDung.EditValue = DateTime.Now.Date;
        }


        public void GetTenKhachHang()
        {
            var lstKH = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource = lstKH;
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            if (Form_Template.intIDDoiTuongCK > 0)
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DoiTuongChiecKhauUpdate", new
                {
                    Id = Form_Template.intIDDoiTuongCK,
                    NgayApDung = Convert.ToDateTime(dteNgayApDung.EditValue),                  
                    Ghichu = txtGhiChu.Text,
                    ChiecKhau = Convert.ToInt32(txtChiecKhau.EditValue),
                    NguoiNhan = txtNguoiNhan.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    Form_Template.intIDDoiTuongCK = 0;

                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_doituongchieckhau"
                    };
                    msgBroker.Publish();

                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DoiTuongChiecKhauInsert", new
                {
                    NgayApDung = Convert.ToDateTime(dteNgayApDung.EditValue),
                    MaKH = Convert.ToInt32(cboKhachHang.EditValue),
                    Ghichu = txtGhiChu.Text,
                    ChiecKhau = Convert.ToInt32(txtChiecKhau.EditValue),
                    NguoiNhan = txtNguoiNhan.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới đối tượng chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode == 1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_doituongchieckhau"
                    };
                    msgBroker.Publish();

                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChiecKhau.EditValue = 0;
                    txtGhiChu.Text = "";
                    txtNguoiNhan.Text = "";                  
                    txtNguoiNhan.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void Form_ThemDoiTuongChiecKhau_Load(object sender, EventArgs e)
        {
            GetTenKhachHang();
            if (Form_Template.Edit)
            {
                var data = ExecSQL.ExecProcedureDataAsDataTable("sp_DoiTuongChiecKhauSelectById", new
                {
                    Id = Form_Template.intIDDoiTuongCK
                });
                if (data.Rows.Count == 0) { return; }
                cboKhachHang.EditValue = data.Rows[0]["MaKH"];
                txtNguoiNhan.EditValue = data.Rows[0]["NguoiNhan"];
                txtChiecKhau.EditValue = data.Rows[0]["ChiecKhau"];
                txtGhiChu.EditValue = data.Rows[0]["GhiChu"];
                dteNgayApDung.EditValue = data.Rows[0]["NgayApDung"];
                cboKhachHang.Properties.ReadOnly = true;
            }
        }

        private void Form_ThemDoiTuongChiecKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDDoiTuongCK = 0;
        }
    }
}