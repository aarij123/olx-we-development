using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Cover()
        {
            return View();
        }
    }
}