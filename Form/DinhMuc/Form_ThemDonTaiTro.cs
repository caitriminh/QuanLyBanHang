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
    public partial class Form_ThemDonTaiTro : XtraForm
    {
        public int intMaSP = 0;
        public int count = 0;
        public Form_ThemDonTaiTro()
        {
            InitializeComponent();
            dteNgayDatHang.EditValue = DateTime.Now.Date;  
            dteNgayGiaoHang.EditValue = DateTime.Now.Date;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_khachhang")
            {
                GetKhachHang();
            }
        }      
     
        public void GetKhachHang()
        {
            var lstKhachHang = ExecSQL.ExecProcedureDataAsDataTable("sp_KhachHangSelect");
            cboKhachHang.Properties.DataSource= lstKhachHang; 
            cboKhachHang.Properties.DisplayMember = "TenKhachHang";
            cboKhachHang.Properties.ValueMember = "ID";
        }

       
        private void cboKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            if(cboKhachHang.EditValue == DBNull.Value)
            {
                txtMaKH.Text = "";
                txtDiaChi.Text = "";             
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
        }

        public void GetCTDonDatHang()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstCTDonDatHang= ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                option = 3,
                madh=txtMaDonHang.Text
            });
            grcChiTietDonHang.DataSource = lstCTDonDatHang;

            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        public void GetSanPham()
        {
            var lstSanPham = ExecSQL.ExecProcedureDataAsDataTable("sp_SanPhamSelectGiaBan");
            cboMaSP.DataSource = lstSanPham;
            cboMaSP.DisplayMember = "ID";
            cboMaSP.ValueMember = "ID";
        }

        private void BtnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaDonHang.Focus();
            if (string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập số hóa đơn", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDonHang.Focus();
                return;
            }
            if (cboKhachHang.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng nhập chọn khách hàng", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }
           
            //Lưu Hoa1 đơn
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DonDatHangInsert", new
            {
                madh = txtMaDonHang.Text,
                ngaydathang = Convert.ToDateTime(dteNgayDatHang.EditValue),
                ngaygiaohang = Convert.ToDateTime(dteNgayGiaoHang.EditValue),
                makh = Convert.ToInt32(cboKhachHang.EditValue),
                ghichu = txtDienGiai.Text, 
                nguoicapnhat = Form_Template.TaiKhoanLogin.ToUpper(),
                MaNVSale = "",
                LoaiDonHang = 1
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
                            ExecSQL.ExecProcedureNonData("sp_ChiTietDonDatHangInsertv2", new
                            {
                                madh = txtMaDonHang.Text,                              
                                TenSanPham = dr["TenSanPham"].ToString(),
                                DonViTinh = dr["DonViTinh"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                ghichu = dr["GhiChu"].ToString(),
                                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper(),
                                dongia = dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                            });
                            break;
                        case DataRowState.Modified:
                            ExecSQL.ExecProcedureNonData("sp_ChiTietDonDatHangUpdate", new
                            {
                                madh = txtMaDonHang.Text,
                                masp = dr["MaSP"].ToString(),
                                soluong = dr["SoLuong"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SoLuong"]),
                                dongia = dr["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["DonGia"]),
                                ghichu = dr["GhiChu"].ToString(),
                                NguoiSua = Form_Template.TaiKhoanLogin.ToUpper(),
                                thanhtien = dr["ThanhTien"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ThanhTien"]),
                            });
                            break;                            
                    }
                    count += 1;
                }
                if (count > 0)
                {                   
                   //Gửi dữ liệu
                   var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_dondathang"
                   };
                    msgBroker.Publish();
                    GetCTDonDatHang();
                    XtraMessageBox.Show("Thêm mới đơn đặt hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GetCTDonDatHang();
                    //Gửi dữ liệu
                    var msgBroker = new MessageBroker
                    {
                        data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        task = "them_dondathang"
                    };
                    msgBroker.Publish();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //Lưu chi tiết hóa đơn

        }
      
        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
            {
            if (string.IsNullOrEmpty(txtMaDonHang.Text))
            {
                XtraMessageBox.Show("Bạn vui lòng nhập vào mã đơn hàng để tiếp tục.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDonHang.Focus();
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
                case "TenSanPham":
                    view.SetRowCellValue(i, "Code", "00");
                    view.SetRowCellValue(i, "MaDonHang", txtMaDonHang.Text);                  
                    view.SetRowCellValue(i, "TenSanPham", e.Value);
                    view.SetRowCellValue(i, "SoLuong", 1);
                    view.SetRowCellValue(i, "DonGia", 0);
                    view.SetRowCellValue(i, "ThanhTien", 0);
                    break;
                case "SoLuong": 
                    view.SetRowCellValue(i, "SoLuong", e.Value);
                    view.SetRowCellValue(i, "ThanhTien", Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i,"DonGia")));                   
                    break;
                case "DonGia":
                    view.SetRowCellValue(i, "DonGia", e.Value);
                    view.SetRowCellValue(i, "ThanhTien", Convert.ToDecimal(e.Value) * Convert.ToDecimal(view.GetRowCellValue(i, "SoLuong")));
                    break;
            }
        }

        private void txtMaHD_Leave(object sender, EventArgs e)
        {
            GetCTDonDatHang();
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetCTDonDatHang();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa chi tiết đơn hàng (" + txtMaDonHang.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiTietDonDatHangDelete", new
            {
                madh = txtMaDonHang.Text,
                masp= intMaSP
            });
            if (data.errcode == 1)
            {
                GetCTDonDatHang();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //var i = gridView1.FocusedRowHandle;
            //if (i < 0) { return; }
            //intMaSP = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaSP"));
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                option = 3,
                madh = txtMaDonHang.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 39;
            var frm = new Form_Report();
            frm.Show();
        }

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
            cboKhachHang.EditValue = null;
            var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = 6});
            txtMaDonHang.Text = data.ToString();
            GetCTDonDatHang();
        }
                  
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DonDatHang_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietHoaDonByMaHD", new { madh = txtMaDonHang.Text });
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không có dữ liệu để xuất Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

       

        private void Form_ThemDonDatHang_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);          
            GetKhachHang();
            GetSanPham();
            if (Form_Template.Edit == true) //
            {
                DataTable lstHD = ExecSQL.ExecProcedureDataAsDataTable("sp_DonDatHangSelectMaDH", new
                {
                    madh = Form_Template.strMaDonDatHang
                });
                if (lstHD.Rows.Count == 0) { return; }
                cboKhachHang.EditValue = Convert.ToInt32(lstHD.Rows[0]["MaKH"]);
                txtMaDonHang.Text = lstHD.Rows[0]["MaDonHang"].ToString();
                dteNgayDatHang.EditValue = Convert.ToDateTime(lstHD.Rows[0]["NgayDatHang"]);
                dteNgayGiaoHang.EditValue = Convert.ToDateTime(lstHD.Rows[0]["NgayGiaoHang"]);
                txtDienGiai.Text = lstHD.Rows[0]["GhiChu"].ToString();
                txtMaDonHang.Properties.ReadOnly = true;

                ///Load chi tiết hòa đơn
                GetCTDonDatHang();
            }
            else
            {
                var data = ExecSQL.ExecProcedureSacalar("spGetMaTuDong", new { option = 6 });
                txtMaDonHang.Text = data.ToString();
                GetCTDonDatHang();
            }
        }

        private void Form_ThemDonDatHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.strMaDonDatHang = "";
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (e.Column == colXoa)
            {
                var dgr = XtraMessageBox.Show($"Có muốn xóa chi tiết đơn hàng ({txtMaDonHang.Text}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChiTietDonDatHangDeletev2", new
                {                  
                    Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id"))
                });
                if (data.errcode == 1)
                {
                    GetCTDonDatHang();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}