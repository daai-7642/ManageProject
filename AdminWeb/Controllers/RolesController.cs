using Entity;
using Logic;
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
    public class RolesController : Controller
    {
        RoleLogic roleLogic = new RoleLogic();
        // GET: Roles
        public ActionResult Index(string roleName="", int pageIndex=1)
        {
            int totalCount = 0;
            int pageSize = OperateHelper.PageSize;
            var list = roleLogic.GetRolesPageList(pageIndex,pageSize, a => a.Id,false, a=> (roleName == "" ? true : a.Name.Contains(roleName)) &&a.Status==1, out totalCount);
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
        public ActionResult CreateRole(MyRoles role,int[] Function)
        {
            role.Status = 1;
            role.Id = DateTime.Now.ToString("yyyyMMddhhmmssff");
            return Json(roleLogic.CreateRole(role,Function));
        }
        [HttpPost]
        public ActionResult DeleteRole(string roleId)
        {
            MyRoles role = new MyRoles()
            {
                Id = roleId,
                Status = 0
            };
            int result=roleLogic.UpdateRoleStatus(role) ;
            return Json(result);
        }
 
    }
}