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
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp
{
    public partial class UC_TrangChu : UserControl
    {

        private string tennv;
        public UC_TrangChu(string tennv)
        {
            InitializeComponent();
            getDataChart();
            getdataLable();
            this.tennv = tennv;
            lblTenNhanVien.Text = tennv;
            // HidesubMenu();
            cmbTonKho.SelectedIndex = 0;
            cmbDoanhthu.SelectedIndex = 0;
            TopSP();
            DoanhThuTrongNgay();
        }



        private void getDataChart()
        {
            chart1.Titles.Clear();
            DateTime today = DateTime.Now;
            DateTime bd = new DateTime(today.Year);
            
            string query = "select Year(NgayTao) as [Nam],sum(HoaDon.TongTien) AS 'TongTien' from HoaDon where Year(NgayTao) between  '2020' and '2021'  group by  Year(NgayTao) order by TongTien asc";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            chart1.DataSource = data;
            chart1.Series["Doanh Thu"].XValueMember = "Nam";
            chart1.Series["Doanh Thu"].YValueMembers = "TongTien";
           // chart1.Titles.Add("THỐNG KÊ DOANH THU");
            chart1.Series["Doanh Thu"].Color = System.Drawing.Color.FromArgb(0, 48, 135);
            //chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.Series[0].ChartType = SeriesChartType.Column;
        }





       
        private void getdataLable()
        {
            string tkKH = "select count(*) AS [SoLuong] from KhachHang";
            DataTable kh = DataProvider.Instance.ExecuteQuery(tkKH);
            //lblKhachhang.Text = kh.Rows[0]["SoLuong"].ToString();

            tkKH = "select count(*) AS [SoLuong] from HoaDon";
            kh = DataProvider.Instance.ExecuteQuery(tkKH);
            //lblHoadon.Text = kh.Rows[0]["SoLuong"].ToString();

            tkKH = "select count(*) AS [SoLuong] from Nhanvien";
            kh = DataProvider.Instance.ExecuteQuery(tkKH);
           // lblNhanvien.Text = kh.Rows[0]["SoLuong"].ToString();

            tkKH = "select(sum(Tongtien) / count(MaHD)) as [Doanh thu] from HoaDon";
            kh = DataProvider.Instance.ExecuteQuery(tkKH);
            int doanhthu = Int32.Parse(kh.Rows[0]["Doanh thu"].ToString());

            //lblDoanhthu.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", doanhthu) + " đ";


            tkKH = "select count(SoLuong) as [Duoidinhmuc] from MatHang Where SoLuong <= 50";
            kh = DataProvider.Instance.ExecuteQuery(tkKH);
            if(kh.Rows.Count > 0)
            {
                lbldinhmuc.Text = kh.Rows[0]["Duoidinhmuc"].ToString();
            }


            tkKH = "select sum(SoLuong) as [SL] from MatHang";
            kh = DataProvider.Instance.ExecuteQuery(tkKH);
            if (kh.Rows.Count > 0)
            {
                lblslton.Text = kh.Rows[0]["SL"].ToString();
            }


            tkKH = "select sum(SoLuong * GiaBan) as [giatri] from MatHang";
            kh = DataProvider.Instance.ExecuteQuery(tkKH);
            if (kh.Rows.Count > 0)
            {
                int giatri = Int32.Parse(kh.Rows[0]["giatri"].ToString());
                lblGiatri.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", giatri) + " đ";
            }
        }


        DateTime today = DateTime.Now;
        private void TopSP()
        {

            DateTime ngbd = new DateTime(today.Year, 1, 1);
            DateTime ngkt = new DateTime(today.Year, 12,30);
            string query = "select top 4 sum(ChiTietHD.SoLuong) as [Top 1], ChiTietHD.MaMH,TenMH from ChiTietHD inner join HoaDon on HoaDon.MaHD = ChiTietHD.MaHD inner join MatHang on MatHang.MaMH = ChiTietHD.MaMH where NgayTao between '"+ngbd+"' and '"+ngkt+"' group by ChiTietHD.MaMH,TenMH order by[Top 1] desc";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count >= 4)
            {
                lblSPTOP1.Text = dt.Rows[0]["TenMH"].ToString();
                lblsanpham.Text = dt.Rows[0]["Top 1"].ToString();
                lbltop2.Text = dt.Rows[1]["TenMH"].ToString();
                lblsltop2.Text = dt.Rows[1]["Top 1"].ToString();
                lbltop3.Text = dt.Rows[2]["TenMH"].ToString();
                lblsltop3.Text = dt.Rows[2]["Top 1"].ToString();
                lbltop4.Text = dt.Rows[3]["TenMH"].ToString();
                lblsltop4.Text = dt.Rows[3]["Top 1"].ToString();

            }
        }

        private void cmbTonKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbTonKho.Text == "Hôm nay")
            {
                DateTime ngbd = new DateTime(today.Year, today.Month,today.Day);
                DateTime ngkt = new DateTime(today.Year, today.Month, today.Day);
                string query = "select top 4 sum(ChiTietHD.SoLuong) as [Top 1], ChiTietHD.MaMH,TenMH from ChiTietHD inner join HoaDon on HoaDon.MaHD = ChiTietHD.MaHD inner join MatHang on MatHang.MaMH = ChiTietHD.MaMH where NgayTao between '" + ngbd + "' and '" + ngkt + "' group by ChiTietHD.MaMH,TenMH order by[Top 1] desc";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                if (dt.Rows.Count >= 4)
                {
                    lblSPTOP1.Text = dt.Rows[0]["TenMH"].ToString();
                    lblsanpham.Text = dt.Rows[0]["Top 1"].ToString();
                    lbltop2.Text = dt.Rows[1]["TenMH"].ToString();
                    lblsltop2.Text = dt.Rows[1]["Top 1"].ToString();
                    lbltop3.Text = dt.Rows[2]["TenMH"].ToString();
                    lblsltop3.Text = dt.Rows[2]["Top 1"].ToString();
                    lbltop4.Text = dt.Rows[3]["TenMH"].ToString();
                    lblsltop4.Text = dt.Rows[3]["Top 1"].ToString();

                }
                else
                
                    LamMoi();
                
                   
            }else if (cmbTonKho.Text == "Hôm qua")
            {
                DateTime ngbd = new DateTime(today.Year, today.Month, today.Day - 1);
                DateTime ngkt = new DateTime(today.Year, today.Month, today.Day - 1);
                string query = "select top 4 sum(ChiTietHD.SoLuong) as [Top 1], ChiTietHD.MaMH,TenMH from ChiTietHD inner join HoaDon on HoaDon.MaHD = ChiTietHD.MaHD inner join MatHang on MatHang.MaMH = ChiTietHD.MaMH where NgayTao between '" + ngbd + "' and '" + ngkt + "' group by ChiTietHD.MaMH,TenMH order by[Top 1] desc";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                if (dt.Rows.Count >= 4)
                {
                    lblSPTOP1.Text = dt.Rows[0]["TenMH"].ToString();
                    lblsanpham.Text = dt.Rows[0]["Top 1"].ToString();
                    lbltop2.Text = dt.Rows[1]["TenMH"].ToString();
                    lblsltop2.Text = dt.Rows[1]["Top 1"].ToString();
                    lbltop3.Text = dt.Rows[2]["TenMH"].ToString();
                    lblsltop3.Text = dt.Rows[2]["Top 1"].ToString();
                    lbltop4.Text = dt.Rows[3]["TenMH"].ToString();
                    lblsltop4.Text = dt.Rows[3]["Top 1"].ToString();

                }
                else
                
                    LamMoi();
                
                   
            }else if(cmbTonKho.Text == "Tuần này")
            {
                DateTime ngbd = new DateTime(today.Year, today.Month, today.Day);
                DateTime ngkt = new DateTime(today.Year, today.Month, today.Day + 7);
                string query = "select top 4 sum(ChiTietHD.SoLuong) as [Top 1], ChiTietHD.MaMH,TenMH from ChiTietHD inner join HoaDon on HoaDon.MaHD = ChiTietHD.MaHD inner join MatHang on MatHang.MaMH = ChiTietHD.MaMH where NgayTao between '" + ngbd + "' and '" + ngkt + "' group by ChiTietHD.MaMH,TenMH order by[Top 1] desc";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                if (dt.Rows.Count >= 4)
                {
                    lblSPTOP1.Text = dt.Rows[0]["TenMH"].ToString();
                    lblsanpham.Text = dt.Rows[0]["Top 1"].ToString();
                    lbltop2.Text = dt.Rows[1]["TenMH"].ToString();
                    lblsltop2.Text = dt.Rows[1]["Top 1"].ToString();
                    lbltop3.Text = dt.Rows[2]["TenMH"].ToString();
                    lblsltop3.Text = dt.Rows[2]["Top 1"].ToString();
                    lbltop4.Text = dt.Rows[3]["TenMH"].ToString();
                    lblsltop4.Text = dt.Rows[3]["Top 1"].ToString();

                }
                else
                
                    LamMoi();
                
                 
            }else if(cmbTonKho.Text == "Tháng này")
            {
                DateTime ngbd = new DateTime(today.Year, today.Month, 1);
                DateTime ngkt = new DateTime(today.Year, today.Month, 30);
                string query = "select top 4 sum(ChiTietHD.SoLuong) as [Top 1], ChiTietHD.MaMH,TenMH from ChiTietHD inner join HoaDon on HoaDon.MaHD = ChiTietHD.MaHD inner join MatHang on MatHang.MaMH = ChiTietHD.MaMH where NgayTao between '" + ngbd + "' and '" + ngkt + "' group by ChiTietHD.MaMH,TenMH order by[Top 1] desc";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                if (dt.Rows.Count >= 4)
                {
                    lblSPTOP1.Text = dt.Rows[0]["TenMH"].ToString();
                    lblsanpham.Text = dt.Rows[0]["Top 1"].ToString();
                    lbltop2.Text = dt.Rows[1]["TenMH"].ToString();
                    lblsltop2.Text = dt.Rows[1]["Top 1"].ToString();
                    lbltop3.Text = dt.Rows[2]["TenMH"].ToString();
                    lblsltop3.Text = dt.Rows[2]["Top 1"].ToString();
                    lbltop4.Text = dt.Rows[3]["TenMH"].ToString();
                    lblsltop4.Text = dt.Rows[3]["Top 1"].ToString();

                }
                else
                
                    LamMoi();
                
            }else if(cmbTonKho.Text == "Năm nay")
            {
                TopSP();
            }
        }

        private void LamMoi()
        {
            lblSPTOP1.Text = "";
            lblsanpham.Text = "";
            lbltop2.Text = "";
            lblsltop2.Text = "";
            lbltop3.Text = "";
            lblsltop3.Text = "";
            lbltop4.Text = "";
            lblsltop4.Text = "";
            labeltop1.Text = "";
            labeltop2.Text = "";
            labeltop3.Text = "";
            labeltop4.Text = "";
        }


        private void DoanhThuTrongNgay()
        {
            DateTime dt = new DateTime(today.Year, today.Month, today.Day);
            string query = "select sum(TongTien) as [TongTien],count(MaHD) as [LuotMuaSam]  from HoaDon where NgayTao = '" + dt + "'";
            string query1 = "select sum(SoLuong) as [HangBan]  from  ChiTietHD inner join HoaDon on HoaDon.MaHD = ChiTietHD.MaHD where NgayTao = '" + dt + "'";
            DataTable data1 = DataProvider.Instance.ExecuteQuery(query1);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0 && data1.Rows.Count > 0)
            {
                int TongTien;
                string Tien = data.Rows[0]["TongTien"].ToString();
                if (!string.IsNullOrEmpty(Tien))
                {
                    TongTien = Int32.Parse(Tien);
                    lbldoanhthu.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongTien) + " đ";
                    lblDonhangMoi.Text = data.Rows[0]["LuotMuaSam"].ToString();

                   lblTonghang.Text = data1.Rows[0]["HangBan"].ToString();
                   
                }
            }
        }


        /*
  private void getDataChart()
  {
   chart1.Titles.Clear();
   DateTime today = DateTime.Now;
   DateTime bd = new DateTime(today.Year, today.Month, 1);
   DateTime kt = bd.AddMonths(1).AddDays(-1);
   string query = "EXEC USP_ThongKeDoanhThuTrongThang @ngaybd , @ngaykt";
   DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { bd, kt });
   chart1.DataSource = data;
   chart1.Series["Doanh Thu"].XValueMember =  "NGAY";
   chart1.Series["Doanh Thu"].YValueMembers = "TongTien";
   chart1.Titles.Add("THỐNG KÊ DOANH THU");
   chart1.Series["Doanh Thu"].Color = System.Drawing.Color.FromArgb(59, 82, 132);
   //chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
   //chart1.ChartAreas[0].AxisX.Minimum = 0;
   //chart1.Series[0].ChartType = SeriesChartType.Column;
}

private void lblTenNhanVien_Click(object sender, EventArgs e)
{
   showsubMenu(pnlTTTaiKhoan);
}


private void addUC(UserControl uc)
{
   uc.Dock = DockStyle.Fill;
   panel1.Controls.Clear();
   panel1.Controls.Add(uc);
   uc.BringToFront();
}

private void btnTK_Click(object sender, EventArgs e)
{
   UC_ThongTinhTaiKhoan f = new UC_ThongTinhTaiKhoan(lblTenNhanVien.Text);
   addUC(f);
}


private void HidesubMenu()
{
   if (pnlTTTaiKhoan.Visible == true)
   {
       pnlTTTaiKhoan.Visible = false;
   }
}


private void showsubMenu(Panel subMenu)
{
   if (subMenu.Visible == false)
   {
       HidesubMenu();
       subMenu.Visible = true;
   }
   else
       subMenu.Visible = false;
} */
    }
}
