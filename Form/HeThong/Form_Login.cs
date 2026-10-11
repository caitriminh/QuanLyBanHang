using DevExpress.XtraEditors;
using QuanLyBanHang.Class;
using SimpleBroker;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_Login : XtraForm
    {

        public Form_Login()
        {
            InitializeComponent();                
        }

      
        private void btnDong_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập vào tên đăng nhập", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.EditValue.ToString()))
            {
                XtraMessageBox.Show("Vui lòng nhập vào mật khẩu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }
           
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Login>("sp_Login", new
            {
                tendn=txtTenDangNhap.Text,              
                mk = Ultis.GetMd5Hash(txtMatKhau.Text)
            });
            if (data == null)
            {
                XtraMessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Text = "";
                txtTenDangNhap.Text = "";
                txtTenDangNhap.Focus();
                return;
            }
            if (data.TenDangNhap.Length>0)
            {
                Form_Template.TaiKhoanLogin = data.TenDangNhap;
                Form_Template.intMaNV = data.MaNV;
                Form_Template.QuyenHan = data.NhomQuyen;
                Form_Template.strHoTen = data.TenNhanVien;
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "login"
                };
                msgBroker.Publish();
                //XtraMessageBox.Show("Đăng nhập thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                XtraMessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
       
        public void ReadConfig()
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
                        if (i == 1)
                            ExecSQL.serverName = ClsAes.Decrypt(line, Form_CauHinh.password);
                        else if (i == 2)
                            ExecSQL.dataName = ClsAes.Decrypt(line, Form_CauHinh.password);
                        else if (i == 3)
                            ExecSQL.uid = ClsAes.Decrypt(line, Form_CauHinh.password);
                        else if (i == 4)
                            ExecSQL.passwordData = ClsAes.Decrypt(line, Form_CauHinh.password);
                        i++;

                    }
                }
                ExecSQL.CONNECTION_STRINGS = $"server={ExecSQL.serverName};uid={ExecSQL.uid};database={ExecSQL.dataName};password={ExecSQL.passwordData};";
    }
            else
            {
                XtraMessageBox.Show("Không tìm thấy thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            //Dọc cấu hình
            ReadConfig();
            string imagePath = Application.StartupPath + @"\logo\background.jpg";
            if (File.Exists(imagePath))
            {
                // Sử dụng PictureEdit để load ảnh
                picLogo.Image = Image.FromFile(imagePath);
            }
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            Form_CauHinh frm = new Form_CauHinh();
            frm.ShowDialog();
        }
    }
}