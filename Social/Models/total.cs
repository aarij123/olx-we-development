using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Social.Models
{
    public class total
    {
        public int totalads { get; set; }
        public int totalsold { get; set; }
        public int totalusers { get; set; }

        public int ads()
        {
            SqlCommand sc = new SqlCommand("totalads",connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "totalads";
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                totalads = (int)sdr["totalads"];
            }sdr.Close();
            return totalads;
        }
        public int sold()
        {
            SqlCommand sc = new SqlCommand("totalsold", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "totalsold";
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                totalsold = (int)sdr["totalsold"];
            }
            sdr.Close();
            return totalsold;
        }
        public int users()
        {
            SqlCommand sc = new SqlCommand("totalusers", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "totalusers";
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                totalusers = (int)sdr["totalusers"];
            }
            sdr.Close();
            return totalusers;
        }
    }
}