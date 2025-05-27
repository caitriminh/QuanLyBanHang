using DevExpress.XtraEditors;
using System;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_DangKyTangCa : XtraForm
    {
        public List<DangKyTangCa> tangca = new List<DangKyTangCa>();
        public Form_DangKyTangCa()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 62
            });
            if (data == null) { return; }
            btnDuyet.Enabled = data.Luu;          
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;           
        }

        private void GetDangKyTangCa(DateTime tungay, DateTime denngay)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_DangKyTangCaSelectAll", new
            {
                tungay,
                denngay
            });
            grcDangKyTangCa.DataSource = data;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", data, "Id");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_dangky_tangca")
            {
                GetDangKyTangCa(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDDKTangCa = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemDKTangCa frm = new Form_ThemDKTangCa();
            frm.ShowDialog();
        }
       
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDangKyTangCa(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }
      
        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetDangKyTangCa(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void Form_DangKyTangCa_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemDKTangCa frm = new Form_ThemDKTangCa();
            frm.ShowDialog();
        }

        private void btnXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgrResult = XtraMessageBox.Show($"Bạn có muốn xác nhận đăng ký tăng cả của nhân viên ({ gridView1.GetRowCellValue(i,"TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult == DialogResult.Yes)
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyTangCaDuyet", new
                {
                    option = 1,
                    Id = Convert.ToInt32(lbl_Id.Text),                  
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    GetDangKyTangCa(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                    if (gridView1.RowCount > 0)
                    {
                        gridView1_FocusedRowChanged(this, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gridView1.FocusedRowHandle, gridView1.FocusedRowHandle));
                    }
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgrResult = XtraMessageBox.Show($"Bạn có muốn duyệt đăng ký tăng cả của nhân viên ({gridView1.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgrResult == DialogResult.Yes)
            {
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyTangCaDuyet", new
                {
                    option = 2,
                    Id = Convert.ToInt32(lbl_Id.Text),                  
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if (data.errcode == 1)
                {
                    GetDangKyTangCa(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                    if (gridView1.RowCount > 0)
                    {
                        gridView1_FocusedRowChanged(this, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gridView1.FocusedRowHandle, gridView1.FocusedRowHandle));
                    }
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Lấy chỉ số của dòng hiện tại đang được chọn trong GridView
            var i = gridView1.FocusedRowHandle;

            // Kiểm tra nếu không có dòng nào được chọn (chỉ số dòng nhỏ hơn 0) thì thoát khỏi hàm
            if (i < 0) { return; }

            // Xóa danh sách đăng ký tăng ca hiện tại để chuẩn bị thêm dữ liệu mới
            tangca.Clear();

            // Thêm một đối tượng mới vào danh sách tangca dựa trên dữ liệu của dòng hiện tại
            tangca.Add(new DangKyTangCa()
            {
                MaNV = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaNV")),  // Lấy mã nhân viên từ cột "MaNV"
                Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id")),      // Lấy ID từ cột "Id"
                Status = Convert.ToInt32(gridView1.GetRowCellValue(i, "Status")) // Lấy trạng thái từ cột "Status"
            });

            // Kiểm tra trạng thái (Status) và kích hoạt hoặc vô hiệu hóa nút btnXacNhan
            btnXacNhan.Enabled = tangca[0].Status == 2 || tangca[0].Status == 3 || tangca[0].Status == 0
                ? false : true;

            // Kiểm tra trạng thái (Status) và kích hoạt hoặc vô hiệu hóa nút btnDuyet
            btnDuyet.Enabled = tangca[0].Status == 3 || tangca[0].Status == 1 || tangca[0].Status == 0
                ? false : true;

            if (tangca[0].Status == 1)
            {
                btnDuyet.Enabled = false;
                btnXacNhan.Enabled = true;
                BtnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else if (tangca[0].Status == 2)
            {
                btnDuyet.Enabled = true;
                btnXacNhan.Enabled = false;
                BtnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            else
            {
                btnDuyet.Enabled = false;
                btnXacNhan.Enabled = false;
                BtnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }


        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            // Lấy chỉ số của dòng hiện tại đang được chọn trong GridView
            var i = gridView1.FocusedRowHandle;

            // Kiểm tra xem cột được nhấp có phải là cột "colKhoiPhuc" và các nút "btnDuyet" hoặc "btnXacNhan" có được kích hoạt không
            if (e.Column == colKhoiPhuc && Convert.ToInt32(gridView1.GetRowCellValue(i,"Status"))>1)
            {
                // Hiển thị hộp thoại xác nhận xem người dùng có muốn khôi phục đăng ký tăng ca của nhân viên không
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn khôi phục đăng ký tăng ca của nhân viên ({gridView1.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn "Yes" trong hộp thoại
                if (dgrResult == DialogResult.Yes)
                {
                    // Gọi thủ tục "sp_DangKyTangCaDuyet" với option 3 để khôi phục đăng ký tăng ca
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyTangCaDuyet", new
                    {
                        option = 3,  // Tùy chọn khôi phục đăng ký tăng ca
                        Id = Convert.ToInt32(lbl_Id.Text),  // Lấy ID của đăng ký tăng ca từ nhãn lbl_Id
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()  // Lấy tài khoản đăng nhập của người thực hiện
                    });

                    // Nếu thủ tục trả về errcode = 1 (thành công)
                    if (data.errcode == 1)
                    {
                        // Cập nhật lại danh sách đăng ký tăng ca từ ngày dteTuNgay đến ngày dteDenNgay
                        GetDangKyTangCa(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView1.RowCount > 0)
                        {
                            gridView1_FocusedRowChanged(this, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gridView1.FocusedRowHandle, gridView1.FocusedRowHandle));
                        }
                        // Hiển thị thông báo thành công
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Hiển thị thông báo cảnh báo nếu có lỗi
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                // Hiển thị cảnh báo nếu đăng ký tăng ca của nhân viên chưa được xác nhận
                XtraMessageBox.Show("Đăng ký tăng ca của nhân viên chưa xác nhận.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DangKyTangCa_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcDangKyTangCa.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            var dgr = XtraMessageBox.Show($"Có muốn xóa đăng ký tăng ca nhân viên {gridView1.GetRowCellValue(i, "TenNhanVien")} này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyTangCaDelete", new
            {
                Id = Convert.ToInt32(lbl_Id.Text)
            });
            if (data.errcode == 1)
            {
                GetDangKyTangCa(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

}
