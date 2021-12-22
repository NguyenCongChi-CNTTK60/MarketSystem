using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp;

namespace DAO
{
    public class DonViTinhDAO
    {
        private static DonViTinhDAO instance;

        public DonViTinhDAO()
        {
        }

        public static DonViTinhDAO Intance
        {
            get { if (instance == null) instance = new DonViTinhDAO(); return instance; }
            set => instance = value;
        }



        public DataTable TimKiemDVT(string tk)
        {
            string query = "select MaDVT from DonViTinh where TenDVT = N'" + tk + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

    }
}
