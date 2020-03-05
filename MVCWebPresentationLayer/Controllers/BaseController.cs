using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = this.Request.Cookies["USERIDENTITY"];

            if (cookie != null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Uusario"));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}