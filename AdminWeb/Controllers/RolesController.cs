using Entity;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Webdiyer.WebControls.Mvc;
namespace AdminWeb.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index(int pageIndex=1)
        {
            int totalCount = 0;
            int pageSize = OperateHelper.PageSize;
            var list = new RoleLogic().GetRolesPageList(pageIndex,pageSize, a => a.Id, null,out totalCount);
            var pageList = new PagedList<MyRoles>(list,pageIndex,pageSize,totalCount);
            return View(pageList);
        }
    }
}