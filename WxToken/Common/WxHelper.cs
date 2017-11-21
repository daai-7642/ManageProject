using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WxToken.Models;

namespace WxToken.Common
{
    public static class WxHelper
    {
        /// <summary>
        /// 获取微信AccessToken
        /// </summary>
        /// <param name="appid">APPIDy</param>
        /// <param name="secret">凭证</param>
        /// <returns></returns>
        public static string GetWXAccessToken(string appid, string secret)
        {
            string result = "";
            string access_token = "";
            try
            {
                if (CacheHelper.Get(appid) == null)
                {
                    System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret).ToString());
                    System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                    result = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                    Newtonsoft.Json.Linq.JObject remark = Newtonsoft.Json.Linq.JObject.Parse(result);

                    access_token = remark.GetValue("access_token").ToString();
                    CacheHelper.Add(appid, access_token, TimeSpan.FromSeconds(7000));
                    return access_token;
                }
                else
                {
                    return CacheHelper.Get(appid).ToString();
                }

            }
            catch (Exception ex)
            {
                //ErrorLogHelper.Error(ex);
                //BizLogHelper.InfoMessage("获取微信accesstoken", "appid:" + appid + ";secret:" + secret + ";返回值:" + result, null);
                return "err";
            }

        }
        /// <summary>
        /// 获取jsapiticket
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetWXJsapi_Ticket(string accessToken)
        {
            string result = "";
            string ticket = "";
            try
            {
                string key = WxConfig.AppId + "ticket";
                if (CacheHelper.Get(key) == null)
                {
                    System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(string.Format(WxUrl.Jsapi_TicketUrl, accessToken).ToString());
                    System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                    result = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                    Newtonsoft.Json.Linq.JObject remark = Newtonsoft.Json.Linq.JObject.Parse(result);

                    ticket = remark.GetValue("ticket").ToString();
                    CacheHelper.Add(key, ticket, TimeSpan.FromSeconds(7000));
                    return ticket;
                }
                else
                {
                    return CacheHelper.Get(key).ToString();
                }

            }
            catch (Exception ex)
            {
                //ErrorLogHelper.Error(ex);
                //BizLogHelper.InfoMessage("获取微信accesstoken", "appid:" + appid + ";secret:" + secret + ";返回值:" + result, null);
                return "err";
            }

        }

        public static string HttpGetRequest(string url)
        {
            string result = "";
            System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(url);
            System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            result = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return result;
        }
        public static string HttpPostRequest(string url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = payload.Length;

            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            //var retString = request.GetResponse() as HttpWebResponse;
            System.IO.Stream stream = request.GetResponse().GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            string retString = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return retString.ToString();
        }

        public static WxModel GetWXJsapi()
        {
            string accessToken = WxHelper.GetWXAccessToken(WxConfig.AppId, WxConfig.Secret);
            if (accessToken != "err")
            {
                string jsapi = WxHelper.GetWXJsapi_Ticket(accessToken);
                if (jsapi != "err")
                {
                    string noncestr = OperateHelper.GenerateNonceStr();
                    string timestamp = OperateHelper.Timestamp();
                    string url = WxConfig.CurrentHost + System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                    string str = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi, noncestr, timestamp, url);
                    string sign = OperateHelper.SHA1(str).ToLower();

                    WxModel wx = new WxModel()
                    {
                        appId = WxConfig.AppId,
                        nonceStr = noncestr,
                        timestamp = timestamp,
                        signature = sign,
                    };
                    return wx;
                }
            }
            return new WxModel();
        }
    }
}