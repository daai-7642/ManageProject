using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        public static string GetMacAddress()
        {
            string mac = "";
            try
            {
                //获取网卡硬件地址 
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();

                List<string> list = new List<string>();

                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();

                        list.Add(mac);
                    }
                }
                moc = null;
                mc = null;

                mac = list[list.Count - 1];

                return mac;
            }
            catch
            {
                return "unknow";
            }
        }
        public static string Pay()
        {
            IAopClient client = new DefaultAopClient("https://openapi.alipaydev.com/gateway.do", "2016091300499167", "MIIEowIBAAKCAQEAvJzdlrkma84AAG7gzxFg2wVBPR0TXXz390Lhy1+KuKT+t2PFiCxA9Ph6Rmz9DB1a8YRKLln0rELdtRFJ12Toio31YRa7LGMAZrb7Q8RlXpOzDBr5BuUp7vUsk25Qe9LhqnAs+rskmG5HukJYWxgDsVkjKl9GkIsh8EDyH/WJ+kbEZz6Tk3jS2umXz8mAgI5ZEXyFP3e3GjbrsDA4649P46LJmCv50n14tuxiNHOhFUYPiaidDrmKFUQCrzowZLXXM2boF1bNoQkXXKecdhLyfDO5szXOjDom7lW3GtFQbKT0dmI/xTzo5X70GanWovcJPYLuLtvBFgbixlPqMmzsewIDAQABAoIBAEYf1Errf5tpNZrznmWeQnJr27uLCd4iTlcB6M0iMoM/5OvuDkz4lxX9JAj3EIXmjB9rXeEp1MwO+DsPuHJ6s/J/oRF90A1KqaWGtpiVdlLZeyIvDRNBNHwBb5dI1meTGg+yMSbvWUXLCqP3cr47iXPwfiCM18F52R5oJx02vxvres4fgBzJZRSmma/FVPTKFEC2txvSQwqhvtggdDmojYhwpUu+hIwn76byjRI5Dfs+yKtTj0NvqbWmcqM03c1ovIV6nqagaJnNoL0luh5lRtpl0jY2af2mpMYb5Bwro8/YgWbBjgh5pCQzgwb/YOoU6zYg20KVj1slIzlQbiXzsNECgYEA+13NwIP3dt/ZNZ5PTRXR2l2iT0JIU8V9/DKtWndGv9bckQZmsKAIPlvBUgRE0KMc458zXHUwvG5RQ6eom2MugttIYTkAVPBn3t4hR1TBm4eDt22QAtUIH1CgnrCuMVhcrx7+E3IRBTJPdE/SEUEcgZs7osJDq+wU8v/R5AudnK8CgYEAwBbtgzAEdci4cOteHrfM+qY0cS06KJctiqE4L3qO4Vv9sqER6rdRPCIIM+GHwnge1Yfc9zu5HvVNup7YfW02ya2Lr/pGFuougXFdo2bgcgC4iKDLgwKlYtM4Ya+WYJTw3XuAncXtHhNNWHfN5Vk4Fiygaq0dtEl679ANJ++x1/UCgYBB2fw6EBh3cwNDcbrStgGpFFieLP4nvBhaRqh1h8PoJBDaiXPDl9kxBParVuT0R5cc5qsc8LKY2sm9UKHyO1SHAY1/suAsYGLF1ymet0yVQzY1iqVsqISdN5EsoZqw4LY/Rn5Hd92Pn/OCxBqDXKxsI8/Gvt/dnVaLpotFE+nxjwKBgQCAHIqJ7TN8TsNcZE3glNs77C+br/tS6QjxpXawi7/RY6X/RdeKQHsIbPYli+wccjq2VSe1KHrdv+L4bUqb1IQu2/UHCBdI3yTnJfG6sjlNL1fjn8I7fT9Keu7mj9HuVkeSn/T2xPPRFDSIpVaH+QokF91haFYgUMWSPaMYmI93JQKBgBdoQofgH3ihD7WIyN9F60OXWBv4LjCNm0YlRMkxW60DeRUTde4lxVO36eas03kUw1ekD5o3dmJz6witmzG5ztYvT2KWS8vJ2/8Jz/Lg0+QDbbiCyPM2F9x6pGEN95UfpIQJEAaHwiAWopXEkD+I2CWcBJ1VI0wm9h1A6InirkYf",
                "json", "1.0", "RSA2", "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAvJzdlrkma84AAG7gzxFg2wVBPR0TXXz390Lhy1+KuKT+t2PFiCxA9Ph6Rmz9DB1a8YRKLln0rELdtRFJ12Toio31YRa7LGMAZrb7Q8RlXpOzDBr5BuUp7vUsk25Qe9LhqnAs+rskmG5HukJYWxgDsVkjKl9GkIsh8EDyH/WJ+kbEZz6Tk3jS2umXz8mAgI5ZEXyFP3e3GjbrsDA4649P46LJmCv50n14tuxiNHOhFUYPiaidDrmKFUQCrzowZLXXM2boF1bNoQkXXKecdhLyfDO5szXOjDom7lW3GtFQbKT0dmI/xTzo5X70GanWovcJPYLuLtvBFgbixlPqMmzsewIDAQAB", "utf-8", false);
            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            request.BizContent = "{" +
            "    \"body\":\"对一笔交易的具体描述信息。如果是多种商品，请将商品描述字符串累加传给body。\"," +
            "    \"subject\":\"大可乐\"," +
            "    \"out_trade_no\":\"" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "\"," +
            "    \"timeout_express\":\"7200m\"," +
            "    \"total_amount\":0.01," +
            "    \"product_code\":\"QUICK_WAP_WAY\"" +
            "  }" ;
            AlipayTradeWapPayResponse response = client.pageExecute(request);
            string form = response.Body;
            return  (form);

        }

    }

}