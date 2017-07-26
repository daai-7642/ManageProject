using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Log4net
{
    public class ErrorLogHelper
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("errorLogger");
        public static void WriteLog(object info)
        {
                loginfo.Error(info);
        }
        public static void WriteLog(Exception err)
        {
            WriteLog(new LogContent()
            {
                UserName = HttpContext.Current.User.Identity.Name,
                EventCategory = "ERROR",
                Description = err.StackTrace,
                Source = err.Message,
                SourceUrl = HttpContext.Current.Request.Url.AbsoluteUri,
                ComputerName = UserHelper.GetUserIp(),
                Mac_Address = Utility.OperateHelper.GetMacAddress()
            });
            
        }
        public static void WriteLog(object obj,Exception err)
        {
            WriteLog(new LogContent()
            {
                UserName = HttpContext.Current.User.Identity.Name,
                EventCategory = "ERROR",
                Description ="错误:"+ Newtonsoft.Json.JsonConvert.SerializeObject(obj)+";\r\n"+ err.StackTrace,
                Source = err.Message,
                SourceUrl = HttpContext.Current.Request.Url.AbsoluteUri,
                ComputerName = UserHelper.GetUserIp(),
                Mac_Address = Utility.OperateHelper.GetMacAddress()
            });

        }
    }
}
