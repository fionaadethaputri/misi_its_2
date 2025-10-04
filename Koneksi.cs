using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace masterdataproduk
{
    internal class Koneksi
    {
        public static SqlConnection GetConnection()
        {
            string connectionString =
            @"Server=localhost\SQLEXPRESS;Database=TokoDB;Trusted_Connection=True;";
            return new SqlConnection(connectionString);
        }
    }
}
