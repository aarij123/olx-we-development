using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Social
{
    public static class connection
    {
        public static SqlConnection sc;
        public static string dbcon = @"Data Source=AARIJ99;Initial Catalog=Project;Integrated Security=True";
        public static SqlConnection get()
        {
            if (sc == null)
            {
                sc = new SqlConnection();
                sc.ConnectionString = dbcon;
                sc.Open();
            }
            return sc;
        }
    }
}