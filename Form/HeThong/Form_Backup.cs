using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Layout;
using QuanLyBanHang.Class;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_Backup : XtraForm
    {
        public Form_Backup()
        {
            InitializeComponent();
        }
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            BackupData(TxtUrl.Text + "/QLBanHang" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak");
        }

        public void BackupData(string filePath)
        {
            string databaseName = ExecSQL.dataName.Replace("]", "]]");
            string backupFile = filePath.Replace("'", "''");

            string backupQuery = string.Format(@"
            BACKUP DATABASE [{0}]
            TO DISK = N'{1}'
            WITH FORMAT,
                 MEDIANAME = 'SQLServerBackups',
                 NAME = N'Full Backup of {0}';",
                databaseName,
                backupFile);

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ExecSQL.CONNECTION_STRINGS))
                using (SqlCommand command = new SqlCommand(backupQuery, connection))
                {
                    command.CommandTimeout = 0;
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                XtraMessageBox.Show("Sao lưu cơ sở dữ liệu thành công.","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Sao lưu cơ sở dữ liệu không thành công:\n\n" + ex.Message,
                    "Cảnh Báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void ReadURL()
        {
            string path = Application.StartupPath + "/config/config.txt";
            // Kiểm tra xem tệp có tồn tại không
            if (File.Exists(path))
            {
                // Đọc từng dòng từ tệp
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    int i = 1;
                    while ((line = reader.ReadLine()) != null)
                    {
                       if (i == 5)
                            TxtUrl.Text = ClsAes.Decrypt(line,Form_CauHinh.password);
                        i++;

                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Không tìm thấy thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnBrowse_Click(object sender, EventArgs e)        {
           
            if (xtraFolderBrowserDialog1.ShowDialog() != DialogResult.OK) { return; }
            TxtUrl.Text = xtraFolderBrowserDialog1.SelectedPath;
        }

        private void Form_Backup_Load(object sender, EventArgs e)
        {
            ReadURL();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}