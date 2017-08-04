using Entity;
using Logic;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using ViewModel;
using Webdiyer.WebControls.Mvc;

namespace AdminWeb.Controllers
{
    public class FunctionController : Controller
    {
        FunctionLogic functionLogic = new FunctionLogic();
        // GET: Function
        public ActionResult Index(int pageIndex=1,string functionName="",int groupID=0)
        {
            //Expression<Func<T, S>>
            int totalCount = 0;
            int pageSize = OperateHelper.PageSize;
            var flist = functionLogic.GetFunctionPageList(pageIndex,pageSize, a => a.OrderNo,true, a => (functionName == "" ? true : a.FunctionName.Contains(functionName))&& (groupID == 0 ? true : a.GroupID == groupID),out totalCount);
            var pageList =new PagedList<Function>(flist,pageIndex,pageSize,totalCount);
            if(Request.IsAjaxRequest())
            { 
                return PartialView("FunctionPage", pageList);
            }
            var list = functionLogic.GetFunctionAndGroupList();
            ViewBag.FunctionList = list;
            return View(pageList);
        }
        public ActionResult CreateFunction()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateFunction(Function funtion)
        {
            return View();
        }
    }
}