using Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WxToken
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception err = HttpContext.Current.Error;
            ErrorLogHelper.WriteLog(new LogContent()
            {
                UserName = User.Identity.Name,
                EventCategory = "ERROR",
                Description = err.StackTrace,
                Source = err.Message,
                SourceUrl = Request.Url.AbsoluteUri,
                ComputerName = UserHelper.GetUserIp(),
                Mac_Address = Utility.OperateHelper.GetMacAddress()
            });
        }
    }
}
