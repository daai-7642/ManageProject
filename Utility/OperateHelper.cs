using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utility
{
    public static class OperateHelper
    {
        /// <summary>
        /// 分页每页条数
        /// </summary>
        public static int PageSize
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"]); }
        }
        #region log日志参数

        /// <summary>
        /// 默认log
        /// </summary>
        public static string DefaultLog
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Defaultlog"]; }
        }
        /// <summary>
        /// 业务log
        /// </summary>
        public static string BuzLog
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Buzlog"]; }
        }
        /// <summary>
        /// 系统异常log
        /// </summary>
        public static string SystemErrorLog
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SystemErrorlog"]; }
        }
        /// <summary>
        /// job日志
        /// </summary>
        public static string JobLog
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Joblog"]; }
        }
        public static string ServiceLog
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["Servicelog"]; }
        }
        public static string UserLog
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UserLog"];
            }
        }
        #endregion  


        public static string GetXmlSqlString(string xmlNodeName)
        {
            return ReadXmlHelper.GetXmlValue(System.Web.HttpContext.Current.Server.MapPath("~/Config/SQLXml.xml"), xmlNodeName);
        }
        public static string GetSystemXmlString(string xmlNodeName, string key)
        {
            object obj = Cache.CacheHelper.Get("SystemXml" + xmlNodeName);
            if (obj == null)
            {
                var xmlList = ReadXmlHelper.GetXmlList(System.Web.HttpContext.Current.Server.MapPath("~/Config/SystemXml.xml"), xmlNodeName);
                Utility.Cache.CacheHelper.AddPermanent("SystemXml" + xmlNodeName, xmlList);
                obj = xmlList;
            }
            return (obj as Dictionary<string, string>).Count == 0 ? "暂无" : (obj as Dictionary<string, string>)[key];
        }

        public static string GetXmlSqlString(string xmlNodeName, bool IsReplace)
        {
            return ReadXmlHelper.GetXmlValue(System.Web.HttpContext.Current.Server.MapPath("~/Config/SQLXml.xml"), xmlNodeName, IsReplace);
        }
        public static string GetXmlSqlString(string url, string xmlNodeName)
        {
            return ReadXmlHelper.GetXmlValue(url + "/Config/SQLXml.xml", xmlNodeName);
        }
        public static string GetSettingValue(string SettingName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[SettingName];
        }
        public static string GetThisFullMethodName()
        {
            System.Reflection.MethodBase method = new System.Diagnostics.StackFrame(1).GetMethod();
            return method.ReflectedType.Name + "." + method.Name;
        }
        public static string GetThisClassName()
        {
            System.Reflection.MethodBase method = new System.Diagnostics.StackFrame(1).GetMethod();
            return method.ReflectedType.Name + ".";
        }
        public static bool IsAjaxReqest(string method = "GET")
        {
            return System.Web.HttpContext.Current.Request.HttpMethod == method;
        }
        //public static string GetCacheTime
        //{
        //    get
        //    {
        //        return System.Configuration.ConfigurationManager.AppSettings["CacheTime"];
        //    }
        //    set { GetCacheTime = value; }
        //}
        public static string UserSessionKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CurrentUserKey"];
            }
        }
        /// <summary>
        /// 获取枚举值上的Description特性的说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="obj">枚举值</param>
        /// <returns>特性的说明</returns>
        public static string GetEnumDescription<T>(T obj)
        {
            var type = obj.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, obj));
            DescriptionAttribute descAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
            {
                return string.Empty;
            }

            return descAttr.Description;
        }
        public static string AreaName
        {
            get
            {
                string areaName = "";
                string url = System.Web.HttpContext.Current.Request.RawUrl;
                if (url.Split('/').Length > 3)
                {
                    areaName = url.Substring(0, url.IndexOf('/', 1));
                }
               
                return areaName;
            }
        }
        public static string FileUrl
        {
            get
            {
                return "/Areas" + AreaName;
            }
        }
    }

}