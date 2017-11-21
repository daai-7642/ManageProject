using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Config/log4net.config", Watch = true)]
namespace Log4net
{
    public class LogHelper
    {
        private LogHelper()
        {
            SetConfig();
        }
    
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("myLogger");

        private static bool IsLoadConfig = false;
        private static void SetConfig()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            //log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="info">提示信息</param>
        public static void WriteLog(string info)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
        public static void WriteLog(string message,string info)
        {
            WriteLog(new LogContent()
            {
                UserName = HttpContext.Current.User.Identity.Name,
                EventCategory = "INFO",
                Description = info,
                Source = message,
                SourceUrl = HttpContext.Current.Request.Url.AbsoluteUri,
                ComputerName = UserHelper.GetUserIp(),
                Mac_Address = Utility.OperateHelper.GetMacAddress()
            });
        }

        public static void WriteLog(string v, object p)
        {
            throw new NotImplementedException();
        }
        public static void WriteLog(string message, object result,object obj)
        {
            WriteLog(message,"返回值"+result.ToString()+"\r\n;"+Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="info">提示信息</param>
        public static void WriteLog(object info)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
           
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
    }
}