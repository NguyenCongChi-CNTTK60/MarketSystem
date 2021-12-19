using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace WindowsFormsApp
{
    public partial class FormSDT : Form
    {
        public FormSDT()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void pnlma_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuaylai_Click_1(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Hide();
        }


        private void addUC(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            pnlSDT.Controls.Clear();
            pnlSDT.Controls.Add(uc);
            uc.BringToFront();
        }

       

        private void btnTiepTuc_Click_1(object sender, EventArgs e)
        {
            string query = "select SDT as [TenDangNhap] from Nhanvien where SDT = '" + txtSDT.Text + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            if (!string.IsNullOrEmpty(txtSDT.Text))
            {
                if (dt.Rows.Count > 0)
                {
                    UC_XacNhanMa uC_XacNhanMa = new UC_XacNhanMa(txtSDT.Text);
                    addUC(uC_XacNhanMa);
                }
                else
                {
                    MessageBox.Show("Số điện thoại chưa đúng", "Thông báo");
                }

            }
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            FormLogin F = new FormLogin();
            F.Show();
            this.Hide();
        }
    }
}

