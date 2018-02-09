using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using DataAccess;
using Entity;
using Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Utility.AliPay;

namespace AdminWeb.Controllers
{
    public class AliPayController : Controller
    {
        private IAopClient client = new AliPayHelper().Client();
        // GET: AliPay
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BillIndex()
        {
            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"\"," +
            "\"trade_no\":\"2018020121001004630200415086\"" +
            "}";
            var model = new AliPayHelper().QueryData(request);
            Entity.AlipayTradeQueryResponse entity = new Entity.AlipayTradeQueryResponse();
            DataTransFormDataAccess.AutoMapping<Aop.Api.Response.AlipayTradeQueryResponse, Entity.AlipayTradeQueryResponse>(model, entity);
            DataRepository.Add<Entity.AlipayTradeQueryResponse>(entity);

            return View();
        }
        /// <summary>
        /// 预支付，提供码，扫完付款
        /// </summary>
        /// <returns></returns>
        public ActionResult Precreate()
        {
            string out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            AlipayTradePrecreateRequest request = new AlipayTradePrecreateRequest();
            request.BizContent="{" +
                "    \"out_trade_no\":\"20150320010101002\"," +
                "    \"total_amount\":\"88.88\"," +
                "    \"subject\":\"Iphone6 16G\"," +
                "    \"store_id\":\"NJ_001\"," +
                "    \"timeout_express\":\"90m\"}";//设置业务参数
            // AlipayTradePrecreateResponse response = client.execute(request);
            AlipayTradePrecreateResponse response = new AliPayHelper().Client().pageExecute(request);
             
              //  var resp = new AliPayHelper().Client().SdkExecute(request);
            //string resultData = HttpHelper.Post(AliPayConfig.serverUrl, resp.Body);
            //LogHelper.WriteLog("扫码支付", resultData);
            return Content(response.Body);
        }
        /// <summary>
        /// 当面付扫码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult BarCodePay(string code)
        {
            string out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            AlipayTradePayRequest request = new AlipayTradePayRequest(); //创建API对应的request类
            request.BizContent="{" +
            "    \"out_trade_no\":\""+ out_trade_no + "\"," +
            "    \"scene\":\"bar_code\"," +
            "    \"auth_code\":\""+ code + "\"," +
            "    \"subject\":\"Iphone6 16G\"," +
            "    \"store_id\":\"NJ_001\"," +
            "    \"timeout_express\":\"2m\"," +
            "    \"total_amount\":\"88.88\"" +
            "  }"; //设置业务参数
            AlipayTradePayResponse response = client.pageExecute(request); //通过alipayClient调用API，获得对应的response类
            
            return Content(response.Body);
        }
        //支付回调
        public ActionResult Receive_notify(AsynNotifyModel payResult)
        { 
            LogHelper.WriteLog("支付结果回调", Newtonsoft.Json.JsonConvert.SerializeObject(payResult));
            return View("Index");
        }
        public ActionResult Scan()
        {
            return View();
        }
    }
}