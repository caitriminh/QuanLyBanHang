using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemPhieuChiCK : XtraForm
    {
        public Form_ThemPhieuChiCK()
        {
            InitializeComponent();
            dteNgayChungTu.EditValue = DateTime.Now.Date;           
        }

        public void GetLoaiPhieuChi()
        {
            var lstPhieuThu = ExecSQL.ExecProcedureDataAsDataTable("sp_LoaiThuChiByLoaiPhieu", new
            {
                LoaiPhieu = "Phiếu chi"
            });
            cboLoaiPhieuThu.Properties.DataSource = lstPhieuThu;
            cboLoaiPhieuThu.Properties.DisplayMember = "LoaiThuChi";
            cboLoaiPhieuThu.Properties.ValueMember = "Id";
            cboLoaiPhieuThu.EditValue = 7;
        }

        public void GetNhaCungCap()        
        {
            var lstNCC = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource = lstNCC;
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
            cboKhachHang.EditValue = Form_Template.intIDMaKH;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dteNgayChungTu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập vào ngày chứng từ.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dteNgayChungTu.Focus();
                return;
            }
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn khách hàng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhieuThu.Focus();
                return;
            }

            if (cboLoaiPhieuThu.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn loại phiếu chi", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiPhieuThu.Focus();
                return;
            }
                       
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhieuChiCKInsert", new
                {                    
                    LoaiThuChi = Convert.ToInt32(cboLoaiPhieuThu.EditValue),
                    SoTien = Convert.ToDecimal(txtSoTien.EditValue),
                    NgayChungTu = Convert.ToDateTime(dteNgayChungTu.EditValue),                 
                    DienGiai = txtGhiChu.Text,
                    MaKH = Convert.ToInt32(cboKhachHang.EditValue),
                    RefId = Form_Template.intIDDoiTuongCK,
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                    DoiTuong = txtNguoiNhan.Text
                }); ;
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
                Close();
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        private void Form_ThemPhieuThu_Load(object sender, EventArgs e)
        {
            GetNhaCungCap();
            GetLoaiPhieuChi();
            
                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_DoiTuongChiecKhauSelect", new
                {
                    Form_Template.tungay,
                    Form_Template.denngay,
                    Id = Form_Template.intIDDoiTuongCK.ToString()
                });
                if (dt.Rows.Count > 0)
                {                   
                    cboKhachHang.EditValue = dt.Rows[0]["MaKH"];   
                    txtThanhToan.EditValue = dt.Rows[0]["TienCK"];
                    txtSoTien.EditValue = dt.Rows[0]["TienCK"];
                    txtNguoiNhan.EditValue = dt.Rows[0]["NguoiNhan"];
                    txtGhiChu.EditValue = "Chi trả chiếc khấu cho " + dt.Rows[0]["NguoiNhan"];
                }
        }

        private void Form_ThemPhieuThu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strMaPhieuThu = "";
            Form_Template.intIDMaNCC = 0;
        }

      
    }
}