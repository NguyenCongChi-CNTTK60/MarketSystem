using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using BUS;
using DTO;

namespace WindowsFormsApp
{
    public partial class UC_BanHang : UserControl
    {
        private string manv, tennv;

 
        int i;
        private void cmbTensp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTenHang.SelectedIndex >= 0)
            {
                i = cmbTenHang.SelectedIndex;
                DataTable dt1 = LoaiHangBUS.Intance.TimKiemMaMH(cmbTenHang.Text);

                if (dt1.Rows.Count > 0)
                {
                    lblMahh.Text = dt1.Rows[0]["MaMH"].ToString();
                    lblGiaban.Text = dt1.Rows[0]["GiaBan"].ToString();

                }
            }
        }

        public UC_BanHang(string manv, string tennv)  // string manv, string tennv
        {
            InitializeComponent();

            list = getListSanPham();
            cmbTenHang.DataSource = list;
            cmbTenHang.ValueMember = "MaMH";
            cmbTenHang.DisplayMember = "TenMH";

            lblMahd.Text = Matudong();
            cmbLoaiHang.SelectedIndex = 0;

            DateTime dt = DateTime.Now;
            dtpNgayban.Value = dt;

            this.manv = manv;
            lblMaNV.Text = manv;
            this.tennv = tennv;
            lblTennv.Text = tennv;

           
        }


        List<LoaiHangDTO> list4;
        private void cmbLoaiHang_Click_1(object sender, EventArgs e)
        {
            list4 = LoaiHangBUS.Intance.getListLoaiHang();
            cmbLoaiHang.DataSource = list4;
            cmbLoaiHang.DisplayMember = "TenLH";
            cmbLoaiHang.ValueMember = "MaLH";
        }


        //
        // Tạo mã hóa đơn tự động
        // chí

        private string Matudong()
        {
            string query = "select MaHD from HoaDon";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "HD001";
            }
            else
            {
                int k;
                ma = "HD";
                //k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 3));
                k = dt.Rows.Count;
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
                    ma = ma + "";
                }
                ma = ma + k.ToString();

            }
            return ma;
        }


        public List<MatHangDTO> getListSanPham()
        {
            string query = "select * from MatHang";
            List<MatHangDTO> list = new List<MatHangDTO>();
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                MatHangDTO product = new MatHangDTO(item);
                list.Add(product);
            }
            return list;
        }
        List<MatHangDTO> list;


        int tongTien;
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(cmbTenHang.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn phải chọn sản phẩm", "Thông báo");
            }
            else
            if ( txtSoLuong.Value > 0)
            {
                bool check = false;
                foreach (ListViewItem item in lvSanPhamBan.Items)
                {
                    if (item.SubItems[0].Text == cmbTenHang.SelectedValue.ToString())
                    {
                        check = true;
                    }
                    if (check)
                    {
                        int temp = Int32.Parse(item.SubItems[2].Text) + Int32.Parse(txtSoLuong.Value.ToString());
                        item.SubItems[2].Text = temp.ToString();
                        item.SubItems[3].Text = (Int32.Parse(item.SubItems[2].Text) * Int32.Parse(lblGiaban.Text)).ToString();
                        break;
                    }
                }


                int gia = Int32.Parse(lblGiaban.Text) * Int32.Parse(txtSoLuong.Value.ToString());
                if (!check)
                {
                    string[] arr = new string[5];
                    arr[0] = lblMahh.Text;
                    arr[1] = cmbTenHang.Text;
                    arr[2] = txtSoLuong.Value.ToString();
                    arr[3] = lblGiaban.Text;
                    arr[4] = gia.ToString();
                    ListViewItem listViewItem = new ListViewItem(arr);
                    lvSanPhamBan.Items.Add(listViewItem);
                }
                tongTien += gia;
                lblTienbangso.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##00}", tongTien) + " đ";
               // lbTienBangChu.Text = ChuyenDoiTien.Instance.So_chu(tongTien);
                Tinhtienhoantra();
                resetInfoProduct();

            }
            else
                MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo");
        }

        private void btnXoaHang_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvSanPhamBan.Items.Count; i++) //duyệt tất cả các item trong list
            {
                if (lvSanPhamBan.Items[i].Checked)//nếu item đó dc check
                {
                    string tien = lvSanPhamBan.Items[i].SubItems[4].Text.ToString();
                    tongTien -= Int32.Parse(tien);
                    lblTienbangso.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##00}", tongTien) + " đ";
                    
                    lvSanPhamBan.Items[i].Remove();//xóa item đó đi
                    Tinhtienhoantra();
                    i--;

                }
            }
        }







        KhachHangDTO khachHang = new KhachHangDTO()
        {
            MaKH = null
        };



      

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTimkiem.Text))
            {
                string id = txtTimkiem.Text;
                string query = "select * from KhachHang where SDT ='" + id + "'";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                if(data.Rows.Count > 0)
                {
                    lblMaKH.Text = data.Rows[0]["MaKH"].ToString();
                    lblTenkh.Text = data.Rows[0]["TenKH"].ToString();
                }
                else
                {
                    lblTenkh.Text = "Khách hàng mới";
                    lblMaKH.Text = "KH00";
                }


               
                lblMaKH.ForeColor = Color.Black;           
                lblTenkh.ForeColor = Color.Black;
                
            }
            else
            {


                lblTenkh.Text = "Khách hàng mới";
                lblMaKH.Text = "KH00";
                lblTenkh.ForeColor = Color.Black;
                lblMaKH.ForeColor = Color.Black;
            }
        }



        private void Tinhtienhoantra()
        {
            if (!string.IsNullOrEmpty(txtTienkhachdua.Text))
            {
                int tienkhachdua = Int32.Parse(txtTienkhachdua.Text);
                int tienhoantra = tienkhachdua - tongTien;
                lblTienHoanTra.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tienhoantra) + " đ";
                lblTienKhachDua.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tienkhachdua) + " đ";
            }
            else
                lblTienHoanTra.Text = "0đ";
        }


        private void txtTienkhachdua_TextChanged(object sender, EventArgs e)
        {
            Tinhtienhoantra();
        }

        private void cmbLoaiHang_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string tk = cmbLoaiHang.Text;
            DataTable dt = LoaiHangBUS.Intance.TimKiemGG(tk);
            DataTable dt1 = LoaiHangBUS.Intance.TimKiemMaMH(tk);
            if (dt.Rows.Count > 0 && dt.Rows.Count > 0)
            {
                cmbTenHang.DataSource = dt;
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimkiem.Text))
            {
                MessageBox.Show("Chưa có thông tin của khách hàng");
            }
            else
            if (lvSanPhamBan.Items.Count > 0)
            {
                HoaDonDTO hd = new HoaDonDTO();
                hd.MaHD = lblMahd.Text;
                hd.MaKH = lblMaKH.Text;
                hd.NgayTao = dtpNgayban.Value;
                hd.MaNV = lblMaNV.Text;
                hd.TongTien = tongTien;




                if (LuuHD(hd))   // lưu hóa đơn
                {
                    //FormInHD formInHD = new FormInHD(lblMakh.Text);
                    foreach (ListViewItem item in lvSanPhamBan.Items)
                    {
                        LuuDH(hd.MaHD, item.SubItems[0].Text, Int32.Parse(item.SubItems[2].Text), Int32.Parse(item.SubItems[3].Text));  //lưu chi tiết hóa đơn
                        string query = "update MatHang set SoLuong = SoLuong - " + Int32.Parse(item.SubItems[2].Text) + "where MaMH = '" + item.SubItems[0].Text + "'";  // cập nhật lại số lượng 
                        DataProvider.Instance.ExecuteQuery(query);

                    }
                    FormInHoaDon formInHoaDon = new FormInHoaDon(lblMahd.Text, lblTienKhachDua.Text,lblTienHoanTra.Text, lblTienbangso.Text);
                    formInHoaDon.Show();
                    lvSanPhamBan.Items.Clear();
                    lblTienbangso.Text = "0 đ";
                    lblTienKhachDua.Text = "0đ";
                    lblTienHoanTra.Text = "0đ";                 
                    lblMaKH.Text = "KH00";
                    lblTenkh.Text = "Chưa xác định";
                    tongTien = 0;
                    lblMahd.Text = Matudong();
                    txtTienkhachdua.Text = "";
                    txtTimkiem.Text = "";

                }
               
            }
            else
            {
                MessageBox.Show("Bạn chưa có sản phẩm để thanh toán");
            }
        }


        //
        // Lưu hóa đơn
        //
        private bool LuuHD(HoaDonDTO dh)
        {
            // Convert datetime to date SQL Server 
            string query = String.Format("insert into HoaDon values('{0}','{1}','{2}','{3}','{4}')", dh.MaHD, dh.MaKH, dh.NgayTao, dh.MaNV, dh.TongTien);
            DataProvider.Instance.ExecuteQuery(query);
            return true;
        }


        private bool LuuDH(string mahd, string mahh, int sl, int dg)
        {
            string query = String.Format("insert into Chitiethd values('{0}','{1}','{2}','{3}')", mahd, mahh, sl, dg);
            DataProvider.Instance.ExecuteQuery(query);
            return true;
        }

        private void txtTimkiem_Click(object sender, EventArgs e)
        {
            txtTimkiem.Text = "";
            txtTimkiem.ForeColor = Color.Black;
        }

        private void resetInfoProduct()
        {
            lblMahh.Text = "";
            cmbTenHang.SelectedIndex = -1;
            txtSoLuong.Value = 0;
            //cmbTenmh.Text = "";
            lblGiaban.Text = "0đ";
        }
    }
}



