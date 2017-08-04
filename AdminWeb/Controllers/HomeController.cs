using FMCG.Utility.RedisCache;
using Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ClearCache()
        {
            RedisHelper.FlushAll();
            return Content("<script>alert('清除缓存')</script>");
        }
        public ActionResult Ajax(string url,string postDataStr)
        {
           return Content( Utility.HttpHelper.Post(url,postDataStr));
        }
    }
}