using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SimpleBroker;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ThemPhieuXuatNVL : XtraForm
    {
        public int intMaSP = 0;
        public int count = 0;
        public Form_ThemPhieuXuatNVL()
        {
            InitializeComponent();
            dteNgayLap.EditValue = DateTime.Now.Date;           
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_khachhang")
            {
                GetKhachHang();
            }
        }      
        
        public void GetKho()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_KhoSelect");
            cboKho.Properties.DataSource= data;
            cboKho.Properties.DisplayMember = "TenKho";
            cboKho.Properties.ValueMember = "Id";
            cboKho.EditValue = 2;
        }

        private void Form_ThemPhieuXuat_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKho();
            GetKhachHang();
            GetNhanVien();
            GetSanPham();
            if (Form_Template.Edit == true) //
            {
                DataTable lstHD = ExecSQL.ExecProcedureDataAsDataTable("sp_HoaDonSelectMaHD", new
                {
                    mahd = Form_Template.strMaHoaDon
                });
                if (lstHD.Rows.Count == 0) { return; }
                cboKhachHang.EditValue =Convert.ToInt32(lstHD.Rows[0]["MaKhachHang"]);
                txtMaHD.Text = lstHD.Rows[0]["MaHoaDon"].ToString();
                dteNgayLap.EditValue = Convert.ToDateTime(lstHD.Rows[0]["NgayLap"]);
                cboNhanVien.EditValue = Convert.ToInt32(lstHD.Rows[0]["MaNhanVien"]);
                txtDienGiai.Text = lstHD.Rows[0]["DienGiai"].ToString();               
                txtMaHD.Properties.ReadOnly = true;

                ///Load chi tiết hòa đơn
                GetCTHoaDon();
            }
            else
            {
                var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new {option = 7});
                txtMaHD.Text= data.ToString();
                GetCTHoaDon();
            }
        }

        public void GetKhachHang()
        {
            var lstKhachHang = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource= lstKhachHang; 
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
        }

        public void GetNhanVien()
        {
            DataTable lstNV = ExecSQL.ExecProcedureDataAsDataTable("sp_NhanVienselectall");
            cboNhanVien.Properties.DataSource = lstNV;
            cboNhanVien.Properties.DisplayMember = "TenNhanVien";
            cboNhanVien.Properties.ValueMember = "ID";
            cboNhanVien.EditValue = Form_Template.intMaNV;
        }

        private void cboKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            if(cboKhachHang.EditValue == DBNull.Value)
            {
                txtMaKH.Text = "";
                txtDiaChi.Text = "";
                txtDienThoai.Text = "";
                txtDienGiai.Text = "";
                return;
            }
            DataTable listKH = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangByID", new
            {
                ID = Convert.ToInt32(cboKhachHang.EditValue)
            });
            if (listKH.Rows.Count == 0) {
                return;
            }
            txtMaKH.Text = listKH.Rows[0]["ID"].ToString();
            txtDiaChi.Text = listKH.Rows[0]["DiaChi"].ToString();
            txtDienThoai.Text = listKH.Rows[0]["DienThoai1"].ToString();          
        }

        public void GetCTHoaDon()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstCTHoaDon= ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new
            {
                mahd=txtMaHD.Text
            });
            grcChiTietPhieuXuat.DataSource = lstCTHoaDon;

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;

        }

        public void GetSanPham()
        {
            var lstSanPham = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelect", new
            {
                option = 3
            });
            cboMaSP.DataSource = lstSanPham;
            cboMaSP.DisplayMember = "ID";
            cboMaSP.ValueMember = "ID";
        }

        private void BtnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaHD.Focus();
            if (txtMaHD.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập số hóa đơn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHD.Focus();
                return;
            }
            if (cboKhachHang.EditValue == null || txtMaKH.Text.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            if (cboKho.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn tên kho", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
            //Lưu Hoa1 đơn
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_HoaDonInsert", new
            {
                mahd = txtMaHD.Text,
                ngaylap = Convert.ToDateTime(dteNgayLap.EditValue),
                manv = Convert.ToInt32( cboNhanVien.EditValue),
                makh = Convert.ToInt32(cboKhachHang.EditValue),
                diengiai = txtDienGiai.Text,                
                loaiphieu=Form_Template.strLoaiPhieu,
                nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper(),
                makho = Convert.ToInt32(cboKho.EditValue)
            });
            if (data.errcode==1) //Lưu thành công
            {                
                for (var i = 0; i <= gridView1.RowCount; i++)
                {

                    var dr = gridView1.GetDataRow(Convert.ToInt32(i));
                    if (ReferenceEquals(dr, null))
                    {
                        break;
                    }
                    switch (dr.RowState)
                    {
                        case DataRowState.Added:                            
                            ExecSQL.ExecProcedureNonData("sp_ChiTietHoaDonInsert", new
                            {
                                mahd = txtMaHD.Text,                                
                                masp = dr["MaSanPham"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia=  dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                chieckhau = 0,
                                tienchieckhau= 0,
                                thanhtoan = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                                VAT = 0,
                                TienVAT = 0,
                                GiaTruocThue = 0
                            });
                            break;
                        case DataRowState.Modified:
                            ExecSQL.ExecProcedureNonData("sp_ChiTietHoaDonUpdate", new
                            {
                                mahd = txtMaHD.Text,
                                masp = dr["MaSanPham"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia = dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                chieckhau = 0,
                                tienchieckhau = 0,
                                thanhtoan = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                                NguoiSua = Form_Template.TaiKhoanLogin.ToUpper(),
                                VAT = 0,
                                TienVAT = 0,
                                GiaTruocThue = 0                                
                            });
                            break;                            
                    }
                    count += 1;
                }
                if (count > 0)
                {
                    //Update lại thành tiền
                    ExecSQL.ExecProcedureNonData("sp_HoaDonUpdateThanhTien", new
                    {
                        madh=txtMaHD.Text
                    });
                   //Gửi dữ liệu
                   var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuxuat_nvl"
                    };
                    msgBroker.Publish();                   
                    GetCTHoaDon();
                    XtraMessageBox.Show("Thêm mới phiếu xuất thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GetCTHoaDon();
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_phieuxuat_nvl"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //Lưu chi tiết hóa đơn

        }
      
        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
            {
            if (txtMaHD.EditValue == null)
            {
                XtraMessageBox.Show("Bạn vui lòng nhập vào mã chứng từ để tiếp tục.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHD.Focus();
                return;
            }
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Bạn vui lòng nhập chọn tên khách hàng.","Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }           
            var view = (GridView)sender;
            var i = view.FocusedRowHandle;        
            switch (view.FocusedColumn.FieldName)
            {
                case "MaSanPham":
                   
                    DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamByID", new { id = e.Value.ToString() });
                    if (dt == null) { return; }                  
                    view.SetRowCellValue(i, "MaHoaDon", txtMaHD.Text);
                    view.SetRowCellValue(i, "MaSanPham", e.Value);
                    view.SetRowCellValue(i, "TenSanPham", dt.Rows[0]["TenSanPham"].ToString());
                    view.SetRowCellValue(i, "DonViTinh", dt.Rows[0]["DonViTinh"].ToString());
                    view.SetRowCellValue(i, "SoLuong", 1);   
                    view.SetRowCellValue(i, "DonGia", 0);                  
                    view.SetRowCellValue(i, "ThanhTien", 0);                   
                    break;
                case "SoLuong":
                    try
                    {
                        view.SetRowCellValue(i, "SoLuong", e.Value);
                        view.SetRowCellValue(i, "ThanhTien", Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i,"DonGia")));  
                    }
                    catch (Exception)
                    {
                        e.Valid = false;
                        e.ErrorText = "Lỗi nhập liệu";
                    }                   
                    break;
                case "DonGia":
                    try
                    {
                        view.SetRowCellValue(i, "DonGia", e.Value);
                        view.SetRowCellValue(i, "ThanhTien", Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i, "SoLuong")));
                    }
                    catch (Exception)
                    {
                        e.Valid = false;
                        e.ErrorText = "Lỗi nhập liệu";
                    }
                    break;
            }
        }

        private void txtMaHD_Leave(object sender, EventArgs e)
        {
            GetCTHoaDon();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetCTHoaDon();
        }

        private void Form_ThemPhieuXuat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa chi tiết hóa đơn (" + txtMaHD.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureSacalar("sp_ChiTietHoaDonDelete", new
            {
                mahd = txtMaHD.Text,
                masp=intMaSP
            });
            if (data.ToString() == "1")
            {
                GetCTHoaDon();
                XtraMessageBox.Show("Xóa hóa đơn thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Xóa hóa đơn không thành công", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Lấy mã sản phẫm khi click vào chi tiết hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            intMaSP = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaSanPham"));
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_InHoaDonByMaHD", new
            {
                mahd = txtMaHD.Text               
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 1;
            var frm = new Form_Report();
            frm.Show();
          

        }

        /// <summary>
        /// Thêm khách hàng mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboKhachHang_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                Form_Template.intIDMaKH = 0;
                Form_Template.Edit = false;
                Form_ThemKhachhang frm = new Form_ThemKhachhang();
                frm.ShowDialog();
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            cboKhachHang.EditValue = DBNull.Value;
            var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = 7 });
            txtMaHD.Text = data.ToString();
            GetCTHoaDon();
        }
                  
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "PhieuXuatKho_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new { mahd = txtMaHD.Text });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }
      
    }
}