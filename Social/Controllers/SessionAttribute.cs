using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social.Controllers
{
    public class SessionAttribute : ActionFilterAttribute
    {
        // GET: SessionAttribute
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.Session["Id"] == null)
            {
                filterContext.Result = new RedirectResult("~\\Accounts\\signin");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}