using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WxToken.Models
{
    public static class WxConfig
    {
        public static string AppId
        {
            get
            {
                return ConfigurationManager.AppSettings["appid"];
            }

        }
        public static string Secret
        {
            get { return ConfigurationManager.AppSettings["secret"]; }

        }
    }
}