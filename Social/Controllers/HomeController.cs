using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Social.Models;
namespace Social.Controllers
{
    [Session]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Newsfeed(ad ad,string name)
        {
            ad ad1 = new ad();
            Admin_User obj = ad1.get_logindetail(Convert.ToInt32(Session["Id"]));
        here:
            List<ad> obj1 = ad1.get_randomuser(Convert.ToInt32(Session["Id"]));
            List<ad> ad_obj = ad1.adshow_all(Convert.ToInt32(Session["Id"]));

            int i = 1;
            string s_gender = obj.user_gender;
            string s_city = obj.user_city;
            int userID = obj.user_id;
            double s_budget =Convert.ToDouble(obj.user_budget);
            string s_interest_name = obj.interest_name;

            List<ad> idofperson = new List<ad>();
            foreach (var item in obj1)
            {

                if (s_gender == @item.user_gender && s_city == @item.user_city && i <= 3 && userID !=@item.user_id)
                {
                    i++;
                    idofperson.Add(item);
                }

            }
          
            List<ad> ads = new List<ad>();
            if (name == null)
            {
                ViewBag.catlist = ad.adcat_show();
                if (idofperson != null)
                {
                    ViewBag.suggestion = idofperson;
                    foreach (var item in ad_obj)
                    {
                        if (s_budget >= Convert.ToDouble(@item.ad_price) && s_interest_name == @item.adcat_name)
                        {
                            ads.Add(item);
                        }
                    }
                }
            
                return View(ads);
            }
            else
                ViewBag.catlist = ad.adcat_show();
                if (idofperson != null)
                {
                    ViewBag.suggestion = idofperson;
                }
                else
                {
                goto here;
                }
                    ads = ad.adshow_bycat(name, Convert.ToInt32(Session["Id"]));
                return View(ads);
        

    }

    public ActionResult home_addetail(ad ad,string id)
        {
            List<ad> ad_list = new List<ad>();
            ad_list.Add(ad.ad_specficdetail(Convert.ToInt32(id)));
            return View(ad_list);
        }

        public ActionResult home_userprofile(string id)
        {
            //user_queries user_detail = new user_queries();
            //ViewBag.profiledetails= user_detail.userprofile_details(Convert.ToInt32(id));
            ad ad = new ad();
            ViewBag.adlist = ad.ad_published(Convert.ToInt32(id));
            
         ad ads= ad.profile_show(Convert.ToInt32(id));
            return View(ads);
        }
        public ActionResult home_salebuy(string id)
        {
            int ad_id = Convert.ToInt32(id);
            int seller_id = new ad().gettinguser(ad_id);
            int buyer_id = Convert.ToInt32(Session["Id"]);
            ad ad = new ad();
            ad.sale_insert(seller_id,ad_id);
            ad.buy_insert(buyer_id,ad_id);
            ad ads = ad.ad_specficdetail(ad_id);
            ad.sold(ads);
          ad.adsale_delete(ad_id);
            return RedirectToAction("Newsfeed");
        }
    }
}