using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social.Models;
namespace Social.Controllers
{
    [Session]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult dashboard()
        {
            total calculation = new total();
          
            return View(calculation);
        }
        public ActionResult admin_adlist()
        {
            return View(new ad().ad_showall());
        }
        public ActionResult admin_userprofile(string id)
        {
            ad ad = new ad();
            ViewBag.userads_list = ad.ad_published(Convert.ToInt32(id));
            ViewBag.userads_sell = ad.sold_show(Convert.ToInt32(id));
            ViewBag.userads_buy = ad.buy_show(Convert.ToInt32(id));
            ad ads = ad.profile_show(Convert.ToInt32(id));
            return View(ads);
        }
        public ActionResult admin_userlist()
        {
            return View(new user_queries().userprofile_showallusers());
        }
        public ActionResult admin_profile()
        {
            user_queries userprofile_details = new user_queries().userprofile_details(Convert.ToInt32(Session["Id"]));
            List<user_queries> userdetails_list = new List<user_queries>();
            userdetails_list.Add(userprofile_details);
            return View(userdetails_list);
        }
        [HttpGet]
        public ActionResult admin_adduser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult admin_adduser(user_queries user_query, HttpPostedFileBase user_img)
        {

            if (user_img != null)
            {
                string ImageName = System.IO.Path.GetFileName(user_img.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                user_img.SaveAs(physicalPath);
                user_query.user_imgpath = ImageName;
                user_query.adminprofile_insert();
                return RedirectToAction("dashboard");

            }
            else
                return RedirectToAction("admin_adduser");
        }
       [HttpGet]
        public ActionResult admin_edituser(string id)
        {
            user_queries userprofile_details = new user_queries().userprofile_details(Convert.ToInt32(id));
            return View(userprofile_details);
        }
        public ActionResult admin_edituser(user_queries userprofile_update, HttpPostedFileBase adminup_img)
        {
            if (adminup_img != null)
            {

                string ImageName = System.IO.Path.GetFileName(adminup_img.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                adminup_img.SaveAs(physicalPath);
                userprofile_update.user_imgpath = ImageName;

            }
            userprofile_update.userprofile_update();
            return RedirectToAction("admin_profile");
        }
        public ActionResult admin_soldreport()
        {
            List<ad> list = new ad().sold_report();
            return View(list);
        }
        [HttpGet]
        public ActionResult adcat_insert() {
            return View();
        }

        [HttpPost]
        public ActionResult adcat_insert(adcategory adcat)
        {
            adcat.adcat_insert();
            return RedirectToAction("adcat_show");
        }
        [HttpGet]
        public ActionResult adcat_edit(string id)
        {
            adcategory adcat = new adcategory().adcat_search(Convert.ToInt32(id));
            return View(adcat);
        }

        [HttpPost]
        public ActionResult adcat_edit(adcategory adcat)
        {
            adcat.adcat_update();
            return RedirectToAction("adcat_show");
        }
        public ActionResult adcat_delete(string id)
        {
            adcategory adcat = new adcategory();
            adcat.adcat_id = Convert.ToInt32(id);
            adcat.adcat_delete();
            return RedirectToAction("adcat_show");
        }
        public ActionResult adcat_show()
        {
            return View(new adcategory().adcat_show());
        }
    }
}