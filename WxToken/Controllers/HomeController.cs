using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WxToken.Common;
using WxToken.Models;
using static WxToken.Models.EnumEntity;

namespace WxToken.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string echoStr = Request.QueryString["echostr"];
            Stream stream = Request.InputStream;
            byte[] byteArray = new byte[stream.Length];
            stream.Read(byteArray, 0, (int)stream.Length);
            string postXmlStr = System.Text.Encoding.UTF8.GetString(byteArray);

            LogHelper.WriteFile(Server.MapPath("~/Logs/"+DateTime.Now.ToString("yyyyMMdd")+".txt"), postXmlStr);
            if (!string.IsNullOrEmpty(postXmlStr))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(postXmlStr);
                //return Content(echoStr);
                string msg = responseMsg( doc);
                return Content(msg);
                //return Content("<xml><ToUserName><![CDATA[w_angchengfei]]></ToUserName><FromUserName><![CDATA[oqoTFvj5SIJy0btLb5AK15YADZQ8]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[this is a test]]></Content><MsgId>1234567890123456</MsgId></xml>");

            }
            return View();
        }
        public string responseMsg( XmlDocument xmlDoc)
        {
            string result = "";
            string msgType = WeiXinXML.GetFromXML(xmlDoc, "MsgType");
            switch (msgType)
            {
                case "event":
                    switch (WeiXinXML.GetFromXML(xmlDoc, "Event"))
                    {
                        case "SCAN"://扫码
                            string msg = "你居然是扫码进来的居然";
                            switch (WeiXinXML.GetFromXML(xmlDoc, "EventKey"))
                            {
                                case "1": //普通码
                                    msg += "是普通码!!";
                                    break;
                                case "2": //商品码
                                    msg += "是商品码!!";
                                    break;
                                case "11": //正常码
                                    msg += "是正常码！！";
                                    break;
                                default:
                                    msg += "这码我也不知道";
                                    break;
                            }
                            result = WeiXinXML.CreateTextMsg(xmlDoc, msg);
                            break;
                        case "subscribe": //订阅
                            result = WeiXinXML.CreateTextMsg(xmlDoc, "欢迎关注我们");
                            break;
                        case "unsubscribe": //取消订阅
                            result = WeiXinXML.CreateTextMsg(xmlDoc, "你居然取消关注我们了");
                            break;
                        case "CLICK":  
                            //DataTable dtMenuMsg = MenuMsgDal.GetMenuMsg(WXMsgUtil.GetFromXML(xmlDoc, "EventKey"));
                            //if (dtMenuMsg.Rows.Count > 0)
                            //{
                            //    List<Dictionary<string, string>> dictList = new List<Dictionary<string, string>>();
                            //    foreach (DataRow dr in dtMenuMsg.Rows)
                            //    {
                            //        Dictionary<string, string> dict = new Dictionary<string, string>();
                            //        dict["Title"] = dr["Title"].ToString();
                            //        dict["Description"] = dr["Description"].ToString();
                            //        dict["PicUrl"] = dr["PicUrl"].ToString();
                            //        dict["Url"] = dr["Url"].ToString();
                            //        dictList.Add(dict);
                            //    }
                            //    result = WXMsgUtil.CreateNewsMsg(xmlDoc, dictList);
                            //}
                            //else
                            //{
                            //    result = WXMsgUtil.CreateTextMsg(xmlDoc, "无此消息哦");
                            //}
                            break;
                        default:
                            break;
                    }
                    break;
                case "text":
                    string text = WeiXinXML.GetFromXML(xmlDoc, "Content");
                    if (text == "这个开发者好帅" || text == "土豪是傻逼" || text == "不按规则也能聊天")
                    {
                        if (text == "土豪是傻逼")
                        {
                            result = WeiXinXML.CreateTextMsg(xmlDoc, "这个傻逼喜欢谭小芹！");
                        }
                        else if (text == "这个开发者好帅")
                        {
                            result = WeiXinXML.CreateTextMsg(xmlDoc, "你说了句大实话啊！哈哈！");
                        }
                        else
                        {
                            result = WeiXinXML.CreateTextMsg(xmlDoc, TuLing.GetTulingMsg(text));
                        }
                    }
                    else
                    {
                        if (text.Contains("t"))
                        {
                            text = text.Replace("t", "");
                            result = WeiXinXML.CreateTextMsg(xmlDoc, TuLing.GetTulingMsg(text));
                        }
                        else
                        {
                            result = WeiXinXML.CreateTextMsg(xmlDoc, "你回复错误导致了土豪成为了傻逼，除非你回复：这个开发者好帅  或者回复：土豪是傻逼 或者你想和机器人聊天请回复格式：t+你好（例如：t我饿了）");
                        }
                    }

                    break;
                default:
                    break;
            }

            //if (!string.IsNullOrWhiteSpace(sAppID)) //没有AppID则不加密(订阅号没有AppID)
            //{
            //    //加密
            //    WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);
            //    string sEncryptMsg = ""; //xml格式的密文
            //    string timestamp = context.Request["timestamp"];
            //    string nonce = context.Request["nonce"];
            //    int ret = wxcpt.EncryptMsg(result, timestamp, nonce, ref sEncryptMsg);
            //    if (ret != 0)
            //    {
            //        FileLogger.WriteErrorLog(context, "加密失败，错误码：" + ret);
            //        return;
            //    }

            //    context.Response.Write(sEncryptMsg);
            //    context.Response.Flush();
            //}
            //else
            // {
            return result;
            //   }
        }
        public ActionResult CreateQrCode(int scene_id)
        {
            string access_token = WxHelper.GetWXAccessToken(WxConfig.AppId, WxConfig.Secret);
            var qrCode = OperateHelper.CreateWxQRCode(access_token, null, ((QRCodeType)(2)).ToString(), "暂无",scene_id);
            //return Json(qrCode,JsonRequestBehavior.AllowGet);
            //return Content(string.Format(WxUrl.ShowQrCodeUrl, qrCode.ticket));
            string fileName =  "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            new System.Net.WebClient().DownloadFile(string.Format(WxUrl.ShowQrCodeUrl, qrCode.ticket), Server.MapPath("~/Qrcode") + fileName);
            return View("/QrCode"+fileName as object);
        }
    }
}