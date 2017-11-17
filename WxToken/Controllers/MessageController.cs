using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WxToken.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            return Content("<xml><ToUserName><![CDATA[toUser]]></ToUserName><FromUserName><![CDATA[fromUser]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[image]]></MsgType><PicUrl><![CDATA[this is a url]]></PicUrl><MediaId><![CDATA[media_id]]></MediaId><MsgId>1234567890123456</MsgId></xml>;");
            return View();
        }
    }
}