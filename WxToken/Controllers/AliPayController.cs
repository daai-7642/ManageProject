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
        private string privateKey = "MIIEowIBAAKCAQEAvJzdlrkma84AAG7gzxFg2wVBPR0TXXz390Lhy1+KuKT+t2PFiCxA9Ph6Rmz9DB1a8YRKLln0rELdtRFJ12Toio31YRa7LGMAZrb7Q8RlXpOzDBr5BuUp7vUsk25Qe9LhqnAs+rskmG5HukJYWxgDsVkjKl9GkIsh8EDyH/WJ+kbEZz6Tk3jS2umXz8mAgI5ZEXyFP3e3GjbrsDA4649P46LJmCv50n14tuxiNHOhFUYPiaidDrmKFUQCrzowZLXXM2boF1bNoQkXXKecdhLyfDO5szXOjDom7lW3GtFQbKT0dmI/xTzo5X70GanWovcJPYLuLtvBFgbixlPqMmzsewIDAQABAoIBAEYf1Errf5tpNZrznmWeQnJr27uLCd4iTlcB6M0iMoM/5OvuDkz4lxX9JAj3EIXmjB9rXeEp1MwO+DsPuHJ6s/J/oRF90A1KqaWGtpiVdlLZeyIvDRNBNHwBb5dI1meTGg+yMSbvWUXLCqP3cr47iXPwfiCM18F52R5oJx02vxvres4fgBzJZRSmma/FVPTKFEC2txvSQwqhvtggdDmojYhwpUu+hIwn76byjRI5Dfs+yKtTj0NvqbWmcqM03c1ovIV6nqagaJnNoL0luh5lRtpl0jY2af2mpMYb5Bwro8/YgWbBjgh5pCQzgwb/YOoU6zYg20KVj1slIzlQbiXzsNECgYEA+13NwIP3dt/ZNZ5PTRXR2l2iT0JIU8V9/DKtWndGv9bckQZmsKAIPlvBUgRE0KMc458zXHUwvG5RQ6eom2MugttIYTkAVPBn3t4hR1TBm4eDt22QAtUIH1CgnrCuMVhcrx7+E3IRBTJPdE/SEUEcgZs7osJDq+wU8v/R5AudnK8CgYEAwBbtgzAEdci4cOteHrfM+qY0cS06KJctiqE4L3qO4Vv9sqER6rdRPCIIM+GHwnge1Yfc9zu5HvVNup7YfW02ya2Lr/pGFuougXFdo2bgcgC4iKDLgwKlYtM4Ya+WYJTw3XuAncXtHhNNWHfN5Vk4Fiygaq0dtEl679ANJ++x1/UCgYBB2fw6EBh3cwNDcbrStgGpFFieLP4nvBhaRqh1h8PoJBDaiXPDl9kxBParVuT0R5cc5qsc8LKY2sm9UKHyO1SHAY1/suAsYGLF1ymet0yVQzY1iqVsqISdN5EsoZqw4LY/Rn5Hd92Pn/OCxBqDXKxsI8/Gvt/dnVaLpotFE+nxjwKBgQCAHIqJ7TN8TsNcZE3glNs77C+br/tS6QjxpXawi7/RY6X/RdeKQHsIbPYli+wccjq2VSe1KHrdv+L4bUqb1IQu2/UHCBdI3yTnJfG6sjlNL1fjn8I7fT9Keu7mj9HuVkeSn/T2xPPRFDSIpVaH+QokF91haFYgUMWSPaMYmI93JQKBgBdoQofgH3ihD7WIyN9F60OXWBv4LjCNm0YlRMkxW60DeRUTde4lxVO36eas03kUw1ekD5o3dmJz6witmzG5ztYvT2KWS8vJ2/8Jz/Lg0+QDbbiCyPM2F9x6pGEN95UfpIQJEAaHwiAWopXEkD+I2CWcBJ1VI0wm9h1A6InirkYf";
        private string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAvJzdlrkma84AAG7gzxFg2wVBPR0TXXz390Lhy1+KuKT+t2PFiCxA9Ph6Rmz9DB1a8YRKLln0rELdtRFJ12Toio31YRa7LGMAZrb7Q8RlXpOzDBr5BuUp7vUsk25Qe9LhqnAs+rskmG5HukJYWxgDsVkjKl9GkIsh8EDyH/WJ+kbEZz6Tk3jS2umXz8mAgI5ZEXyFP3e3GjbrsDA4649P46LJmCv50n14tuxiNHOhFUYPiaidDrmKFUQCrzowZLXXM2boF1bNoQkXXKecdhLyfDO5szXOjDom7lW3GtFQbKT0dmI/xTzo5X70GanWovcJPYLuLtvBFgbixlPqMmzsewIDAQAB";
        // GET: AliPay
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
"    \"out_trade_no\":\""+out_trade_no+"\"," +
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
        public ActionResult Search()
        {
            IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", "app_id", privateKey, "json", "1.0", "RSA2", publicKey, "utf-8", false);
            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"20150320010101001\"," +
            "\"trade_no\":\"2014112611001004680073956707\"" +
            "}";
            AlipayTradeQueryResponse response = client.Execute(request);
            return Content(response.Body);
        }

    }
}