using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class FunctionController : Controller
    {
        // GET: Function
        public ActionResult Index()
        {
            var list = new FunctionLogic().GetFunctionList();
            return View(list);
        }
    }
}