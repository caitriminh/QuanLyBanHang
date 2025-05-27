using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace VisualStudio2019.Class
{
    public class Ultis
    {
       
        //Đọc dữ liệu từ file app.config
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        //Ghi dữ liệu từ file app.config
        public static void SetSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }


        public static string GetMd5Hash(string input)
        {
            // Sử dụng lớp MD5 để tạo đối tượng mã hóa
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển đổi chuỗi input thành mảng byte
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Mã hóa mảng byte
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi mảng byte thành chuỗi hexa
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); // "X2" để định dạng mỗi byte thành 2 ký tự hexa
                }
                return sb.ToString();
            }
        }

        
    }
}
