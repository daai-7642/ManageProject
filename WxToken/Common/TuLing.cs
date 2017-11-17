using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WxToken.Common
{
    public class TuLing
    {

        public static string RequestTuling(string info)
        {
            info = HttpContext.Current.Server.UrlEncode(info);
            string url = "http://www.tuling123.com/openapi/api?key=72a2b178d33d9f04d7c0e7b11f7e0279&info=" + info;
            return RequestUrl(url, "get");
        }

        public static string GetTulingMsg(string info)
        {
            string jsonStr = RequestTuling(info);
            if (GetJsonValue(jsonStr, "code") == "100000")
            {
                return GetJsonValue(jsonStr, "text");
            }
            if (GetJsonValue(jsonStr, "code") == "200000")
            {
                return GetJsonValue(jsonStr, "text") + GetJsonValue(jsonStr, "url");
            }
            return "不知道怎么回复你哎";
        }

        public static string RequestUrl(string url, string method)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }

        public static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }
    }
}