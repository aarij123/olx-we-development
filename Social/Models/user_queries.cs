using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Social.Models
{
    public class user_queries
    {
        public int user_id { get; set; }
        public string user_fname { get; set; }
        public string user_lname { get; set; }
        public string user_email { get; set; }
        public string user_pass { get; set; }
        public string user_contact { get; set; }
        public int user_genderid { get; set; }
        public string user_gender { get; set; }
        public string user_address { get; set; }
        public string user_city { get; set; }
        public string user_imgpath { get; set; }
        public List<ad> Interest_list { get; set; }
        public int interest_id { get; set; }
        public string interest_name { get; set; }
        public string user_budget { get; set; }
        public string interest { get; set; }



        public user_queries userprofile_details(int id)
        {
            Admin_User userdetails = new Admin_User().get_logindetail(id);
            List<user_queries> list = new List<user_queries>();
           user_queries userprofile_details = new user_queries();
            userprofile_details.user_id = userdetails.user_id;
            userprofile_details.user_fname= userdetails.user_fname;
            userprofile_details.user_lname= userdetails.user_lname;
            userprofile_details.user_email= userdetails.user_email;
            userprofile_details.user_pass= userdetails.user_pass;
            userprofile_details.user_contact= userdetails.user_contact;
            userprofile_details.user_gender= userdetails.user_gender;
            userprofile_details.user_address= userdetails.user_address;
            userprofile_details.user_city= userdetails.user_city;
            userprofile_details.user_imgpath = userdetails.user_imgpath;
            userprofile_details.user_budget = userdetails.user_budget;
            userprofile_details.interest_name = userdetails.interest_name;
            list.Add(userprofile_details);
            return userprofile_details;

        }
        public void userprofile_update()
        {
            SqlCommand sc = new SqlCommand("userprofile_update", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "userprofile_update";
            sc.Parameters.AddWithValue("@id", user_id);
            sc.Parameters.AddWithValue("@fname",user_fname);
            sc.Parameters.AddWithValue("@lname",user_lname);
            sc.Parameters.AddWithValue("@email",user_email);
            sc.Parameters.AddWithValue("@pass",user_pass);
            sc.Parameters.AddWithValue("@contact",user_contact);
            sc.Parameters.AddWithValue("@gender",user_gender);
            sc.Parameters.AddWithValue("@address",user_address);
            sc.Parameters.AddWithValue("@city",user_city);
            sc.Parameters.AddWithValue("@imagepath",user_imgpath);
            sc.Parameters.AddWithValue("@usr_budget", user_budget);
            sc.Parameters.AddWithValue("@usr_interest", interest_name);
            sc.ExecuteNonQuery();
        }
        public void userprofile_insert()
        {
            SqlCommand sc = new SqlCommand("userprofile_insert", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "userprofile_insert";
            sc.Parameters.AddWithValue("@usr_fname", user_fname);
            sc.Parameters.AddWithValue("@usr_lname", user_lname);
            sc.Parameters.AddWithValue("@usr_email", user_email);
            sc.Parameters.AddWithValue("@usr_pass", user_pass);
            sc.Parameters.AddWithValue("@usr_contact", user_contact);
            sc.Parameters.AddWithValue("@usr_gender", user_gender);
            sc.Parameters.AddWithValue("@usr_address", user_address);
            sc.Parameters.AddWithValue("@usr_city", user_city);
            sc.Parameters.AddWithValue("@usr_imgpath", user_imgpath);
            sc.Parameters.AddWithValue("@usr_budget", user_budget);
            sc.Parameters.AddWithValue("@usr_interest", interest_name);
            sc.ExecuteNonQuery();
        }
        public void adminprofile_insert()
        {
            SqlCommand sc = new SqlCommand("adminprofile_insert", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adminprofile_insert";
            sc.Parameters.AddWithValue("@usr_fname", user_fname);
            sc.Parameters.AddWithValue("@usr_lname", user_lname);
            sc.Parameters.AddWithValue("@usr_email", user_email);
            sc.Parameters.AddWithValue("@usr_pass", user_pass);
            sc.Parameters.AddWithValue("@usr_contact", user_contact);
            sc.Parameters.AddWithValue("@usr_gender", user_gender);
            sc.Parameters.AddWithValue("@usr_address", user_address);
            sc.Parameters.AddWithValue("@usr_city", user_city);
            sc.Parameters.AddWithValue("@usr_imgpath", user_imgpath);
            sc.ExecuteNonQuery();
        }
        public List<user_queries> userprofile_showallusers()
        {
            SqlCommand sc = new SqlCommand("userprofile_showallusers",connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "userprofile_showallusers";
            SqlDataReader sdr = sc.ExecuteReader();
            List<user_queries> users_list = new List<user_queries>();
            while (sdr.Read())
            {
                user_queries users = new user_queries();
                users.user_id = (int)sdr["usr_id"];
                users.user_fname = (string)sdr["usr_fname"];
                users.user_lname = (string)sdr["usr_lname"];
                users.user_email = (string)sdr["usr_email"];
                users.user_pass = (string)sdr["usr_pass"];
                users.user_contact = (string)sdr["usr_contact"];
                users.user_gender = (string)sdr["usr_gender"];
                users.user_address = (string)sdr["usr_address"];
                users.user_city = (string)sdr["usr_city"];
                users.user_imgpath = (string)sdr["usr_imgpath"];
                users.user_budget = (string)sdr["usr_budget"];
                users.interest_name = (string)sdr["usr_interest"];
                users_list.Add(users);
            }
            sdr.Close();
            return users_list;
        }

    }
}