using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WxToken.Common;
using WxToken.Models;

namespace WxToken.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            return Content("<xml><ToUserName><![CDATA[toUser]]></ToUserName><FromUserName><![CDATA[fromUser]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[image]]></MsgType><PicUrl><![CDATA[this is a url]]></PicUrl><MediaId><![CDATA[media_id]]></MediaId><MsgId>1234567890123456</MsgId></xml>;");
            //return View();
        }
        /// <summary>
        /// 消息推送
        /// </summary>
        /// <returns></returns>
        public ActionResult MassTexting()
        {
            string accessToken= WxHelper.GetWXAccessToken(WxConfig.AppId,WxConfig.Secret);
            string msgUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token="+ accessToken;
            string openid = "oQSqlxIdxzhOrxtxKI9z-KYGRAec";
            string templateId = "BUG2myxVPYR04fxztBHazsCze7fNWAqNbBHHTNvBRkY";
            string data = "{\"touser\":\""+openid+"\",\"template_id\":\""+templateId+"\", \"url\":\"www.baidu.com\"}";
            WxHelper.HttpPostRequest(msgUrl, data);

            //string postXmlStr = "<xml><ToUserName><![CDATA[gh_807ce952e271]]></ToUserName><FromUserName><![CDATA[oQSqlxIdxzhOrxtxKI9z-KYGRAec]]></FromUserName></xml>;";
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(postXmlStr);
            //string result = WeiXinXML.CreateTextMsg(doc, "给你推送一条消息");
            return View();
        }
    }
}