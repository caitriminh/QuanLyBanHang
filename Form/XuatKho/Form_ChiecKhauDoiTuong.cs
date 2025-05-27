using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Microsoft.VisualBasic;
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
    public partial class Form_ChiecKhauDoiTuong : XtraForm
    {
       
        public Form_ChiecKhauDoiTuong()
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
                Menu = 17
            });
            if (data == null) { return; }
            BtnThem.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnOptionIn.Enabled = data.InAn;
        }

      
        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_doituongchieckhau")
            {
                GetDoiTuongChiecKhau(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
            }
        }

        public void GetDoiTuongChiecKhau(DateTime tungay, DateTime denngay)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstGiaBan =  ExecSQL.ExecProcedureDataAsDataTable("sp_DoiTuongChiecKhauSelect", new
            {
                tungay,
                denngay
            });
            grcChiecKhau.DataSource = lstGiaBan;
            lbl_ID.DataBindings.Clear();
            lbl_MaKH.DataBindings.Clear();

            lbl_ID.DataBindings.Add("text", lstGiaBan, "Id");
            lbl_MaKH.DataBindings.Add("text", lstGiaBan, "MaKH");
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDoiTuongChiecKhau(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_Template.intIDDoiTuongCK = 0;
            Form_ThemDoiTuongChiecKhau frm = new Form_ThemDoiTuongChiecKhau();
            frm.ShowDialog();
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa đối tượng chiếc khấu này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DoiTuongChiecKhauDelete", new
            {
                ID = Convert.ToInt32(lbl_ID.Text)
            });
            if (data.errcode == 1)
            {
                GetDoiTuongChiecKhau(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDDoiTuongCK = Convert.ToInt32(lbl_ID.Text);
            Form_Template.Edit = true;
            Form_ThemDoiTuongChiecKhau frm = new Form_ThemDoiTuongChiecKhau();
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
            xtraSaveFileDialog1.FileName = "DoiTuongChiecKhau_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcChiecKhau.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void grcChiecKhau_DoubleClick(object sender, EventArgs e)
        {
            if (BtnSua.Enabled)
            {
                Form_Template.intIDDoiTuongCK = Convert.ToInt32(lbl_ID.Text);
                Form_Template.Edit = true;
                Form_ThemDoiTuongChiecKhau frm = new Form_ThemDoiTuongChiecKhau();
                frm.ShowDialog();
            }           
        }

        public void GetKy()
        {
            cboKy2.DataSource = DateTime.Now.GetDateOfWeek();
            cboKy2.DisplayMember = "name";
            cboKy2.ValueMember = "id";
            cboKy.EditValue = 5;
        }

        private void Form_ChiecKhauDoiTuong_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetKy();
        }

        private void CboKy_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowSelected = (DataRowView)cboKy2.GetRowByKeyValue(cboKy.EditValue);
            dteTuNgay.EditValue = Convert.ToDateTime(rowSelected.Row["from"]);
            dteDenNgay.EditValue = Convert.ToDateTime(rowSelected.Row["to"]);
            GetDoiTuongChiecKhau(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var i = gridView1.FocusedRowHandle;
            if (i < 0) { return; }
            if(e.Column == colAdd)
            {              
                Form_Template.intIDDoiTuongCK = Convert.ToInt32(lbl_ID.Text);
                Form_Template.intIDMaKH = Convert.ToInt32(lbl_MaKH.Text);
                Form_Template.tungay = Convert.ToDateTime(dteTuNgay.EditValue);
                Form_Template.denngay = Convert.ToDateTime(dteDenNgay.EditValue);
                Form_ThemPhieuChiCK frm = new Form_ThemPhieuChiCK();
                frm.ShowDialog();
            }
            else if(e.Column == colCopy)
            {
                var dgrResult = XtraMessageBox.Show("Bạn có muốn sao chép đối tượng nhận chiếc khấu sang tháng sau không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dgrResult != DialogResult.Yes) { return; }
                var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_DoiTuongChiecKhauCopy", new
                {
                    Id = Convert.ToInt32(lbl_ID.Text),
                    NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
                });
                if(data.errcode == 1)
                {
                    GetDoiTuongChiecKhau(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
                    XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TienChiecKhau", new
            {
                MaKH = lbl_MaKH.Text,
                IdCK = lbl_ID.Text,
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 18;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnPrintA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TienChiecKhau", new
            {
                MaKH = lbl_MaKH.Text,
                IdCK = lbl_ID.Text,
                tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                denngay = Convert.ToDateTime(dteDenNgay.EditValue)
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 32;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDoiTuongChiecKhau(Convert.ToDateTime(dteTuNgay.EditValue), Convert.ToDateTime(dteDenNgay.EditValue));
        }

        private void BtnPrintLetter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = gridView1.GetSelectedRows();
            if (selectedRows.Length == 0) { return; }

            xtraSaveFileDialog1.Filter = "Document Word Files|*.doc;*.docx";
            RtfInputData.LoadDocument(Application.StartupPath + "\\config\\MauIn_PhongBi_v1.docx", DocumentFormat.OpenXml);

            foreach (var rowHandle in selectedRows)
            {
                if (rowHandle < 0) continue; // Bỏ qua dòng không hợp lệ

                MailMergeOptions myMergeOptions = RtfInputData.Document.CreateMailMergeOptions();

                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_DoiTuongChiecKhauSelect", new
                {
                    tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                    denngay = Convert.ToDateTime(dteDenNgay.EditValue),
                    Id = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Id"))
                });

                myMergeOptions.DataSource = dt;
                myMergeOptions.MergeMode = MergeMode.NewSection;

                string fileName = gridView1.GetRowCellValue(rowHandle, "NguoiNhan").ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                xtraSaveFileDialog1.FileName = fileName;

                if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    RtfInputData.Document.MailMerge(myMergeOptions, xtraSaveFileDialog1.FileName, DocumentFormat.OpenXml);
                    Process.Start(xtraSaveFileDialog1.FileName);
                }
            }

        }

        private void btnPrintLetterv2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = gridView1.GetSelectedRows();
            if (selectedRows.Length == 0) { return; }

            xtraSaveFileDialog1.Filter = "Document Word Files|*.doc;*.docx";
            RtfInputData.LoadDocument(Application.StartupPath + "\\config\\MauIn_PhongBi_v2.docx", DocumentFormat.OpenXml);

            foreach (var rowHandle in selectedRows)
            {
                if (rowHandle < 0) continue; // Bỏ qua dòng không hợp lệ

                MailMergeOptions myMergeOptions = RtfInputData.Document.CreateMailMergeOptions();

                DataTable dt = ExecSQL.ExecProcedureDataAsDataTable("sp_DoiTuongChiecKhauSelect", new
                {
                    tungay = Convert.ToDateTime(dteTuNgay.EditValue),
                    denngay = Convert.ToDateTime(dteDenNgay.EditValue),
                    Id = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Id"))
                });

                myMergeOptions.DataSource = dt;
                myMergeOptions.MergeMode = MergeMode.NewSection;

                string fileName = gridView1.GetRowCellValue(rowHandle, "NguoiNhan").ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                xtraSaveFileDialog1.FileName = fileName;

                if (xtraSaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    RtfInputData.Document.MailMerge(myMergeOptions, xtraSaveFileDialog1.FileName, DocumentFormat.OpenXml);
                    Process.Start(xtraSaveFileDialog1.FileName);
                }
            }
        }
    }
}