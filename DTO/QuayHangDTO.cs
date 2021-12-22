using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QuayHangDTO
    {
        private string maQH,tenQH;


        public QuayHangDTO()
        {

        }
        public QuayHangDTO(DataRow row)
        {
            this.MaQH = row["MaQH"].ToString();
            this.TenQH = row["TenQH"].ToString();

        }

        public string MaQH { get => maQH; set => maQH = value; }
        public string TenQH { get => tenQH; set => tenQH = value; }
    }
}
