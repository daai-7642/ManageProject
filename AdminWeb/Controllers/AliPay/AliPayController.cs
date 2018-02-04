using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility.AliPay;

namespace AdminWeb.Controllers
{
    public class AliPayController : Controller
    {
        // GET: AliPay
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BillIndex()
        {
            new AliPayHelper().QueryData();
            return View();
        }
    }
}