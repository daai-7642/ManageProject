using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxToken.Models
{
    public class WxModel
    {
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
        /// <summary>
        /// 授权信息
        /// </summary>
        public string access_token { get; set; }
        public string openid { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
    
}