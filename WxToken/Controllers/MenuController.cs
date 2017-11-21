using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WxToken.Common;
using WxToken.Models;

namespace WxToken.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            string accessToken = WxHelper.GetWXAccessToken(WxConfig.AppId, WxConfig.Secret);


            string url = " https://api.weixin.qq.com/cgi-bin/menu/create?access_token="+ accessToken;
            string postDataStr = "{\"button\":[{\"type\":\"view\",\"name\":\"首页菜单\",\"url\":\"" + WxConfig.CurrentHost+ "/Home/HomeIndex\"},{\"name\":\"菜单\",\"sub_button\":[{\"type\":\"scancode_waitmsg\",\"name\":\"扫一扫\",   \"key\": \"rselfmenu_0_1\", \"sub_button\": [ ]},{\"type\":\"view\",\"name\":\"视频\",\"url\":\"http://v.qq.com/\"},{\"type\":\"click\",\"name\":\"赞一下我们\",\"key\":\"V1001_GOOD\"}]}]}";

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
            return Content(retString.ToString());
        }
    }
    

}