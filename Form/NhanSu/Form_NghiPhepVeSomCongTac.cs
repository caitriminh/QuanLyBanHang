namespace VisualStudio2019.Form
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Base;
    using QuanLyBanHang.Class;
    using SimpleBroker;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Windows.Forms;
    using VisualStudio2019.Class;


    public partial class Form_NghiPhepVeSomCongTac : XtraForm
    {
        
        public List<NghiPhepVeSomCongTac> nghiphep = new List<NghiPhepVeSomCongTac>();       
        public Form_NghiPhepVeSomCongTac()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        /// <summary>
        /// The GetPhanQuyen
        /// </summary>
        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 61
            });
            if (data == null) { return; }
            btnDuyet.Enabled = data?.Luu ?? false;
            BtnSua.Enabled = data?.Sua ?? false;
            BtnExcel.Enabled = data?.Excel ?? false;
        }

        /// <summary>
        /// The GetDangKyNghiPhep
        /// </summary>
        /// <param name="tungay">The tungay<see cref="DateTime"/></param>
        /// <param name="denngay">The denngay<see cref="DateTime"/></param>
        private void GetDangKyNghiPhep(DateTime tungay, DateTime denngay)
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_NghiPhepSelectAll", new
            {
                tungay,
                denngay
            });
            grcDangKyNghiPhep.DataSource = data;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", data, "Id");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        /// <summary>
        /// The GetDangKyVeSom
        /// </summary>
        /// <param name="tungay">The tungay<see cref="DateTime"/></param>
        /// <param name="denngay">The denngay<see cref="DateTime"/></param>
        private void GetDangKyVeSom(DateTime tungay, DateTime denngay)
        {
            var x = gridView2.FocusedRowHandle;
            var y = gridView2.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_VeSomSelectAll", new
            {
                tungay,
                denngay
            });
            grcVeSom.DataSource = data;

            gridView2.FocusedRowHandle = x;
            gridView2.TopRowIndex = y;
        }

        /// <summary>
        /// The GetDangKyCongTac
        /// </summary>
        /// <param name="tungay">The tungay<see cref="DateTime"/></param>
        /// <param name="denngay">The denngay<see cref="DateTime"/></param>
        private void GetDangKyCongTac(DateTime tungay, DateTime denngay)
        {
            var x = gridView3.FocusedRowHandle;
            var y = gridView3.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_CongTacSelectAll", new
            {
                tungay,
                denngay
            });
            grcCongTac.DataSource = data;

            gridView3.FocusedRowHandle = x;
            gridView3.TopRowIndex = y;
        }

        /// <summary>
        /// The OnNext
        /// </summary>
        /// <param name="value">The value<see cref="MessageBroker"/></param>
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_nghiphep")
            {
                GetDangKyNghiPhep(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (value.task == "them_vesom")
            {
                GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (value.task == "them_congtac")
            {
                GetDangKyCongTac(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        /// <summary>
        /// The BtnSua_ItemClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraBars.ItemClickEventArgs"/></param>
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = true;
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                int i = gridView1.FocusedRowHandle;
                if (i < 0) { return; }
                Form_Template.intIDNghiPhep_VS_CT = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id"));
                Form_ThemDKNghiPhep frm = new Form_ThemDKNghiPhep();
                frm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                int j = gridView2.FocusedRowHandle;
                if (j < 0) { return; }
                Form_Template.intIDNghiPhep_VS_CT = Convert.ToInt32(gridView2.GetRowCellValue(j, "Id"));
                Form_ThemDKVeSom frm = new Form_ThemDKVeSom();
                frm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                int k = gridView3.FocusedRowHandle;
                if (k < 0) { return; }
                Form_Template.intIDNghiPhep_VS_CT = Convert.ToInt32(gridView3.GetRowCellValue(k, "Id"));
                Form_ThemDKCongTac frm = new Form_ThemDKCongTac();
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// The BtnNap_ItemClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraBars.ItemClickEventArgs"/></param>
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                GetDangKyNghiPhep(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                GetDangKyCongTac(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        /// <summary>
        /// The BtnDong_ItemClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraBars.ItemClickEventArgs"/></param>
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// The GetKy
        /// </summary>
        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        /// <summary>
        /// The cboKy_EditValueChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void cboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                GetDangKyNghiPhep(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                GetDangKyCongTac(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        /// <summary>
        /// The btnThem_ItemClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraBars.ItemClickEventArgs"/></param>
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDNghiPhep_VS_CT = 0;
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                Form_ThemDKNghiPhep frm = new Form_ThemDKNghiPhep();
                frm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                Form_ThemDKVeSom frm = new Form_ThemDKVeSom();
                frm.ShowDialog();
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                Form_ThemDKCongTac frm = new Form_ThemDKCongTac();
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// The btnXacNhan_ItemClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraBars.ItemClickEventArgs"/></param>
        private void btnXacNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                var i = gridView1.FocusedRowHandle;
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn xác nhận đăng ký nghỉ phép của nhân viên ({gridView1.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 1,
                        Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyNghiPhep(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView1.RowCount > 0)
                        {
                            gridView1_FocusedRowChanged(this, new FocusedRowChangedEventArgs(i,i));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                var j = gridView2.FocusedRowHandle;
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn xác nhận đăng ký về sớm của nhân viên ({gridView2.GetRowCellValue(j, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 1,
                        Id = Convert.ToInt32(gridView2.GetRowCellValue(j, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView2.RowCount > 0)
                        {
                            gridView2_FocusedRowChanged(this, new FocusedRowChangedEventArgs(j, j));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                var k = gridView3.FocusedRowHandle;
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn xác nhận đăng ký công tác của nhân viên ({gridView3.GetRowCellValue(k, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 1,
                        Id = Convert.ToInt32(gridView3.GetRowCellValue(k, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView3.RowCount > 0)
                        {
                            gridView3_FocusedRowChanged(this, new FocusedRowChangedEventArgs(k, k));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// The btnDuyet_ItemClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraBars.ItemClickEventArgs"/></param>
        private void btnDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                var i = gridView1.FocusedRowHandle;
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn duyệt đăng ký nghỉ phép của nhân viên ({gridView1.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 2,
                        Id = Convert.ToInt32(lbl_Id.Text),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyNghiPhep(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView1.RowCount > 0)
                        {
                            gridView1_FocusedRowChanged(this, new FocusedRowChangedEventArgs(i, i));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                var j = gridView2.FocusedRowHandle;
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn duyệt đăng ký về sớm của nhân viên ({gridView2.GetRowCellValue(j, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 2,
                        Id = Convert.ToInt32(gridView2.GetRowCellValue(j, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView2.RowCount > 0)
                        {
                            gridView2_FocusedRowChanged(this, new FocusedRowChangedEventArgs(j, j));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                var k = gridView3.FocusedRowHandle;
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn duyệt đăng ký công tác của nhân viên ({gridView3.GetRowCellValue(k, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 2,
                        Id = Convert.ToInt32(gridView3.GetRowCellValue(k, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView3.RowCount > 0)
                        {
                            gridView3_FocusedRowChanged(this, new FocusedRowChangedEventArgs(k, k));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// The gridView1_FocusedRowChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            nghiphep.Clear();
            nghiphep.Add(new NghiPhepVeSomCongTac()
            {
                MaNV = Convert.ToInt32(gridView1.GetRowCellValue(i, "MaNV")),
                Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id")),
                Status = Convert.ToInt32(gridView1.GetRowCellValue(i, "Status"))
            });
            if (nghiphep[0].Status == 1)
            {
                btnDuyet.Enabled = false;
                btnXacNhan.Enabled = true;
                BtnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else if (nghiphep[0].Status == 2)
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

        /// <summary>
        /// The gridView1_RowCellClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs"/></param>
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (e.Column == colKhoiPhuc && Convert.ToInt32(gridView1.GetRowCellValue(i, "Status")) > 1)
            {
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn khôi phục đăng ký nghỉ phép của nhân viên ({gridView1.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 3,
                        Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyNghiPhep(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView1.RowCount > 0)
                        {
                            gridView1_FocusedRowChanged(this, new FocusedRowChangedEventArgs(i, i));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Đăng ký nghỉ phép của nhân viên chưa xác nhận.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// The Form_NghiPhepVeSomCongTac_Load
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void Form_NghiPhepVeSomCongTac_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        /// <summary>
        /// The BtnExcel_ItemClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraBars.ItemClickEventArgs"/></param>
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                if (gridView1.SelectedRowsCount <= 0)
                {
                    XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                xtraSaveFileDialog1.FileName = "DangKyNghiPhep_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                grcDangKyNghiPhep.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                if (gridView2.SelectedRowsCount <= 0)
                {
                    XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                xtraSaveFileDialog1.FileName = "DangKyVeSom_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                grcVeSom.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                if (gridView3.SelectedRowsCount <= 0)
                {
                    XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                xtraSaveFileDialog1.FileName = "DangKyCongTac_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
                grcCongTac.ExportToXlsx(xtraSaveFileDialog1.FileName);
                Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
            }
        }

        /// <summary>
        /// The xtraTabControl1_SelectedPageChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraTab.TabPageChangedEventArgs"/></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == tab_NghiPhep)
            {
                GetDangKyNghiPhep(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (xtraTabControl1.SelectedTabPage == tab_VeSom)
            {
                GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
            else if (xtraTabControl1.SelectedTabPage == tab_CongTac)
            {
                GetDangKyCongTac(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        /// <summary>
        /// The gridView2_FocusedRowChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FocusedRowChangedEventArgs"/></param>
        private void gridView2_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var i = gridView2.FocusedRowHandle;
            if (i < 0) { return; }
            nghiphep.Clear();
            nghiphep.Add(new NghiPhepVeSomCongTac()
            {
                MaNV = Convert.ToInt32(gridView2.GetRowCellValue(i, "MaNV")),
                Id = Convert.ToInt32(gridView2.GetRowCellValue(i, "Id")),
                Status = Convert.ToInt32(gridView2.GetRowCellValue(i, "Status"))
            });
            if (nghiphep[0].Status == 1)
            {
                btnDuyet.Enabled = false;
                btnXacNhan.Enabled = true;
                BtnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else if (nghiphep[0].Status == 2)
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

        /// <summary>
        /// The gridView2_RowCellClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs"/></param>
        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = gridView2.FocusedRowHandle;
            if (e.Column == col_KhoiPhuc2 && Convert.ToInt32(gridView2.GetRowCellValue(i, "Status")) > 1)
            {
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn khôi phục đăng ký về sớm của nhân viên ({gridView2.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 3,
                        Id = Convert.ToInt32(gridView2.GetRowCellValue(i, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyVeSom(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView2.RowCount > 0)
                        {
                            gridView2_FocusedRowChanged(this, new FocusedRowChangedEventArgs(i, i));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Đăng ký về sớm của nhân viên chưa xác nhận.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// The gridView3_RowCellClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs"/></param>
        private void gridView3_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = gridView3.FocusedRowHandle;
            if (e.Column == col_KhoiPhuc3 && Convert.ToInt32(gridView3.GetRowCellValue(i, "Status")) > 1)
            {
                var dgrResult = XtraMessageBox.Show($"Bạn có muốn khôi phục đăng ký công tác của nhân viên ({gridView3.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult == DialogResult.Yes)
                {
                    var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DangKyNghiPhepDuyet", new
                    {
                        option = 3,
                        Id = Convert.ToInt32(gridView3.GetRowCellValue(i, "Id")),
                        NguoiSua = Form_Template.TaiKhoanLogin.ToUpper()
                    });
                    if (data.errcode == 1)
                    {
                        GetDangKyCongTac(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                        if (gridView3.RowCount > 0)
                        {
                            gridView2_FocusedRowChanged(this, new FocusedRowChangedEventArgs(i, i));
                        }
                        XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Đăng ký công tác của nhân viên chưa xác nhận.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// The gridView3_FocusedRowChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FocusedRowChangedEventArgs"/></param>
        private void gridView3_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var i = gridView3.FocusedRowHandle;
            if (i < 0) { return; }
            nghiphep.Clear();
            nghiphep.Add(new NghiPhepVeSomCongTac()
            {
                MaNV = Convert.ToInt32(gridView3.GetRowCellValue(i, "MaNV")),
                Id = Convert.ToInt32(gridView3.GetRowCellValue(i, "Id")),
                Status = Convert.ToInt32(gridView3.GetRowCellValue(i, "Status"))
            });
            if (nghiphep[0].Status == 1)
            {
                btnDuyet.Enabled = false;
                btnXacNhan.Enabled = true;
                BtnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else if (nghiphep[0].Status == 2)
            {
                btnDuyet.Enabled = true;
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

        private void grcCongTac_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.Edit = true;
            int k = gridView3.FocusedRowHandle;
            if (k < 0) { return; }
            Form_Template.intIDNghiPhep_VS_CT = Convert.ToInt32(gridView3.GetRowCellValue(k, "Id"));
            Form_ThemDKCongTac frm = new Form_ThemDKCongTac();
            frm.ShowDialog();
        }

        private void grcVeSom_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.Edit = true;
            int j = gridView2.FocusedRowHandle;
            if (j < 0) { return; }
            Form_Template.intIDNghiPhep_VS_CT = Convert.ToInt32(gridView2.GetRowCellValue(j, "Id"));
            Form_ThemDKVeSom frm = new Form_ThemDKVeSom();
            frm.ShowDialog();
        }

        private void grcDangKyNghiPhep_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.Edit = true;
            int j = gridView1.FocusedRowHandle;
            if (j < 0) { return; }
            Form_Template.intIDNghiPhep_VS_CT = Convert.ToInt32(gridView1.GetRowCellValue(j, "Id"));
            Form_ThemDKNghiPhep frm = new Form_ThemDKNghiPhep();
            frm.ShowDialog();
        }
    }

}
