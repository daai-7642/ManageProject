using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace AdminWeb.Controllers
{
    public class DbOperateController : Controller
    {
         
        public ActionResult BackDB()
        {
            bool result=DbOperate.BackDB(Server.MapPath("/DB")+"/"+DateTime.Now.ToString("yyyyMMdd"));
            return Content("<script>alert('备份成功')</script>");
        }
    }
}