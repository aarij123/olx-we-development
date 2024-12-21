using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social.Models
{
    public class user_registration
    {

        public static bool Check { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "*")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "*")]
        public int? gender_id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Image_Path { get; set; }
        public static string msg;
        public string gender_name { get; set; }

        public List<SelectListItem> genderlist { get; set; }
        public static List<SelectListItem> populategender()
        {
            //getting gender list
            List<SelectListItem> items = new List<SelectListItem>();
            string command = "SELECT * from Gender";
            SqlCommand sc = new SqlCommand(command, connection.get());
            using (SqlDataReader sdr = sc.ExecuteReader())
            {
                while (sdr.Read())
                {
                    items.Add(new SelectListItem
                    {

                        Text = sdr["Gender_Name"].ToString(),
                        Value = sdr["Gender_id"].ToString()
                    });
                }
            }
            return items;
            //ending list returns gendername and value
        }

        //public void InsertUser()
        //{
        //    SqlCommand sc = new SqlCommand("InsertUser", connection.get());
        //    sc.CommandType = System.Data.CommandType.StoredProcedure;
        //    sc.Parameters.AddWithValue("@username", Name);
        //    sc.Parameters.AddWithValue("@Gender", gender_name);
        //    sc.Parameters.AddWithValue("@Email", email);
        //    sc.Parameters.AddWithValue("@Mobile", Mobile);
        //    sc.Parameters.AddWithValue("@Password ", Password);
        //    sc.Parameters.AddWithValue("@Image_Path", Image_Path);
        //    sc.ExecuteNonQuery();
        //}

        //No need to use right now
        //public List<user_queries> populategender()
        //{
        //    //getting gender list
        //    List<user_queries> items = new List<user_queries>();
        //    string command = "SELECT * from Gender";
        //    SqlCommand sc = new SqlCommand(command, connection.get());

        //    SqlDataReader rdr = sc.ExecuteReader();
        //    while (rdr.Read())
        //    {
        //        user_queries gender_obj = new user_queries();
        //        gender_obj.user_genderid = (int)rdr["Gender_id"];
        //        gender_obj.user_gender = (string)rdr["Gender_name"];
        //        items.Add(gender_obj);
        //    }
        //    rdr.Close();
        //    return items;
        //    //ending list returns gendername and value
        //}
    }
}
