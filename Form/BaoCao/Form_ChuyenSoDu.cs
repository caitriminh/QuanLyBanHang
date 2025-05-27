using DevExpress.XtraEditors;
using SimpleBroker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudio2019.Class;

namespace VisualStudio2019.Form
{
    public partial class Form_ChuyenSoDu : XtraForm
    {
        public Form_ChuyenSoDu()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            var dgrResult = XtraMessageBox.Show("Có muốn chuyển tồn kho sang tháng sau không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dgrResult != DialogResult.Yes) { return; }
            var data = ExecSQL.ExecProcedureDataFistOrDefault<Status>("sp_ChuyenSoDu", new
            {
                kymoi = Convert.ToDateTime(dteKy.EditValue).ToString("yyyy-MM-01")
            });
            if (data.errcode == 1)
            {
                //Gửi dữ liệu
                var msgBroker = new MessageBroker
                {
                    data = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    task = "chuyensodu"
                };
                msgBroker.Publish();
                XtraMessageBox.Show(data.errmsg, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(data.errmsg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_ChuyenSoDu_Load(object sender, EventArgs e)
        {
            dteKy.EditValue = DateTime.Now.ToString("yyyy-MM-01");
        }
    }
}