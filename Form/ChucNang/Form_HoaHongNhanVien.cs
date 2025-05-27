using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_HoaHongNhanVien : XtraForm
    {
       
        public Form_HoaHongNhanVien()
        {
            InitializeComponent();
            dteTuNgay.EditValue = DateTime.Now.Date.ToString("01/MM/yyyy");
            dteDenNgay.EditValue = DateTime.Now.Date;
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 22
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnPrint.Enabled = data.InAn;
        }

         private void OnNext(MessageBroker value)
        {
            if (value.task == "them_hoahong")
            {
                GetData();
            }
        }

        public void GetData()
        {
           if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                GetHoaHong();
                BtnXoa.Enabled = true; 
            }
            else
            {
                GetHoaHongv2();
                BtnXoa.Enabled = false;                        
            }
        }

        public void GetHoaHong()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstGiaBan =  ExecSQL.ExecProcedureDataAsDataTable("spGetHoaHong", new
            {
                Option=1,
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            grcHoaHongNhanVien.DataSource = lstGiaBan;
            if(lstGiaBan.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }          
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }


        public void GetHoaHongv2()
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstGiaBan = ExecSQL.ExecProcedureDataAsDataTable("spGetHoaHong", new
            {
                Option = 3,
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            gcHoaHongNhanVien2.DataSource = lstGiaBan;
            if (lstGiaBan.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tìm thấy dữ liệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }          
            gridView2.FocusedRowHandle = y;
            gridView2.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDHoaHongNhanVien = 0;
            Form_ThemHoaHongNhanVien frm = new Form_ThemHoaHongNhanVien();
            frm.ShowDialog();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                var i = gridView1.FocusedRowHandle;
                var dgr = XtraMessageBox.Show($"Có muốn xóa hoa hồng của nhân viên ({gridView1.GetRowCellValue(i, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_HoaHongDelete", new
                {
                    Id = Convert.ToInt32(gridView1.GetRowCellValue(i,"Id"))
                });
                if (data.errcode == 1)
                {
                    GetData();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                var j = gridView2.FocusedRowHandle;
                var dgr = XtraMessageBox.Show($"Có muốn xóa hoa hồng của nhân viên ({gridView2.GetRowCellValue(j, "TenNhanVien")}) này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgr != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_HoaHongDelete", new
                {
                    Id = Convert.ToInt32(gridView2.GetRowCellValue(j, "Id"))
                });
                if (data.errcode == 1)
                {
                    GetData();
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                int i = gridView1.FocusedRowHandle;
                if (i < 0){
                    return;
                }
                Form_Template.intIDHoaHongNhanVien = Convert.ToInt32(gridView1.GetRowCellValue(i,"Id"));               
            }
            else
            {
                int j = gridView2.FocusedRowHandle;
                if (j < 0)
                {
                    return;
                }
                Form_Template.intIDHoaHongNhanVien = Convert.ToInt32(gridView2.GetRowCellValue(j, "Id"));
            }
            Form_Template.Edit = true;
            Form_ThemHoaHongNhanVien frm = new Form_ThemHoaHongNhanVien();
            frm.ShowDialog();

        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        
        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "HoaHongNhanVien_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                gcHoaHongNhanVien2.ExportToXlsx(xtraSaveFileDialog1.FileName);
            }
            else
            {
                grcHoaHongNhanVien.ExportToXlsx(xtraSaveFileDialog1.FileName);
            }
          
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }


        private void BtnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }

        private void CboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetData();
        }
      
        private void Form_HoaHongNhanVien_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        private void grcHoaHongNhanVien_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled == false) { return; }
            Form_Template.intIDHoaHongNhanVien = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemHoaHongNhanVien frm = new Form_ThemHoaHongNhanVien();
            frm.ShowDialog();
        }

        private void BtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                if (gridView1.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("Vui lòng chọn danh sách nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRows = gridView1.GetSelectedRows();
                var joinId = string.Join(",", from r in selectedRows select gridView1.GetRowCellValue(Convert.ToInt32(r), "Id"));

                Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("spGetHoaHong", new
                {
                    Option = 1,
                    tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                    denngay = Convert.ToDateTime(dteDenNgay.EditValue),
                    Id = joinId
                });
                if (Form_Template.dtReport.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Form_Template.IntReport = 42;
                var frm = new Form_Report();
                frm.Show();
            }
            else
            {
                if (gridView2.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("Vui lòng chọn danh sách nhân viên.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRows = gridView2.GetSelectedRows();
                var joinId = string.Join(",", from r in selectedRows select gridView2.GetRowCellValue(Convert.ToInt32(r), "Id"));

                Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("spGetHoaHong", new
                {
                    Option = 3,
                    tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                    denngay = Convert.ToDateTime(dteDenNgay.EditValue),
                    Id = joinId
                });
                if (Form_Template.dtReport.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Form_Template.IntReport = 42;
                var frm = new Form_Report();
                frm.Show();
            }
        }

        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            if (e.Column == colAdd)
            {
                Form_Template.intIDHoaHongNhanVien = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id"));
                Form_Template.Edit = true;
                Form_CapNhatDoanhSo frm = new Form_CapNhatDoanhSo();
                frm.ShowDialog();
            }
        }

        private void BtnOption_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new Form_ThietLapHoaHong();
            frm.ShowDialog();
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            GetData();
        }
    }
}