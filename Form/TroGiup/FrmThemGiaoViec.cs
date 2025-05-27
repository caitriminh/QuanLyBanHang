using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class FrmThemGiaoViec : XtraForm
    {
        public FrmThemGiaoViec()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtTenTruong.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập tên trường", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtDiaChi.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập địa chỉ", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTruong.Focus();
                return;
            }
            //Chỉnh sửa thông tin
            if (Form_Template.intIDGiaoViec > 0)
            {
                var dataEdit = ExecSQL.ExecProcedureDataFistOrDefault<Status>("spUpdateGiaoViec", new
                {
                    id = Form_Template.intIDGiaoViec,
                    NgayNhap = Convert.ToDateTime(dteNgayNhap.EditValue),
                    TenTruong = txtTenTruong.Text,
                    DiaChi = txtDiaChi.Text,
                    SoLuongHS = txtSoLuongHS.EditValue,
                    HieuTruong = txtHieuTruong.Text,
                    DienThoai = txtDienThoai.Text,
                    NoiDung = txtNoiDungCongViec.Text,
                    KhoKhan = txtKhoKhan.Text,
                    HuongXuLy = txtHuongXuLy.Text,
                    KetQua = txtKetQua.Text,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
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
                        task = "them_giaoviec"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(dataEdit.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form_Template.intIDGiaoViec = 0;
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Cập nhật chiếc khấu không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else //Thêm mới chiếc khấu
            {              
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("spInsertGiaoViec", new
                {
                    NgayNhap = Convert.ToDateTime(dteNgayNhap.EditValue),
                    TenTruong = txtTenTruong.Text,
                    DiaChi = txtDiaChi.Text,
                    SoLuongHS = txtSoLuongHS.EditValue,
                    HieuTruong = txtHieuTruong.Text,
                    DienThoai = txtDienThoai.Text,
                    NoiDung = txtNoiDungCongViec.Text,
                    KhoKhan = txtKhoKhan.Text,
                    HuongXuLy = txtHuongXuLy.Text,
                    KetQua = txtKetQua.Text,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data == null)
                {
                    XtraMessageBox.Show("Thêm mới báo cáo không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (data.errcode==1)
                {
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_giaoviec"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenTruong.Text = ""; 
                    txtTenTruong.Focus();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void FrmThemGiaoViec_Load(object sender, EventArgs e)
        {
            if (Form_Template.Edit)
            {
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("spGetGiaoViec", new
                {
                    Option = 2,
                    Id = Form_Template.intIDGiaoViec
                });
                if (dt.Rows.Count > 0)
                {
                    dteNgayNhap.EditValue= dt.Rows[0]["NgayNhap"];
                    txtTenTruong.EditValue = dt.Rows[0]["TenTruong"];
                    txtDiaChi.EditValue = dt.Rows[0]["DiaChi"];                  
                    txtSoLuongHS.EditValue = dt.Rows[0]["SoLuongHS"];
                    txtHieuTruong.EditValue = dt.Rows[0]["HieuTruong"];
                    txtDienThoai.EditValue = dt.Rows[0]["DienThoai"];
                    txtNoiDungCongViec.EditValue = dt.Rows[0]["NoiDung"];
                    txtKhoKhan.EditValue = dt.Rows[0]["KhoKhan"];
                    txtHuongXuLy.EditValue = dt.Rows[0]["HuongXuLy"];
                    txtKetQua.EditValue = dt.Rows[0]["KetQua"];
                }
            }
            else
            {
                dteNgayNhap.EditValue = DateTime.Now.Date;
            }
        }

        private void FrmThemGiaoViec_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmThemGiaoViec_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDGiaoViec = 0;
        }
    }
}