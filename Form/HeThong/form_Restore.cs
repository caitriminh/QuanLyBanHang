using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class form_Restore : XtraForm
    {
        public form_Restore()
        {
            InitializeComponent();
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            Restore(TxtUrl.Text);
        }

        public void Restore(string filename)
        {  
            // Câu lệnh SQL để khôi phục cơ sở dữ liệu
            string restoreQuery = $@"
            USE master;
            ALTER DATABASE [{ExecSQL.dataName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE [{ExecSQL.dataName}] FROM DISK = '{filename}' WITH REPLACE;
            ALTER DATABASE [{ExecSQL.dataName}] SET MULTI_USER;";

            try
            {
                using (SqlConnection connection = new SqlConnection(ExecSQL.CONNECTION_STRINGS))
                {
                    SqlCommand command = new SqlCommand(restoreQuery, connection);
                    connection.Open();
                    command.ExecuteNonQuery();                  
                    XtraMessageBox.Show("Khôi phục cở sở dữ liệu thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {              
                XtraMessageBox.Show("Khôi phục cở sở dữ liệu không thành công.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            xtraOpenFileDialog1.Filter = "Database files |*.bak";
            xtraOpenFileDialog1.FileName = "Chọn file Database Bán Hàng";
            if (xtraOpenFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            TxtUrl.Text = xtraOpenFileDialog1.FileName;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}