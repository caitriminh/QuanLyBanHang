using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_DonDatHang : XtraForm
    {
        readonly int option = 1;
        public Form_DonDatHang()
        {
            InitializeComponent();
            dteTuNgay.EditValue = DateTime.Now.Date.ToString("01/MM/yyyy");
            dteDenNgay.EditValue = DateTime.Now.Date;

            btninlenhsanxuat.Visibility = BarItemVisibility.Never;
            btnInGiaoNhan.Visibility = BarItemVisibility.Never;

            btnInDonHangA5.Visibility = BarItemVisibility.Always;
            BtnIn.Visibility = BarItemVisibility.Always;
            btnInDonHangTaiTro.Visibility = BarItemVisibility.Always;
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 37
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnOption.Enabled = data.InAn;           
        }

        /// <summary>
        /// Load danh sách hóa đơn khi có sự thay đổi form_themhoadon
        /// </summary>
        /// <param name="value"></param>
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_dondathang")
            {
                GetDondatHang(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        public void GetDondatHang(int option, DateTime tungay, DateTime denngay)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstCTDonDatHang =  ExecSQL.ExecProcedureDataAsDataTable("sp_DonDatHangSelectAll", new
            {
                option,
                tungay,
                denngay
            });
            grcDonDatHang.DataSource = lstCTDonDatHang;
            if (lstCTDonDatHang.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lbl_MaDonHang.DataBindings.Clear();
            lbl_MaDonHang.DataBindings.Add("text", lstCTDonDatHang, "MaDonHang");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                GetDondatHang(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else
            {
                GetCTDonHang();
            }
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemDonDatHang frm = new Form_ThemDonDatHang();
            frm.ShowDialog();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            if (Convert.ToInt32(gridView1.GetRowCellValue(i, "LoaiDonHang")) ==0)
            {
                var lstCTDonHang = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
                {
                    option = 1,
                    madh = gridView1.GetRowCellValue(i, "MaDonHang").ToString()
                });
                grcChiTietDonDatHang.DataSource = lstCTDonHang;
            }
            else
            {
                var lstCTDonHang2 = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
                {
                    option = 3,
                    madh = gridView1.GetRowCellValue(i, "MaDonHang").ToString()
                });
                grcChiTietDonDatHang.DataSource = lstCTDonHang2;
            }
         
        }

        private void BtnXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa hóa đơn (" + lbl_MaDonHang.Text + ") này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DonDatHangDelete", new
            {
                madh = lbl_MaDonHang.Text
            });
            if (data.errcode == 1)
            {
                GetDondatHang(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Template.strMaDonDatHang = lbl_MaDonHang.Text;
            Form_Template.Edit = true;
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            if (Convert.ToInt32( gridView1.GetRowCellValue(i, "LoaiDonHang")) == 0)
            {
                Form_ThemDonDatHang frm = new Form_ThemDonDatHang();
                frm.ShowDialog();
            }
            else if (Convert.ToInt32(gridView1.GetRowCellValue(i, "LoaiDonHang")) == 1)
            {
                Form_ThemDonTaiTro frm = new Form_ThemDonTaiTro();
                frm.ShowDialog();
            }
            else
            {
                Form_ThemDonTang frm = new Form_ThemDonTang();
                frm.ShowDialog();
            }
        }

        private void BtnDong_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                madh = lbl_MaDonHang.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 21;
            var frm = new Form_Report();
            frm.Show();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các đơn hàng trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gridView1.GetSelectedRows();
            var joinMaDH = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "MaDonHang"));

            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "DonDatHang_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss"); ;
            if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var dt = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new { madh = joinMaDH });
                ExportExcel.ExportExcelFromDataTable(dt, xtraSaveFileDialog1.FileName);
            }
        }

        private void btnTim_ItemClick(object sender, ItemClickEventArgs e)
        {      
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                GetDondatHang(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else
            {
                GetCTDonHang();
            }
        }


        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);       
            GetDondatHang(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void grcDonDatHang_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) return;
            Form_Template.strMaDonDatHang = lbl_MaDonHang.Text;
            Form_Template.Edit = true;
            Form_ThemDonDatHang frm = new Form_ThemDonDatHang();
            frm.ShowDialog();
        }

        private void Form_DonDatHang_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {           
            if(e.Column == colXacNhan)
            {
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn xác nhận đơn hàng {lbl_MaDonHang.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dgrResult != DialogResult.Yes) return;
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DonDatHangXacNhan", new
                {
                    MaDonHang = lbl_MaDonHang.Text,
                    Status = 2,
                    NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if(data.errcode == 1)
                {
                    GetDondatHang(option, Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnInDonHangA5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                madh = lbl_MaDonHang.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 33;
            var frm = new Form_Report();
            frm.Show();
        }

        public void GetCTDonHang()
        {
            var x = gvCTDonHang.FocusedRowHandle;
            var y = gvCTDonHang.TopRowIndex;
            var lstCTDonHang = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                option = 2,
                FromDate = Convert.ToDateTime(dteTuNgay.EditValue),
                ToDate = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            gcCTDonDatHang.DataSource = lstCTDonHang;
            gvCTDonHang.FocusedRowHandle = x;
            gvCTDonHang.TopRowIndex= y;
        }


        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {          
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                GetCTDonHang();
                btninlenhsanxuat.Visibility = BarItemVisibility.Always;
                btnInGiaoNhan.Visibility = BarItemVisibility.Always;
                btnInDonHangA5.Visibility = BarItemVisibility.Never;
                BtnIn.Visibility = BarItemVisibility.Never;
                btnInDonHangTaiTro.Visibility = BarItemVisibility.Never;
                btnReportDonHangQuatang.Visibility = BarItemVisibility.Never;
                BtnXoa.Enabled = false;
                BtnThem.Enabled = false;
                BtnSua.Enabled = false;
            }
            else
            {
                btninlenhsanxuat.Visibility = BarItemVisibility.Never;
                btnInGiaoNhan.Visibility = BarItemVisibility.Never;
                btnInDonHangA5.Visibility = BarItemVisibility.Always;
                BtnIn.Visibility = BarItemVisibility.Always;
                btnInDonHangTaiTro.Visibility = BarItemVisibility.Always;
                btnReportDonHangQuatang.Visibility = BarItemVisibility.Always;
                GetPhanQuyen();
            }
        }

        private void btninlenhsanxuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCTDonHang.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các đơn hàng trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gvCTDonHang.GetSelectedRows();
            var joinMaDH = string.Join(",", from r in selectedRows select gvCTDonHang.GetRowCellValue(Convert.ToInt32(r), "MaDonHang"));
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                option = 1,
                madh = joinMaDH
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 37;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnInGiaoNhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvCTDonHang.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn các đơn hàng trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedRows = gvCTDonHang.GetSelectedRows();
            var joinMaDH = string.Join(",", from r in selectedRows select gvCTDonHang.GetRowCellValue(Convert.ToInt32(r), "MaDonHang"));
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                option = 1,
                madh = joinMaDH
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 38;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnInDonHangTaiTro_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                option = 3,
                madh = lbl_MaDonHang.Text
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

        private void btnThemDHTaiTro_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemDonTaiTro frm = new Form_ThemDonTaiTro();
            frm.ShowDialog();
        }

        private void btnThemDonHangTang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemDonTang frm = new Form_ThemDonTang();
            frm.ShowDialog();
        }

        private void btnReportDonHangQuatang_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_ChiTietDonDatHang", new
            {
                option = 3,
                madh = lbl_MaDonHang.Text
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 43;
            var frm = new Form_Report();
            frm.Show();
        }
    }
}