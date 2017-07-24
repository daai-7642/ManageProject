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
        public ActionResult Index(string roleName="", int pageIndex=1)
        {
            int totalCount = 0;
            int pageSize = OperateHelper.PageSize;
            var list = new RoleLogic().GetRolesPageList(pageIndex,pageSize, a => a.Id, a=> roleName == "" ? true : a.Name.Contains(roleName), out totalCount);
            var pageList = new PagedList<MyRoles>(list,pageIndex,pageSize,totalCount);
            if(Request.IsAjaxRequest())
            {
                return PartialView("RolePage",pageList);
            }
            return View(pageList);
        }

        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(MyRoles role)
        {
            return View();
        }
    }
}