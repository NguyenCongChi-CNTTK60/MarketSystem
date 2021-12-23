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
    public partial class UC_DangKyTaiKhoan : UserControl
    {

        private string MaNV;
        public UC_DangKyTaiKhoan()
        {
            InitializeComponent();
            cmbGioiTinh.SelectedIndex = 0;
            MaNV = Matudong();
        }


        private void lblDangNhap_Click(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Hide();
        }



        string query = "select Manv from Nhanvien";
        private string Matudong()
        {

            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "NV01";
            }
            else
            {
                int k;
                ma = "NV";
                k = Convert.ToInt32(dt.Rows.Count);
                k++;
                if (k < 10)
                {
                    ma = ma + "0";
                }
                else if (k >= 10 && k < 100)
                {
                    ma = ma + "";
                }
                else if (k >= 100 && k < 1000)
                {
                    ma = ma + " ";
                }
                ma = ma + k.ToString();

            }
            return ma;
        }

        private void chkHienThiMK_CheckedChanged(object sender, EventArgs e)
        {

        }

   




        private void LamMoi()
        {
            txtTenDangNhap.Text = "VD: chi";
            txtTennv.Text = "VD: Nguyễn Công Chí";
            txtDiachi.Text = "An Chấn, Tuy An, Phú Yên";
            txtMatkhau.Text = "****";
            txtSĐT.Text = "VD: 0328644258";
            cmbGioiTinh.SelectedIndex = 0;
            txtTennv.ForeColor = Color.Silver;
            txtSĐT.ForeColor = Color.Silver;
            txtDiachi.ForeColor = Color.Silver;
            txtTenDangNhap.ForeColor = Color.Silver;
            txtMatkhau.ForeColor = Color.Silver;

        }

       


        private bool check_data()
        {
            if (string.IsNullOrEmpty(txtTennv.Text))
            {

                //lblThongbao.Text = "Vui lòng nhập tên của bạn";
                
                return false;

            }
            else


            if (string.IsNullOrEmpty(txtSĐT.Text))
            {

                //lblThongbao.Text = "Vui lòng nhập SĐT của bạn";
                
                return false;
            }
            else



            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {

                //lblThongbao.Text = "Vui lòng nhập tên đăng nhập";
               
                return false;
            }
            else


            if (string.IsNullOrEmpty(txtMatkhau.Text))
            {

                //lblThongbao.Text = "Vui lòng nhập mật khẩu";
                
                return false;
            }


            return true;
        }

        private void lblDangNhap_Click_1(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Hide();
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Hide();
        }

        private void txtTennv_Click_1(object sender, EventArgs e)
        {
            txtTennv.Text = "";
            txtTennv.ForeColor = Color.Black;
        }

        private void txtTennv_Leave(object sender, EventArgs e)
        {
            if (txtTennv.Text == "")
            {
                txtTennv.Text = "VD: Nguyễn Văn A";
                txtTennv.ForeColor = Color.Silver;
            }
        }

        private void txtSĐT_Click(object sender, EventArgs e)
        {
            txtSĐT.Text = "";
            txtSĐT.ForeColor = Color.Black;
        }

        private void txtSĐT_Leave_1(object sender, EventArgs e)
        {
            if (txtSĐT.Text == "")
            {
                txtSĐT.Text = "VD: 0328644258";
                txtSĐT.ForeColor = Color.Silver;
            }
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_Click_1(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = "";
            txtTenDangNhap.ForeColor = Color.Black;
        }

        private void txtTenDangNhap_Leave_1(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                txtTenDangNhap.Text = "VD: VanA";
                txtTenDangNhap.ForeColor = Color.Silver;
            }
        }

        private void txtDiachi_Click_1(object sender, EventArgs e)
        {
            txtDiachi.Text = "";
            txtDiachi.ForeColor = Color.Black;
        }

        private void txtDiachi_Leave(object sender, EventArgs e)
        {
            if (txtDiachi.Text == "")
            {
                txtDiachi.Text = "VD: An Chấn, Tuy An, Phú Yên";
                txtDiachi.ForeColor = Color.Silver;
            }
        }

        private void txtMatkhau_Click_1(object sender, EventArgs e)
        {
            txtMatkhau.Text = "";
            txtMatkhau.ForeColor = Color.Black;
        }

        private void txtMatkhau_Leave(object sender, EventArgs e)
        {
            if (txtMatkhau.Text == "")
            {
                txtMatkhau.Text = "*****";
                txtMatkhau.ForeColor = Color.Silver;
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string query = "select TenDangNhap as [TenDangNhap] from Nhanvien where TenDangnhap = '" + txtTenDangNhap.Text + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);



            if (check_data() == true)
            {
                if (dt.Rows.Count > 0)
                {

                    MessageBox.Show("Tên đăng nhập đã tồn tại","Thông báo");

                }
                else
                {

                    if (NhanVienBUS.Intance.themNV(MaNV, txtTennv.Text, cmbGioiTinh.Text, txtDiachi.Text, txtSĐT.Text, txtTenDangNhap.Text, txtMatkhau.Text))
                    {
                        MessageBox.Show("Đăng ký thành công", "Thông báo");
                        LamMoi();
                        MaNV = Matudong();
                    }

                    //lblThongbao.Text = "Đăng ký tài khoản thất bại";

                }
            }
        }
    }
}

