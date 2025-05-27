using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace QuanLyBanHang.Form.BaoCao
{
    public partial class Form_BaoCaoDongTien : XtraForm
    {
        public Form_BaoCaoDongTien()
        {
            InitializeComponent();
        }

        public void GetNam()
        {
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_Nam");
            cboNam2.DataSource= data;
            cboNam2.DisplayMember = "Nam";
            cboNam2.ValueMember = "Nam";
            cboNam.EditValue = DateTime.Now.Year;
        }

        private void Form_BaoCaoDongTien_Load(object sender, EventArgs e)
        {
            GetNam();
        }

        public void GetBaoCaoDongTien()
        {
            if (cboNam.EditValue == null) { return; }
            var data = ExecSQL.ExecProcedureDataAsDataTable("sp_BaoCaoDongTien", new
            {
                nam = Convert.ToInt32(cboNam.EditValue)
            }); ;
            pvtBCDongTien.DataSource = data;
        }

        private void cboNam_EditValueChanged(object sender, EventArgs e)
        {
            GetBaoCaoDongTien();
        }

        private void BtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            xtraSaveFileDialog1.Filter = "Excel files |*.xlsx";
            xtraSaveFileDialog1.FileName = "BaoCaoDongTien_" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
            if (xtraSaveFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            pvtBCDongTien.ExportToXlsx(xtraSaveFileDialog1.FileName);
            // Mở file với ứng dụng mặc định
            Process.Start(new ProcessStartInfo(xtraSaveFileDialog1.FileName) { UseShellExecute = true });
        }

        private void BtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}