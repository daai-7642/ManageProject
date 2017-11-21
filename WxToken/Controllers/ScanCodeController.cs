using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WxToken.Common;
using WxToken.Models;

namespace WxToken.Controllers
{
    public class ScanCodeController : Controller
    {
        // GET: ScanCode
        public ActionResult Index()
        {

            string accessToken = WxHelper.GetWXAccessToken(WxConfig.AppId, WxConfig.Secret);
            if(accessToken != "err")
            {
                string jsapi = WxHelper.GetWXJsapi_Ticket(accessToken);
                if(jsapi!="err")
                {
                    LogHelper.WriteFile(Server.MapPath("~/Logs/jsapi.txt"), jsapi);

                    string noncestr = OperateHelper.GenerateNonceStr();
                    string timestamp = OperateHelper.Timestamp();
                    string url = WxConfig.CurrentHost+Request.Url.AbsolutePath;
                    string str = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi, noncestr, timestamp, url);
                    string sign = OperateHelper.SHA1(str).ToLower();
                    LogHelper.WriteFile(Server.MapPath("~/Logs/jsapi.txt"), str);

                    WxModel wx = new WxModel()
                    {
                        appId = WxConfig.AppId,
                        nonceStr = noncestr,
                        timestamp = timestamp,
                        signature = sign,
                    };
                    return View(wx);
                }
                return Content("apierr");
            }
            else
            {
                return Content("err");
            }
            
        }
       
    }
}