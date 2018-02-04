using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility.AliPay;
using Utility.Cache;

namespace WxToken.Controllers
{
    public class AliPayController : Controller
    {
        public ActionResult MobilePay()
        {
            //appid ,private key ,public key,
            //测试 url https://openapi.alipaydev.com/gateway.do
            //正式 url https://openapi.alipay.com/gateway.do
            string out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            IAopClient client = new DefaultAopClient(AliPayConfig.serverUrl, AliPayConfig.app_id, AliPayConfig.merchant_private_key,
                AliPayConfig.format, AliPayConfig.version, AliPayConfig.sginType, AliPayConfig.alipay_public_key, AliPayConfig.charset, AliPayConfig.keyFromsFile);
            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            //支付异步回调地址
            request.SetNotifyUrl("http://1x687f9296.iok.la/AliPay/Receive_notify");
            request.SetReturnUrl("http://www.baidu.com");
            request.BizContent = "{" +
            "    \"body\":\"这是一个大可乐，有2.5L，大不大\"," +
            "    \"subject\":\"大可乐\"," +
            "    \"out_trade_no\":\"" + out_trade_no + "\"," +
            "    \"timeout_express\":\"90m\"," +
            "    \"total_amount\":0.01," +
            "    \"product_code\":\"QUICK_WAP_WAY\"" +
            "  }";
            AlipayTradeWapPayResponse response = client.pageExecute(request);
            string form = response.Body;
            return Content(form);

        }

        public ActionResult PcPay()
        {
            string out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmssfff");


            IAopClient client = new DefaultAopClient(AliPayConfig.serverUrl, AliPayConfig.app_id, AliPayConfig.merchant_private_key, AliPayConfig.format, AliPayConfig.version, AliPayConfig.sginType
               , AliPayConfig.alipay_public_key, AliPayConfig.charset, AliPayConfig.keyFromsFile);
            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();

            request.SetNotifyUrl("http://1x687f9296.iok.la/AliPay/Receive_notify");
            request.SetReturnUrl("http://www.baidu.com");




            //-----------------
            //AlipayTradePayModel model = new AlipayTradePayModel();
            //model.OutTradeNo = out_trade_no;
            //model.ProductCode = "FAST_INSTANT_TRADE_PAY";
            //model.TotalAmount = "8.88";
            //model.Subject = "zhifu";
            //model.Body = "zhifu";
            //request.SetBizModel(model);

            ///------------
            //    request.BizContent = "{" +
            //"    \"out_trade_no\":\"" + out_trade_no + "\"," +
            //"    \"product_code\":\"FAST_INSTANT_TRADE_PAY\"," +
            //"    \"total_amount\":88.88," +
            //"    \"subject\":\"Iphone616G\"," +
            //"    \"body\":\"Iphone616G\"," +

            //"  }";//填充业务参数

            //request.BizContent = "{" +
            //    "\"out_trade_no\":\"" + out_trade_no + "\"," +
            //    "\"scene\":\"bar_code\"," +
            //    "\"auth_code\":\"28763443825664394\"," +
            //    "\"product_code\":\"FAST_INSTANT_TRADE_PAY\"," +// FACE_TO_FACE_PAYMENT
            //    "\"subject\":\"Iphone616G\"," +
            //    "\"buyer_id\":\"2088202954065786\"," +
            //    "\"seller_id\":\"2088102146225135\"," +
            //    "\"total_amount\":88.88," +
            //    "\"discountable_amount\":8.88," +
            //    "\"body\":\"Iphone616G\"," +
            //    "\"goods_detail\":[{" +
            //    "\"goods_id\":\"apple-01\"," +
            //    "\"goods_name\":\"ipad\"," +
            //    "\"quantity\":1," +
            //    "\"price\":2000," +
            //    "\"goods_category\":\"34543238\"," +
            //    "\"body\":\"特价手机\"," +
            //    "\"show_url\":\"http://www.alipay.com/xxx.jpg\"" +
            //    "}]," +
            //    "\"operator_id\":\"yx_001\"," +
            //    "\"store_id\":\"NJ_001\"," +
            //    "\"terminal_id\":\"NJ_T_001\"," +
            //    "\"extend_params\":{" +
            //    "\"sys_service_provider_id\":\"2088511833207846\"" +
            //    "}," +
            //    "\"timeout_express\":\"90m\"" +
            //    "}";

            request.BizContent = "{" +
            "    \"body\":\"Iphone6 16G\"," +
            "    \"subject\":\"Iphone6 16G\"," +
            "    \"out_trade_no\":\"" + out_trade_no + "\"," +
            "    \"total_amount\":88.88," +
            "    \"product_code\":\"FAST_INSTANT_TRADE_PAY\"" +
            "  }";
            AlipayTradePagePayResponse response = client.pageExecute(request);
            return Content(response.Body);
        }



        public ActionResult Receive_notify(AsynNotifyModel payResult)
        {
            //string key = "PayOrderList";
            //var list = CacheFactory.Cache.GetCache<List<string>>(key);
            //if (list == null)
            //{
            //    list = new List<string>();
            //}
            //list.Add(out_trade_no);
            //CacheFactory.Cache.SetCache<List<string>>(key, list);
            LogHelper.WriteLog("支付结果回调", Newtonsoft.Json.JsonConvert.SerializeObject(payResult));
            return View("Index");
        }
        public ActionResult Index()
        {
            return View();
        }


    }
}