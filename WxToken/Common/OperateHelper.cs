using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using WxToken.Models;
using static WxToken.Models.EnumEntity;

namespace WxToken.Common
{
    public static class OperateHelper
    {
        public static string Post(string url,string postDataStr)
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
            System.IO.Stream stream = request.GetResponse().GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            string retString = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return retString;
        }
        public static string Get(string url)
        {
            System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(url);
            System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            string result = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return result;
        }
        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        public static string Timestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public static string SHA1(string str)
        {
            return  FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");

        }
        /// <summary>
        /// 生成微信二维码
        /// </summary>
        /// <param name="access_token">token</param>
        /// <param name="expire_seconds">有效时间</param>
        /// <param name="action_name">场景名称</param>
        /// <param name="action_info">场景信息</param>
        /// <param name="scene_id">场景id</param>
        ///相关链接 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1443433542
        /// <returns></returns>
        public static WxQrCodeInfo CreateWxQRCode(string access_token, string expire_seconds, string action_name, string action_info, int scene_id)
        {
            string postDataStr = "";
            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + access_token;
            if (action_name.Equals(QRCodeType.QR_SCENE.ToString()))
            {
                postDataStr = "{\"expire_seconds\": 2592000,\"action_name\": \"" + action_name + "\", \"action_info\": {\"scene\": {\"scene_id\": "
                                  + scene_id + "" + "}}}";
            }
            else
            {
                postDataStr = "{\"action_name\": \"" + action_name + "\", \"action_info\": {\"scene\": {\"scene_id\": "
                                 + scene_id + "" + "}}}";
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            //retString
            WxQrCodeInfo qrCode = Newtonsoft.Json.JsonConvert.DeserializeObject<WxQrCodeInfo>(retString);
            return qrCode;
        }

    }
}