using System.Data;

namespace DTO
{
    public class MatHangDTO
    {
 
        private string maMH, tenMH, maDVT, maQH;
        private int soLuong, giaBan;

        public MatHangDTO()
        {

        }
        public MatHangDTO(DataRow row)
        {
            this.MaMH = row["MaMH"].ToString();
            this.TenMH = row["TenMH"].ToString();
            this.MaDVT = row["MaDVT"].ToString();
            this.GiaBan = (int)row["GiaBan"];
            this.SoLuong = (int)row["SoLuong"];
        }

        public string MaMH { get => maMH; set => maMH = value; }
        public string TenMH { get => tenMH; set => tenMH = value; }
        public string MaDVT { get => maDVT; set => maDVT = value; }
        public string MaQH { get => maQH; set => maQH = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int GiaBan { get => giaBan; set => giaBan = value; }
    }
}

