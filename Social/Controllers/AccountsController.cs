using Social.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        [HttpGet]
        public ActionResult signin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signin(user_registration User)
        {
            if (Session["Id"] == null)
            {
                SqlCommand sc = new SqlCommand("Login", connection.get());
                sc.CommandType = System.Data.CommandType.StoredProcedure;
                sc.Parameters.AddWithValue("@Email", User.email);
                sc.Parameters.AddWithValue("@Password", User.Password);
                SqlDataReader sdr = sc.ExecuteReader();
                if (sdr.Read())
                {
                    user_registration.Check = true;
                    Session["Id"] = (int)sdr["usr_id"];
                    Session["Role"] = (string)sdr["usr_role"];
                    Session["Firstname"] = (string)sdr["usr_fname"];
                    Session["Image_Path"] = (string)sdr["usr_imgpath"];

                    sdr.Close();
                    if (Session["Role"].ToString() == "Admin")
                    {
                        return RedirectToAction("dashboard", "Admin");
                    }
                    else if (Session["Role"].ToString() == "User")
                    {
                        return RedirectToAction("Newsfeed", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username and password");
                    sdr.Close();
                }
            }

            return View();
        }
        [HttpGet]
        public ActionResult signup(ad ad)
        {
            Admin_User au = new Admin_User();
            au.Interest_list=ad.adcat_show();
            ViewBag.interest_list = au.Interest_list;
            return View();
        }
        [HttpPost]
        public ActionResult signup(user_queries user_query, HttpPostedFileBase inputimage)
        {
            
            if (inputimage != null)
            {
                string ImageName = System.IO.Path.GetFileName(inputimage.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                inputimage.SaveAs(physicalPath);
                user_query.user_imgpath = ImageName;
                user_query.userprofile_insert();
                return RedirectToAction("signin");

            }
            else
                return RedirectToAction("signup");
        }
        public ActionResult signout()
        {
            Session["Id"] = "";
            Session["Id"] = null;
      
            
            return RedirectToAction("signin");
        }
    }
}