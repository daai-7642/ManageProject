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
        FunctionLogic functionLogic = new FunctionLogic();
        // GET: Function
        public ActionResult Index()
        {
            var list = functionLogic.GetFunctionAndGroupList();
            return View(list);
        }
    }
}