using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_CauHinh : XtraForm
    {
        public static string password = "triminh";
        public Form_CauHinh()
        {
            InitializeComponent();         
        }
             

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "/config/config.txt";
            // Tạo mảng chứa các giá trị cần mã hóa
            string[] dataToEncrypt = {
                txtMayChu.Text,
                txtDatabase.Text,
                txtTaiKhoan.Text,
                txtPassword.Text,
                txtURL.Text,
                txtURLAnh.Text,
                txtDuongDanLuuAnhNhanVien.Text
            };

            // Mã hóa tất cả giá trị trong mảng
            var encryptedData = dataToEncrypt.Select(value => ClsAes.Encrypt(value, password)).ToArray();

            // Ghi thông tin đã mã hóa vào tệp
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var encryptedLine in encryptedData)
                {
                    writer.WriteLine(encryptedLine);
                }
            }

            // Lưu tên đường dẫn của ảnh
            //var result = ExecSQL.ExecProcedureNonData("sp_ThongTinUpdateURL", new
            //{
            //    DuongDanLuuHinh = txtURLAnh.Text
            //});

            // Thông báo lưu thành công
            XtraMessageBox.Show("Đã lưu cấu hình thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void Form_ThongTin_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "/config/config.txt";

            // Kiểm tra xem tệp có tồn tại không
            if (File.Exists(path))
            {
                // Tạo mảng các TextBox tương ứng với thứ tự dòng trong tệp
                TextEdit[] textBoxes = { txtMayChu, txtDatabase, txtTaiKhoan, txtPassword, txtURL, txtURLAnh, txtDuongDanLuuAnhNhanVien };

                // Đọc từng dòng từ tệp
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    int i = 0;

                    // Đọc từng dòng và gán giá trị vào TextBox tương ứng
                    while ((line = reader.ReadLine()) != null && i < textBoxes.Length)
                    {
                        textBoxes[i].Text = ClsAes.Decrypt(line, password);
                        i++;
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Không tìm thấy thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (xtraFolderBrowserDialog1.ShowDialog() != DialogResult.OK) { return; }
            txtURL.Text = xtraFolderBrowserDialog1.SelectedPath;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            if (xtraFolderBrowserDialog1.ShowDialog() != DialogResult.OK) { return; }
            txtURLAnh.Text = xtraFolderBrowserDialog1.SelectedPath;
        }

        private void btnChonNV_Click(object sender, EventArgs e)
        {
            if (xtraFolderBrowserDialog1.ShowDialog() != DialogResult.OK) { return; }
            txtDuongDanLuuAnhNhanVien.Text = xtraFolderBrowserDialog1.SelectedPath;
        }
    }
}