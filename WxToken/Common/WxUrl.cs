using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxToken.Common
{
    public static class WxUrl
    {
        public static string Jsapi_TicketUrl
        {
            get { return "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi"; }
        }
        public static string ShowQrCodeUrl
        {
            get
            {
                return "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";
            }
        }
    }
}