using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    
    public class DonViTinhDTO
    {
        private string madvt, tendvt;


        public DonViTinhDTO()
        {

        }
        public DonViTinhDTO(DataRow row)
        {
            this.Madvt = row["MaDVT"].ToString();
            this.Tendvt = row["TenDVT"].ToString();
           
        }

     
        public string Madvt { get => madvt; set => madvt = value; }
        public string Tendvt { get => tendvt; set => tendvt = value; }
    }
}
