using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Logic;
using Webdiyer.WebControls.Mvc;
using Entity;

namespace AdminWeb.Controllers
{
    public class LoggerController : Controller
    {
        LogLogic loggerLogic = new LogLogic();
        // GET: Logger
        public ActionResult Index(int pageIndex = 1)
        {
            int totalCount = 0;
            int pageSize = OperateHelper.PageSize;
            var list = loggerLogic.GetLogsPageList(pageIndex, pageSize, a =>(DateTime)a.CollectDate, false,null, out totalCount);
            var pageList = new PagedList<MyLogger>(list, pageIndex, pageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView("LoggerPage", pageList);
            }
            return View(pageList);
        }
    }
}