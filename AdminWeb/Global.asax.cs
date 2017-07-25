using AdminWeb.Models;
using Log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AdminWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            Exception err = HttpContext.Current.Error;

            ErrorLogHelper.WriteLog(new LogContent()
            {
                UserName = User.Identity.Name,
                EventCategory = "ERROR",
                Description = err.StackTrace,
                Source=err.Message,
                SourceUrl=Request.Url.AbsoluteUri,
                ComputerName = UserHelper.GetUserIp(),
                Mac_Address=Utility.OperateHelper.GetMacAddress()
            });
        }
    }
}
