using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace Social.Models
{
    public class adcategory
    {
        public string adcat_name { get; set; }
        public int adcat_id { get; set; }
        public void adcat_insert()
        {
            SqlCommand sc = new SqlCommand("adcat_insert",connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adcat_insert";
            sc.Parameters.AddWithValue("@cat_name",adcat_name);
            sc.ExecuteNonQuery();
        }
        public adcategory adcat_search(int id)
        {
            SqlCommand sc = new SqlCommand("adcat_search", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adcat_search";
            sc.Parameters.AddWithValue("@cat_id",id);
            SqlDataReader sdr = sc.ExecuteReader();
            adcategory adcat = new adcategory();
            while (sdr.Read())
            {
                adcat.adcat_id = (int)sdr["adcat_id"];
                adcat.adcat_name = (string)sdr["adcat_name"];
            }
            sdr.Close();
            return adcat;
        }
        public void adcat_update()
        {
            SqlCommand sc = new SqlCommand("adcat_update", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adcat_update";
            sc.Parameters.AddWithValue("@cat_id",adcat_id);
            sc.Parameters.AddWithValue("@cat_name", adcat_name);
            sc.ExecuteNonQuery();
        }
        public void adcat_delete()
        {
            SqlCommand sc = new SqlCommand("adcat_delete", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adcat_delete";
            sc.Parameters.AddWithValue("@cat_id", adcat_id);
            sc.ExecuteNonQuery();
        }
        public List<adcategory> adcat_show()
        {
            SqlCommand sc = new SqlCommand("adcat_show", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adcat_show";
            SqlDataReader sdr = sc.ExecuteReader();
            List<adcategory> adcat_list = new List<adcategory>();
            while (sdr.Read())
            {
                adcategory adcat = new adcategory();
                adcat.adcat_id = (int)sdr["adcat_id"];
                adcat.adcat_name = (string)sdr["adcat_name"];
                adcat_list.Add(adcat);
            }
            sdr.Close();
            return adcat_list;
        }
    }
}