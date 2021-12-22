using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace WindowsFormsApp
{
    public partial class FormThongTinHangMoi : Form
    {
        public FormThongTinHangMoi()
        {
            InitializeComponent();
            cmbLoaiHang.SelectedIndex = 0;
            cmbĐVT.SelectedIndex = 0;
            txtMaMH.Text = Matudong();
            cmbQuayhang.SelectedIndex = 0;
        }
        List<LoaiHangDTO> list1;

        private void btnX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        private void Lammoi()
        {
            txtTenMH.Text = "Nhập tên mặt hàng";
            txtTenMH.ForeColor = Color.Silver;
            cmbLoaiHang.SelectedIndex = 0;
            cmbĐVT.SelectedIndex = -1;

        }



        private string Matudong()
        {
            string query = "select MaMH from MatHang";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "SP001";
            }
            else
            {
                int k;
                ma = "SP";
                k = dt.Rows.Count + 3;
                k++;
                if (k < 10)
                {
                    ma = ma + "00";
                }
                else if (k >= 10 && k < 100)
                {
                    ma = ma + "0";
                }
                else if (k >= 100 && k < 1000)
                {
                    ma = ma + " ";
                }
                ma = ma + k.ToString();

            }
            return ma;
        }

        private void cmbLoaiHang_Click(object sender, EventArgs e)
        {
            list1 = LoaiHangBUS.Intance.getListLoaiHang();
            cmbLoaiHang.DataSource = list1;
            cmbLoaiHang.DisplayMember = "TenLH";
            cmbLoaiHang.ValueMember = "MaLH";
        }


        string temp2;
        string temp3;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DataTable dt = QuayHangBUS.Intance.TimKiemQH(cmbQuayhang.Text);
            if (dt.Rows.Count > 0)
            {
                temp3 = dt.Rows[0]["MaQH"].ToString();
            }

            DataTable dt1 = DonViTinhBUS.Intance.TimKiemDVT(cmbĐVT.Text);
            if (dt1.Rows.Count > 0)
            {
                temp2 = dt1.Rows[0]["MaDVT"].ToString();
            }
            if (string.IsNullOrEmpty(txtMaMH.Text))
            {
                MessageBox.Show("Tên mặt hàng không được trống");
            }
            else
            {
                if (LuuHH(txtMaMH.Text, txtTenMH.Text, temp2, 0, 1, temp,temp3))
                {
                    MessageBox.Show("Lưu thông tin hàng thành công");
                    txtMaMH.Text = Matudong();
                    Lammoi();
                }
                else
                    MessageBox.Show("Không thể lưu thông tin này");
            }
        }




        private bool LuuHH(string mh, string tenh, string madv, int sl, int dg, string maLH,string MaQH)
        {
            // Convert datetime to date SQL Server 
            string query = String.Format(" insert into MatHang (MaMH,TenMH,MaDVT,SoLuong,GiaBan,MaLH,MaQH)  values('{0}',N'{1}',N'{2}','{3}','{4}','{5}','{6}')", mh, tenh, madv, sl, dg, maLH,MaQH);
            DataProvider.Instance.ExecuteQuery(query);
            return true;
        }


        private string temp;
        private void cmbLoaiHang_SelectedIndexChanged_1(object sender, EventArgs e)
        {


            DataTable dt = MatHangBUS.Intance.TimKiemLH(cmbLoaiHang.Text);
            if (dt.Rows.Count > 0)
            {
                temp = dt.Rows[0]["MaLH"].ToString();
            }

        }

        private void txtTenMH_Click(object sender, EventArgs e)
        {
            txtTenMH.Text = "";
            txtTenMH.ForeColor = Color.Black;
        }

        private void cmbĐVT_Click(object sender, EventArgs e)
        {
            list2 = getListDVT();
            cmbĐVT.DataSource = list2;
            cmbĐVT.ValueMember = "MaDVT";
            cmbĐVT.DisplayMember = "TenDVT";
        }


        public List<DonViTinhDTO> getListDVT()
        {
            string query = "select * from DonViTinh";
            List<DonViTinhDTO> list = new List<DonViTinhDTO>();
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                DonViTinhDTO product = new DonViTinhDTO(item);
                list.Add(product);
            }
            return list;
        }
        List<DonViTinhDTO> list2;

        private void cmbQuayhang_Click(object sender, EventArgs e)
        {
            list3 = getListQuayHang();
            cmbQuayhang.DataSource = list3;
            cmbQuayhang.ValueMember = "MaQH";
            cmbQuayhang.DisplayMember = "TenQH";
        }


        public List<QuayHangDTO> getListQuayHang()
        {
            string query = "select * from QuayHang";
            List<QuayHangDTO> list = new List<QuayHangDTO>();
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                QuayHangDTO product = new QuayHangDTO(item);
                list.Add(product);
            }
            return list;
        }
        List<QuayHangDTO> list3;

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}




