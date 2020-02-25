using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.CarsCatalog
{
    class Singleton
    {
        private static Singleton instance;
        public static SqlConnection SqlConnection { get; private set; }
        private Singleton()
        {
            SqlConnection = new SqlConnection("Data Source=ALEKS;Initial Catalog=usersdb;Integrated Security=True");
            SqlConnection.Open();
        }
        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }
    }
}
