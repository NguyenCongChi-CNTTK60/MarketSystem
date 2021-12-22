using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class DonViTinhBUS
    {
        private static DonViTinhBUS instance;

        public DonViTinhBUS()
        {
        }

        public static DonViTinhBUS Intance
        {
            get { if (instance == null) instance = new DonViTinhBUS(); return instance; }
            set => instance = value;
        }

        public DataTable TimKiemDVT(string tk)
        {
            return DonViTinhDAO.Intance.TimKiemDVT(tk);
        }
    }
}
