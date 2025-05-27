using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_TonKho : XtraForm
    {
        
        public Form_TonKho()
        {
            InitializeComponent();
            dteKy.EditValue = DateTime.Now.Date.ToString("MM/yyyy");
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 21
            });
            if (data == null) { return; }
            BtnExcel.Enabled = data.Excel;
            BtnIn.Enabled = data.InAn;
            btnChuyenSoDu.Enabled = data.Luu;
            BtnXoa.Enabled = data.Xoa;
        }

        /// <summary>
        /// Load danh sách hóa đơn khi có sự thay đổi form_themhoadon
        /// </summary>
        /// <param name="value"></param>
        private void OnNext(MessageBroker value)
        {
            if (value.task == "chuyensodu")
            {
                GetTonKho(Convert.ToDateTime(dteKy.EditValue));
            }
        }

        public void GetTonKho(DateTime Ky)
        {
            var y = gridView1.FocusedRowHandle;
            var x = gridView1.TopRowIndex;
            var lstTonKho =  ExecSQL.ExecProcedureDataAsDataTable("sp_TonKhoSelectAll", new
            {                
                Ky                
            });
            grcTonKho.DataSource = lstTonKho;          
            gridView1.FocusedRowHandle = y;
            gridView1.TopRowIndex = x;
        }

        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTonKho(Convert.ToDateTime(dteKy.EditValue));
        }

        private void BtnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.Edit = false;
            Form_ThemPhieuXuat frm = new Form_ThemPhieuXuat();
            frm.ShowDialog();
        }


        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dgr = XtraMessageBox.Show("Có muốn xóa tồn kho không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dgr != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_TonKhoDelete", new
            {
                ky = Convert.ToDateTime(dteKy.EditValue).ToString("yyyy-MM-01")
            }); 
            if (data.errcode==1)
            {
                GetTonKho(Convert.ToDateTime(dteKy.EditValue));
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_TonKhoSelectAll", new
            {
                Ky = Convert.ToDateTime(dteKy.EditValue).ToString("yyyyMM01")
            }); 
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 6;
            var frm = new Form_Report();
            frm.Show();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để xuất.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "TonKho_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcTonKho.ExportToXlsx(xtraSaveFileDialog1.FileName);            
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });           
        }

        private void btnTim_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTonKho(Convert.ToDateTime(dteKy.EditValue));
        }

        private void Form_TonKho_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetTonKho(Convert.ToDateTime(dteKy.EditValue));
        }

        private void btnChuyenSoDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_ChuyenSoDu frm = new Form_ChuyenSoDu();
            frm.ShowDialog();
        }
    }
}