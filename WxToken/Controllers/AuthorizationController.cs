using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WxToken.Common;
using WxToken.Models;

namespace WxToken.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// snsapi_base 只能通过code获取openid，snsapi_userinfo 可通过code获取用户信息
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public ActionResult GetAuthorizeCode(string state)
        {
            string code = Request["code"];
            if (code==null)
            {
                string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={2}#wechat_redirect", WxConfig.AppId, HttpUtility.UrlEncode(WxConfig.CurrentHost + Request.Url.AbsolutePath), state);
                //WxHelper.HttpGetRequest(url);
                return Redirect(url);
            }
            else
            { 
                string openidUrl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code ", WxConfig.AppId,WxConfig.Secret,code);
                string baseResult=WxHelper.HttpGetRequest(openidUrl);
                WxModel wxModel = JsonConvert.DeserializeObject<WxModel>(baseResult);
                
                
                string userInfOUrl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN ", wxModel.access_token, wxModel.openid);
                string userInfoResult = WxHelper.HttpGetRequest(userInfOUrl);
                return Content(Request.Url.AbsoluteUri);
            }
        }
    }
}