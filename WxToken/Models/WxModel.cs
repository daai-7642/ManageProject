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
    }
    public class WxUserInfo
    {
        //{"openid":"oQSqlxIdxzhOrxtxKI9z-KYGRAec","nickname":"未来","sex":1,"language":"zh_CN","city":"海淀","province":"北京","country":"中国","headimgurl":"http:\/\/wx.qlogo.cn\/mmopen\/vi_32\/PiajxSqBRaEKic06Y7mHrVcuwz3nficmnok6YR5Tzupl6ox6sVJ7gKcQszLRKCf4XTib6R9CUFDaywzyjzP4Rzmxcw\/0","privilege":[]}

        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string privilege { get; set; }
        public string unionid { get; set; }
    }
}