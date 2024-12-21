using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social.Models
{
    public class ad : Admin_User
    {
        public int buyer_id { get; set; }
        public int seller_id { get; set; }
        public List<ad> adcat_list { get; set; }
        public int ad_id { get; set; }
        public string ad_title { get; set; }
        public int adcat_id { get; set; }
        public string adcat_name { get; set; }
        public string ad_price { get; set;}
        public string ad_desc { get; set; }
        public string[] ad_img {get; set;}
        public string ad_img1 {get;set;}
        public string ad_img2 { get; set; }
        public string ad_img3 { get; set; }
        public string ad_img4 { get; set; }
        public DateTime ad_datetime { get; set; }
   
        public void ad_upload(int usr_id)
        {
            //this method will be used  to upload ads through profile
            ad_datetime = DateTime.Now;
            SqlCommand sc = new SqlCommand("ad_upload", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_upload";
            sc.Parameters.AddWithValue("@ad_title", ad_title);
            sc.Parameters.AddWithValue("@adcat_name", adcat_name);
            sc.Parameters.AddWithValue("@ad_price", ad_price);
            sc.Parameters.AddWithValue("@ad_desc", ad_desc);
            sc.Parameters.AddWithValue("@ad_img1", ad_img[0]);
            sc.Parameters.AddWithValue("@ad_img2", ad_img[1]);
            sc.Parameters.AddWithValue("@ad_img3", ad_img[2]);
            sc.Parameters.AddWithValue("@ad_img4", ad_img[3]);
            sc.Parameters.AddWithValue("@ad_datetime", ad_datetime);
            sc.Parameters.AddWithValue("@usr_id", usr_id);
            sc.ExecuteNonQuery();

        }

        public ad ad_search()
        {
            //this search method will be use to update ads detail ... this will put values in field to update
            SqlCommand sc = new SqlCommand("ad_search", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_search";
            sc.Parameters.AddWithValue("@ad_id",ad_id);
            SqlDataReader sdr = sc.ExecuteReader();
            ad ad_search = new ad();
            while (sdr.Read())
            {
                
                ad_search.ad_title = (string)sdr["ad_title"];
                ad_search.adcat_name = (string)sdr["adcat_name"];
                ad_search.ad_price = (string)sdr["ad_price"];
                ad_search.ad_desc = (string)sdr["ad_desc"];
                ad_search.ad_img1 = (string)sdr["ad_img1"];
                ad_search.ad_img2 = (string)sdr["ad_img2"];
                ad_search.ad_img3 = (string)sdr["ad_img3"];
                ad_search.ad_img4 = (string)sdr["ad_img4"];
            }
           
            sdr.Close();
            return ad_search;
        }
       public void ad_update(int usr_id)
        {
            //this method is use for update ads
            if (ad_img[0] != null && ad_img[1] != null && ad_img[2] != null && ad_img[3] != null)
            {
                ad_img1 = ad_img[0];
                ad_img2 = ad_img[1];
                ad_img3 = ad_img[2];
                ad_img4 = ad_img[3];
            }          

            ad_datetime = DateTime.Now;
            SqlCommand sc = new SqlCommand("ad_update",connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_update";
            sc.Parameters.AddWithValue("@ad_id", ad_id);
            sc.Parameters.AddWithValue("@ad_title",ad_title);
            sc.Parameters.AddWithValue("@adcat_name",adcat_name);
            sc.Parameters.AddWithValue("@ad_price",ad_price);
            sc.Parameters.AddWithValue("@ad_desc",ad_desc);
            sc.Parameters.AddWithValue("@ad_img1",ad_img1);
            sc.Parameters.AddWithValue("@ad_img2", ad_img2);
            sc.Parameters.AddWithValue("@ad_img3", ad_img3);
            sc.Parameters.AddWithValue("@ad_img4", ad_img4);
            sc.Parameters.AddWithValue("@ad_datetime",ad_datetime);
            sc.Parameters.AddWithValue("@usr_id",usr_id);
            sc.ExecuteNonQuery();
        }

        public List<ad> adcat_show()
        { //this method will be used to ad category but also for dropdown when uploading ad

            SqlCommand sc = new SqlCommand("adcat_populate", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adcat_populate";
            List<ad> lst = new List<ad>();
            SqlDataReader rdr = sc.ExecuteReader();
            while (rdr.Read())
            {
                ad ad = new ad();
                ad.adcat_id = (int)rdr["adcat_id"];
                ad.adcat_name = (string)rdr["adcat_name"];
                lst.Add(ad);
            }
            rdr.Close();
            return lst;
        }
        public List<ad> ad_published(int id)
        {
            //this method will be use to show active ad's of specfic user
            SqlCommand sc = new SqlCommand("ad_published", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_published";
            sc.Parameters.AddWithValue("@id", id);
            List<ad> active_ad = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["ad_id"];
                ad.ad_title = (string)sdr["ad_title"];
                ad.adcat_name = (string)sdr["adcat_name"];
                ad.ad_price = (string)sdr["ad_price"];
                ad.ad_desc = (string)sdr["ad_desc"];
                ad.ad_img1 = (string)sdr["ad_img1"];
                ad.ad_img2 = (string)sdr["ad_img2"];
                ad.ad_img3 = (string)sdr["ad_img3"];
                ad.ad_img4 = (string)sdr["ad_img4"];
                ad.ad_datetime = (DateTime)sdr["ad_datetime"];
                active_ad.Add(ad);
            }
            sdr.Close();
            return active_ad;
        }

        public ad ad_specficdetail(int id)
        {
            //this method will cover detail view of ads in user profile section and newsfeed ads details as well
            SqlCommand sc = new SqlCommand("ad_specificdetail", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_specificdetail";
            sc.Parameters.AddWithValue("@ad_id",id);
            ad ad = new ad();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
               
                ad.user_id = (int)sdr["usr_id"];
                ad.user_fname=(string)sdr["usr_fname"];
                ad.user_lname= (string)sdr["usr_lname"];
                ad.user_email= (string)sdr["usr_email"];
                ad.user_pass= (string)sdr["usr_pass"];
                ad.user_contact= (string)sdr["usr_contact"];
                ad.user_gender= (string)sdr["usr_gender"];
                ad.user_address= (string)sdr["usr_address"];
                ad.user_city= (string)sdr["usr_city"];
                ad.user_imgpath= (string)sdr["usr_imgpath"];
             ad.user_budget = (string)sdr["usr_budget"];
              ad.interest_name = (string)sdr["usr_interest"];
                ad.ad_id= (int)sdr["ad_id"];
                ad.ad_title= (string)sdr["ad_title"];
                ad.adcat_name= (string)sdr["adcat_name"];
                ad.ad_price= (string)sdr["ad_price"];
                ad.ad_desc= (string)sdr["ad_desc"];
                ad.ad_img1= (string)sdr["ad_img1"];
                ad.ad_img2= (string)sdr["ad_img2"];
                ad.ad_img3= (string)sdr["ad_img3"];
                ad.ad_img4= (string)sdr["ad_img4"];
                ad.ad_datetime= (DateTime)sdr["ad_datetime"];
            
            }
            sdr.Close();
            return ad;
        }

        public List<ad> adshow_bycat(string name,int user_id)
        {
            //this method show ads  by category
            SqlCommand sc = new SqlCommand("adshow_bycat", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adshow_bycat";
            sc.Parameters.AddWithValue("@cat_name",name);
            sc.Parameters.AddWithValue("@user_id",user_id);
            List<ad> adcat_list = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["ad_id"];
                ad.ad_title = (string)sdr["ad_title"];
                ad.adcat_name = (string)sdr["adcat_name"];
                ad.ad_price = (string)sdr["ad_price"];
                ad.ad_desc = (string)sdr["ad_desc"];
                ad.ad_img1 = (string)sdr["ad_img1"];
                ad.ad_img2 = (string)sdr["ad_img2"];
                ad.ad_img3 = (string)sdr["ad_img3"];
                ad.ad_img4 = (string)sdr["ad_img4"];
                ad.ad_datetime = (DateTime)sdr["ad_datetime"];
                adcat_list.Add(ad);

            }
            sdr.Close();

            return adcat_list;
        }
        public List<ad> adshow_all(int user_id)
        {
            // byusing this method ads will show in news feed but person who logged in can't see his/her ads in newsfeed he must go in his/her profile to see.
            SqlCommand sc = new SqlCommand("adshow_all", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adshow_all";
            sc.Parameters.AddWithValue("@user_id", user_id);
            List<ad> adcat_list = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["ad_id"];
                ad.ad_title = (string)sdr["ad_title"];
                ad.adcat_name = (string)sdr["adcat_name"];
                ad.ad_price = (string)sdr["ad_price"];
                ad.ad_desc = (string)sdr["ad_desc"];
                ad.ad_img1 = (string)sdr["ad_img1"];
                ad.ad_img2 = (string)sdr["ad_img2"];
                ad.ad_img3 = (string)sdr["ad_img3"];
                ad.ad_img4 = (string)sdr["ad_img4"];
                ad.ad_datetime = (DateTime)sdr["ad_datetime"];
                adcat_list.Add(ad);

            }
            sdr.Close();

            return adcat_list;
        }
        public ad profile_show(int id)
        {
            //justgetting profile details to show .. IENumerable Conversion error on different classes with same view

            ad ad = new ad();
            Admin_User userdetails = new Admin_User().get_logindetail(id);
            userdetails.user_id = userdetails.user_id;
            ad.user_fname = userdetails.user_fname;
            ad.user_lname = userdetails.user_lname;
           ad.user_email = userdetails.user_email;
            ad.user_pass = userdetails.user_pass;
           ad.user_contact = userdetails.user_contact;
            ad.user_gender = userdetails.user_gender;
           ad.user_address = userdetails.user_address;
            ad.user_city = userdetails.user_city;
            ad.user_imgpath = userdetails.user_imgpath;
            ad.user_budget = userdetails.user_budget;
            ad.interest_name = userdetails.interest_name;
            return ad;
        }
        public void ad_delete(int id)
        {
            //this method will delete specfic ad
            SqlCommand sc = new SqlCommand("ad_delete", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_delete";
            sc.Parameters.AddWithValue("@ad_id", id);
            sc.ExecuteNonQuery();
        }

        public List<ad> ad_showall()
        {
            //this method will show all ad's in admin panel
            SqlCommand sc = new SqlCommand("ad_showall", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_showall";
            SqlDataReader sdr = sc.ExecuteReader();
            List<ad> ad_list = new List<ad>();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["ad_id"];
                ad.ad_title = (string)sdr["ad_title"];
                ad.adcat_name = (string)sdr["adcat_name"];
                ad.ad_price = (string)sdr["ad_price"];
                ad.ad_desc = (string)sdr["ad_desc"];
                ad.ad_img1 = (string)sdr["ad_img1"];
                ad.ad_img2 = (string)sdr["ad_img2"];
                ad.ad_img3 = (string)sdr["ad_img3"];
                ad.ad_img4 = (string)sdr["ad_img4"];
                ad.ad_datetime = (DateTime)sdr["ad_datetime"];
                ad.user_id = (int)sdr["usr_id"];
                ad_list.Add(ad);
            }
            sdr.Close();
            return ad_list;
        }
        //public List<ad> homeprofie_specificdetail(int user_id)
        //{
        //    //this method will show user profile in home area as well as the all ads related to specific user

        //}
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //SELLING CODE
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int gettinguser(int ad_id)
        {
            SqlCommand sc = new SqlCommand("gettinguser", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "gettinguser";
            sc.Parameters.AddWithValue("@ad_id", ad_id);
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                user_id = (int)sdr["usr_id"];
            }
            sdr.Close();
            return user_id;
        }
        public void sale_insert(int user_id, int ad_id)
        {
            SqlCommand sc = new SqlCommand("sale_insert", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "sale_insert";
            sc.Parameters.AddWithValue("@user_id", user_id);
            sc.Parameters.AddWithValue("@ad_id", ad_id);
            sc.ExecuteNonQuery();
        }
        public void buy_insert(int user_id, int ad_id)
        {
            SqlCommand sc = new SqlCommand("buy_insert", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "buy_insert";
            sc.Parameters.AddWithValue("@user_id", user_id);
            sc.Parameters.AddWithValue("@ad_id", ad_id);
            sc.ExecuteNonQuery();
        }
        public void adsale_delete(int ad_id)
        {
            //delete those ads which are sold
            SqlCommand sc = new SqlCommand("adsale_delete", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "adsale_delete";
            sc.Parameters.AddWithValue("@ad_id", ad_id);
            sc.ExecuteNonQuery();
        }
        public void sold(ad ad)
        {
            //inserting sold ads into sold table
            SqlCommand sc = new SqlCommand("ad_sold", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "ad_sold";
            sc.Parameters.AddWithValue("@id", ad.ad_id);
            sc.Parameters.AddWithValue("@title", ad.ad_title);
            sc.Parameters.AddWithValue("@cat_name", ad.adcat_name);
            sc.Parameters.AddWithValue("@price", ad.ad_price);
            sc.Parameters.AddWithValue("@desc", ad.ad_desc);
            sc.Parameters.AddWithValue("@img1", ad.ad_img1);
            sc.Parameters.AddWithValue("@img2", ad.ad_img2);
            sc.Parameters.AddWithValue("@img3", ad.ad_img3);
            sc.Parameters.AddWithValue("@img4", ad.ad_img4);
            sc.Parameters.AddWithValue("@datetime", ad.ad_datetime);
            sc.Parameters.AddWithValue("@usr_id", ad.user_id);
            sc.ExecuteNonQuery();
        }
        public List<ad> sold_show(int id)
        {
            //this method willshow specific buy ads of specfic profile
            SqlCommand sc = new SqlCommand("sold_show", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "sold_show";
            sc.Parameters.AddWithValue("@user_id", id);
            List<ad> list = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["id"];
                ad.ad_title = (string)sdr["title"];
                ad.adcat_name = (string)sdr["cat_name"];
                ad.ad_price = (string)sdr["price"];
                ad.ad_desc = (string)sdr["descript"];
                ad.ad_img1 = (string)sdr["img1"];
                ad.ad_img2 = (string)sdr["img2"];
                ad.ad_img3 = (string)sdr["img3"];
                ad.ad_img4 = (string)sdr["img4"];
                ad.ad_datetime = (DateTime)sdr["posted"];
                ad.user_id = (int)sdr["usr_id"];
                ad.user_fname = (string)sdr["usr_fname"];
                ad.user_lname = (string)sdr["usr_lname"];
                ad.user_email = (string)sdr["usr_email"];
                ad.user_contact = (string)sdr["usr_contact"];
                ad.user_address = (string)sdr["usr_address"];
                ad.user_imgpath = (string)sdr["usr_imgpath"];
                ad.user_budget = (string)sdr["usr_budget"];
                ad.interest_name = (string)sdr["usr_interest"];
                list.Add(ad);
            }
            sdr.Close();
            return list;
        }
        public List<ad> buy_show(int id)
        {
            //this method willshow specific buy ads of specfic profile
            SqlCommand sc = new SqlCommand("buy_show", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "buy_show";
            sc.Parameters.AddWithValue("@user_id", id);
            List<ad> list = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["id"];
                ad.ad_title = (string)sdr["title"];
                ad.adcat_name = (string)sdr["cat_name"];
                ad.ad_price = (string)sdr["price"];
                ad.ad_desc = (string)sdr["descript"];
                ad.ad_img1 = (string)sdr["img1"];
                ad.ad_img2 = (string)sdr["img2"];
                ad.ad_img3 = (string)sdr["img3"];
                ad.ad_img4 = (string)sdr["img4"];
                ad.ad_datetime = (DateTime)sdr["posted"];
                ad.user_id = (int)sdr["usr_id"];
                ad.user_fname = (string)sdr["usr_fname"];
                ad.user_lname = (string)sdr["usr_lname"];
                ad.user_email = (string)sdr["usr_email"];
                ad.user_contact = (string)sdr["usr_contact"];
                ad.user_address = (string)sdr["usr_address"];
                ad.user_imgpath = (string)sdr["usr_imgpath"];
               ad.user_budget = (string)sdr["usr_budget"];
                ad.interest_name = (string)sdr["usr_interest"];
                list.Add(ad);
            }
            sdr.Close();
            return list;
        }

        public List<ad> soldshow_byid(int id)
        {
            //this method willshow specific ad detail of sold ads
            SqlCommand sc = new SqlCommand("soldshow_byid", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "soldshow_byid";
            sc.Parameters.AddWithValue("@ad_id", id);
            List<ad> list = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["id"];
                ad.ad_title = (string)sdr["title"];
                ad.adcat_name = (string)sdr["cat_name"];
                ad.ad_price = (string)sdr["price"];
                ad.ad_desc = (string)sdr["descript"];
                ad.ad_img1 = (string)sdr["img1"];
                ad.ad_img2 = (string)sdr["img2"];
                ad.ad_img3 = (string)sdr["img3"];
                ad.ad_img4 = (string)sdr["img4"];
                ad.ad_datetime = (DateTime)sdr["posted"];
                ad.user_id = (int)sdr["usr_id"];
                ad.user_fname = (string)sdr["usr_fname"];
                ad.user_lname = (string)sdr["usr_lname"];
                ad.user_email = (string)sdr["usr_email"];
                ad.user_contact = (string)sdr["usr_contact"];
                ad.user_address = (string)sdr["usr_address"];
                ad.user_imgpath = (string)sdr["usr_imgpath"];
                ad.user_budget = (string)sdr["usr_budget"];
                ad.interest_name = (string)sdr["usr_interest"];
                list.Add(ad);
            }
            sdr.Close();
            return list;
        }
        public List<ad> buyshow_byid(int id)
        {
            //this method willshow specific ad detail of buy ads
            SqlCommand sc = new SqlCommand("buyshow_byid", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "buyshow_byid";
            sc.Parameters.AddWithValue("@ad_id", id);
            List<ad> list = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["id"];
                ad.ad_title = (string)sdr["title"];
                ad.adcat_name = (string)sdr["cat_name"];
                ad.ad_price = (string)sdr["price"];
                ad.ad_desc = (string)sdr["descript"];
                ad.ad_img1 = (string)sdr["img1"];
                ad.ad_img2 = (string)sdr["img2"];
                ad.ad_img3 = (string)sdr["img3"];
                ad.ad_img4 = (string)sdr["img4"];
                ad.ad_datetime = (DateTime)sdr["posted"];
                ad.user_id = (int)sdr["usr_id"];
                ad.user_fname = (string)sdr["usr_fname"];
                ad.user_lname = (string)sdr["usr_lname"];
                ad.user_email = (string)sdr["usr_email"];
                ad.user_contact = (string)sdr["usr_contact"];
                ad.user_address = (string)sdr["usr_address"];
                ad.user_imgpath = (string)sdr["usr_imgpath"];
               ad.user_budget = (string)sdr["usr_budget"];
                ad.interest_name = (string)sdr["usr_interest"];
                list.Add(ad);
            }
            sdr.Close();
            return list;
        }
        public List<ad> sold_report()
        {
            //this method willshow specific ad detail of buy ads
            SqlCommand sc = new SqlCommand("sold_report", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "sold_report";
            List<ad> list = new List<ad>();
            SqlDataReader sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                ad ad = new ad();
                ad.ad_id = (int)sdr["id"];
                ad.ad_title = (string)sdr["title"];
                ad.adcat_name = (string)sdr["cat_name"];
                ad.ad_price = (string)sdr["price"];
                ad.ad_desc = (string)sdr["descript"];
                ad.ad_img1 = (string)sdr["img1"];
                ad.ad_img2 = (string)sdr["img2"];
                ad.ad_img3 = (string)sdr["img3"];
                ad.ad_img4 = (string)sdr["img4"];
                ad.ad_datetime = (DateTime)sdr["posted"];
                ad.user_id = (int)sdr["usr_id"];
                ad.buyer_id = (int)sdr["buyer_id"];
                list.Add(ad);
            }
            sdr.Close();
            return list;
        }
        public List<ad> get_randomuser(int id)
        {
            SqlCommand sc = new SqlCommand("userprofile_showallusers", connection.get());
            sc.CommandType = System.Data.CommandType.StoredProcedure;
            sc.CommandText = "userprofile_showallusers";
          //  sc.Parameters.AddWithValue("@id", id);
            SqlDataReader sdr = sc.ExecuteReader();
            List<ad> random_list = new List<ad>();
            while (sdr.Read())
            {
                ad user_session = new ad();
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
                random_list.Add(user_session);
            }

            sdr.Close();
            return (random_list);
        }
    }
}