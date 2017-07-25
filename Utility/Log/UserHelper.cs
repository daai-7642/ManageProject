using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Log4net
{
    public static class UserHelper
    {
        /// <summary>
        /// 获取用户标识
        /// 适用Forms验证、Windows验证
        /// </summary>
        /// <returns>用户标识</returns>
        public static string GetUserIdentity()
        {
            //返回当前登录者标识
            return "admin";
            //if (HttpContext.Current.User != null)
            //    return HttpContext.Current.User.Identity.Name;
            //else
            //    return string.Empty;
        }

        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns>用户IP地址</returns>
        public static string GetUserIp()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// 获取当前服务器名称
        /// </summary>
        /// <returns></returns>
        public static string GetServerName()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MachineName;
            }
            else
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// 获取用户请求页面地址
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrl()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                return HttpContext.Current.Request.RawUrl;
            }
            else
            {
                return "";
            }
        }

         
        /// <summary>
        ///c#加密不可逆加密 
        /// </summary>
        /// <param name="PasswordString">加密字符串</param>
        /// <param name="PasswordFormat">加密类型</param>
        /// <returns></returns>
        public static string EncryptPassword(string PasswordString, string PasswordFormat)
        {
            string encryptPassword = null;
            if (PasswordFormat == "SHA1")
            {
                encryptPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
            }
            else if (PasswordFormat == "MD5")

            {
                encryptPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5");
            }
            return encryptPassword;
        }
    }
}
