using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp;

namespace DAO
{
    public class QuayHangDAO
    {
        private static QuayHangDAO instance;

        public QuayHangDAO()
        {
        }

        public static QuayHangDAO Intance
        {
            get { if (instance == null) instance = new QuayHangDAO(); return instance; }
            set => instance = value;
        }


        public DataTable TimKiemQH(string tk)
        {
            string query = "select MaQH from QuayHang where TenQH = N'" + tk + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

    }
}
