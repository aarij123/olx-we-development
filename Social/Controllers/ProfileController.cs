using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social.Models;
using System.IO;

namespace Social.Controllers
{
    [Session]
    public class ProfileController : Controller
    {
        // GET: Profile
       [HttpGet]
        public ActionResult myprofile(ad ad)
        {
            ViewBag.ads = ad.ad_published(Convert.ToInt32(Session["Id"]));
            ViewBag.soldads = new ad().sold_show(Convert.ToInt32(Session["Id"]));
            ViewBag.buyads = new ad().buy_show(Convert.ToInt32(Session["Id"]));
            return View();
        }
        public ActionResult ad_historydetail(string id)
        {
            List<ad> list = new ad().soldshow_byid(Convert.ToInt32(id));
            return View(list);
        }
        public ActionResult ad_historybuydetail(string id)
        {
            List<ad> list = new ad().buyshow_byid(Convert.ToInt32(id));
            return View(list);
        }
        [HttpGet]
        public ActionResult profilesettings(ad ad)
        {
            Admin_User au = new Admin_User();
            au.Interest_list = ad.adcat_show();
            ViewBag.interest_list = au.Interest_list;
            user_queries userprofile_details = new user_queries().userprofile_details(Convert.ToInt32(Session["Id"]));
            return View(userprofile_details);
        }
        [HttpPost]
        public ActionResult profilesettings(user_queries userprofile_update,HttpPostedFileBase userup_img)
        {
            if (userup_img != null)
            {
              
                string ImageName = System.IO.Path.GetFileName(userup_img.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                userup_img.SaveAs(physicalPath);
                userprofile_update.user_imgpath = ImageName;

            }
            userprofile_update.userprofile_update();
            return RedirectToAction("myprofile");
        
        }
        [HttpGet]
        public ActionResult profiledetails()
        {
            user_queries userprofile_details = new user_queries().userprofile_details(Convert.ToInt32(Session["Id"]));
            List<user_queries> userdetails_list = new List<user_queries>();
            userdetails_list.Add(userprofile_details);
            return View(userdetails_list);
        }
        [HttpGet]
        public ActionResult ad_addnew(ad ad)
        {
            ad.adcat_list = ad.adcat_show();
            ViewBag.adcat_list = ad.adcat_list;
            return View(ad);
        }
        [HttpPost]

        public ActionResult ad_addnew(ad ad, HttpPostedFileBase[] ad_img)
        {
            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                int i = 0;

                foreach (HttpPostedFileBase file in ad_img)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Images/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        ad.ad_img[i] = InputFileName;
                        i++;
                    }

                }
                ad.ad_upload(Convert.ToInt32(Session["Id"]));
            }
            return RedirectToAction("myprofile");
            
        }
        public ActionResult ad_detailview(string id)
        {
            List<ad> specific_adlist = new List<ad>();
            specific_adlist.Add(new ad().ad_specficdetail(Convert.ToInt32(id)));
            return View(specific_adlist);
        }
        [HttpGet]
        public ActionResult ad_update(string id)
        {
            ad ad = new ad();
            ad.adcat_list = ad.adcat_show();
            ViewBag.adcat_list = ad.adcat_list;
            ad.ad_id =Convert.ToInt32(id);
            ad ad_search= ad.ad_search();
            ad_search.ad_id= Convert.ToInt32(id);
            return View(ad_search);
        }
        [HttpPost]
        public ActionResult ad_update(ad ad, HttpPostedFileBase[] ad_img)
        {
         
            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                int i = 0;
                
                    foreach (HttpPostedFileBase file in ad_img)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Images/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            ad.ad_img[i] = InputFileName;
                            i++;
                        }

                }
             
                ad.ad_update(Convert.ToInt32(Session["Id"]));
                
            
            }
            return RedirectToAction("myprofile");

        }

        public ActionResult ad_delete(string id)
        {
            ad ad = new ad();
                ad.ad_delete(Convert.ToInt32(id));
            return RedirectToAction("myprofile");
        }
    }
}