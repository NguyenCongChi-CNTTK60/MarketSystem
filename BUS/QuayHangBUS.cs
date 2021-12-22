using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class QuayHangBUS
    {
        private static QuayHangBUS instance;

        public QuayHangBUS()
        {
        }

        public static QuayHangBUS Intance
        {
            get { if (instance == null) instance = new QuayHangBUS(); return instance; }
            set => instance = value;
        }


        public DataTable TimKiemQH(string tk)
        {
            return QuayHangDAO.Intance.TimKiemQH(tk);
        }
    }
}
