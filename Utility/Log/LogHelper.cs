using System;
using System.Collections.Generic;
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
            log4net.Config.XmlConfigurator.Configure();
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
            loginfo.Info(info);
            //if (loginfo.IsInfoEnabled)
            //{
            //    loginfo.Info(info);
            //}
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="info">错误</param>
        /// <param name="ex">Exception</param>
        public static void WriteLog(string info, Exception ex)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(info, ex);
            }
        }
    }
}