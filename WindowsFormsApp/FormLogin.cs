using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
            lblCanhBao.Text = "";

        }






        FormTrangChu f = new FormTrangChu();
       


       // FormSignUp n = new FormSignUp();
        private void lblDangky_Click(object sender, EventArgs e)
        {
            
        }

      

        

       

        


        bool Login(string userName, string passWord)
        {
            return NhanVienBUS.Intance.Login(userName, passWord);
        }

       
       

     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text;
            string passWord = txtMatKhau.Text;
            if (Login(tenDangNhap, passWord))
            {
                FormTrangChu1 n = new FormTrangChu1(txtTenDangNhap.Text);
                n.Show();
                this.Hide();
            }
            else
                lblCanhBao.Text = "Sai tài khoản hoặc mật khẩu!";
            lblCanhBao.ForeColor = Color.Brown;
        }

        private void chkHienThiMK_CheckedChanged(object sender, EventArgs e)
        {

            if (chkHienThiMK.Checked == true)
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
                txtMatKhau.PasswordChar = '*';
        }

        private void txtTenDangNhap_TextChanged_1(object sender, EventArgs e)
        {
            lblCanhBao.Text = "";
        }

        private void txtTenDangNhap_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = "";
            txtTenDangNhap.ForeColor = Color.Black;
        }

        private void txtTenDangNhap_Leave(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                txtTenDangNhap.Text = "VD: VanA";
                txtTenDangNhap.ForeColor = Color.Silver;
            }
        }

        private void txtMatKhau_Click(object sender, EventArgs e)
        {
            txtMatKhau.Text = "";
            txtMatKhau.ForeColor = Color.Black;
        }

        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "")
            {
                txtMatKhau.Text = "*****";
                txtMatKhau.ForeColor = Color.Silver;
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            lblCanhBao.Text = "";
        }

        private void addUC(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(userControl);
            userControl.BringToFront();
        }




        private void lblDangky_Click_1(object sender, EventArgs e)
        {
            UC_DangKyTaiKhoan f = new UC_DangKyTaiKhoan();
            addUC(f);
        }

        private void lblBanQuenMK_Click_1(object sender, EventArgs e)
        {
            FormSDT f = new FormSDT();
            f.Show();
            this.Hide();
        }
    }
}
