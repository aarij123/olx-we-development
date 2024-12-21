using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Social.Models
{
    public class Admin_User
    {
        public int user_id { get; set; }
        public string user_fname { get; set; }
        public string user_lname { get; set; }
        public string user_email { get; set; }
        public string user_pass { get; set; }
        public string user_contact { get; set; }
        public string user_gender { get; set; }
        public string user_address { get; set; }
        public string user_city { get; set; }
        public string user_imgpath { get; set; }
        public List<ad> Interest_list { get; set; }
        public int interest_id { get; set; }
        public string interest_name { get; set; }
        public string user_budget { get; set; }
        public string interest { get; set; }
        public Admin_User get_logindetail(int id)
        {
            string query = "SELECT * from User_Registration WHERE usr_id='" + id + "'";
            SqlCommand sc = new SqlCommand(query, connection.get());
            SqlDataReader sdr = sc.ExecuteReader();
            //List<login> log = new List<login>();
            Admin_User user_session = new Admin_User();
            while (sdr.Read())
            {
                user_session.user_id = (int)sdr["usr_id"];
                user_session.user_fname = (string)sdr["usr_fname"];
                user_session.user_lname = (string)sdr["usr_lname"];
                user_session.user_email = (string)sdr["usr_email"];
                user_session.user_pass = (string)sdr["usr_pass"];
                user_session.user_contact = (string)sdr["usr_contact"];
                user_session.user_gender = (string)sdr["usr_gender"];
                user_session.user_address = (string)sdr["usr_address"];
                user_session.user_city = (string)sdr["usr_city"];
                user_session.user_imgpath = (string)sdr["usr_imgpath"];
                user_session.user_budget = (string)sdr["usr_budget"];
                user_session.interest_name = (string)sdr["usr_interest"];
            }

            sdr.Close();
            return (user_session);
        }
    }
}