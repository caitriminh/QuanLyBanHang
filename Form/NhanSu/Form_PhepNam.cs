using DevExpress.XtraEditors;
using System;
using VisualStudio2019.Class;
using SimpleBroker;
using QuanLyBanHang.Class;
using System.Windows.Forms;
using System.Diagnostics;

namespace VisualStudio2019.Form
{
    public partial class Form_PhepNam : XtraForm
    {
        public Form_PhepNam()
        {
            InitializeComponent();
            GetPhanQuyen();
        }

        public void GetPhanQuyen()
        {
            var data = ExecSQL.ExecProcedureDataFistOrDefault<QuyenHan>("sp_PhanQuyenSelectById", new
            {
                NhomQuyen = Form_Template.QuyenHan,
                Menu = 60
            });
            if (data == null) { return; }
            btnChuyenPhepNam.Enabled = data.Luu;          
            BtnSua.Enabled = data.Sua;
            BtnExcel.Enabled = data.Excel;
            btnCapNhatPhepNam.Enabled = data.Luu;
        }

        public void GetNam()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_GetThangNam", new
            {
                option = 1
            });
            cboNam2.DataSource = data;
            cboNam2.DisplayMember = "Nam";
            cboNam2.ValueMember = "Nam";
            cboNam.EditValue = DateTime.Now.Year;
        }

        public void GetThang()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_GetThangNam", new
            {
                option = 2,
                Nam = Convert.ToInt32(cboNam.EditValue)
            });
            cboThang2.DataSource = data;
            cboThang2.DisplayMember = "Thang";
            cboThang2.ValueMember = "Thang";
            cboThang.EditValue = DateTime.Now.Month;
        }

        private void GetPhepNam()
        {
            var x = gridView1.FocusedRowHandle;
            var y = gridView1.TopRowIndex;

            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_PhepNamSelectAll", new
            {
                Nam = Convert.ToInt32(cboNam.EditValue),
                Thang = Convert.ToInt32(cboThang.EditValue)
            });
            grcPhepNam.DataSource = data;
            lbl_Id.DataBindings.Clear();
            lbl_Id.DataBindings.Add("text", data, "Id");

            gridView1.FocusedRowHandle = x;
            gridView1.TopRowIndex = y;
        }

        private void OnNext(MessageBroker value)
        {
            if (value.task == "them_phepnam")
            {
                GetPhepNam();
            }
        }
       
        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.intIDPhepNam = Convert.ToInt32(lbl_Id.Text);
            Form_Template.Edit = true;
            Form_ThemPhepNamBS frm = new Form_ThemPhepNamBS();
            frm.ShowDialog();
        }
       
        private void BtnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetPhepNam();
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void Form_PhepNam_Load(object sender, EventArgs e)
        {
            this.Subscribe<MessageBroker>(OnNext);
            GetNam();
        }

        private void cboNam_EditValueChanged(object sender, EventArgs e)
        {
            if (cboNam.EditValue == null) { return; }
            GetThang();
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (cboNam.EditValue == null || cboThang.EditValue == null)
            {
                return;
            }
            GetPhepNam();
        }

        private void btnChuyenPhepNam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = XtraMessageBox.Show("Bạn có muốn thực hiện không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChuyenPhepNam", new
            {
                Nam = Convert.ToInt32(cboNam.EditValue),
                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
            });
            if (data.errcode == 1)
            {
                GetPhepNam();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            xtraSaveFileDialog1.FileName = "PhepNam_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            grcPhepNam.ExportToXlsx(xtraSaveFileDialog1.FileName);
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Template.dtReport = ExecSQL.ExecProcedureDataAsDataTable("sp_PhepNamSelectAll", new
            {
                Nam = Convert.ToInt32(cboNam.EditValue),
                Thang = Convert.ToInt32(cboThang.EditValue)
            });
            if (Form_Template.dtReport.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không tồn tại dữ liệu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form_Template.IntReport = 28;
            var frm = new Form_Report();
            frm.Show();
        }

        private void btnCapNhatPhepNam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = XtraMessageBox.Show("Bạn có muốn thực hiện không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_PhepNamInsert", new
            {                
                NguoiTao = Form_Template.TaiKhoanLogin.ToUpper()
            });
            if (data.errcode == 1)
            {
                GetPhepNam();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    
}
