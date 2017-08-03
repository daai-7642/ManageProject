using Entity;
using Logic;
using Microsoft.Ajax.Utilities;
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
        public ActionResult Index(int pageIndex=1,string functionName="")
        {
            //Expression<Func<T, S>>

            var list = functionLogic.GetFunctionAndGroupList();//.Where();
            return View(list);
        }
    }
}