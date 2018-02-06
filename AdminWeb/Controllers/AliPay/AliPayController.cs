using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using DataAccess;
using Entity;
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
            var model= new AliPayHelper().QueryData(request);
            Entity.AlipayTradeQueryResponse entity = new Entity.AlipayTradeQueryResponse();
             DataTransFormDataAccess.AutoMapping<Aop.Api.Response.AlipayTradeQueryResponse, Entity.AlipayTradeQueryResponse>(model, entity);
            DataRepository.Add<Entity.AlipayTradeQueryResponse>(entity);
            
            return View();
        }
    }
}