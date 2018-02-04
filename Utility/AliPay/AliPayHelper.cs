using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.AliPay
{
    public class AliPayHelper
    {
        IAopClient client = null;
        public AliPayHelper()
        {
              client = new DefaultAopClient(AliPayConfig.serverUrl, AliPayConfig.app_id, AliPayConfig.merchant_private_key, AliPayConfig.format, AliPayConfig.version, AliPayConfig.sginType
            , AliPayConfig.alipay_public_key, AliPayConfig.charset, AliPayConfig.keyFromsFile);
        }

        public string QueryData()
        {
             AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"\"," +
            "\"trade_no\":\"2018020121001004630200415086\"" +
            "}";
             var resp = client.SdkExecute(request);
             string resultData=  HttpHelper.Post(AliPayConfig.serverUrl,resp.Body);
            JObject obj=  JObject.Parse( JObject.Parse(resultData)["alipay_trade_query_response"].ToString());
            AlipayTradeQueryResponse response = new AlipayTradeQueryResponse();
            foreach (var item in obj)
            {
                System.Reflection.PropertyInfo[] storefield = typeof(AlipayTradeQueryResponse).GetProperties();
               System.Xml.Serialization. XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(AlipayTradeQueryResponse));
                System.Reflection.PropertyInfo t = storefield.Where(a =>a.Name == item.Key).First();
                if (t != null)
                {
                    t.SetValue(response,"", null);
                }
            }
            return resultData;
        }
        public void QueryBody()
        {
            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"\"," +
            "\"trade_no\":\"2018020121001004630200415086\"" +
            "}";
            AlipayTradeQueryResponse response = client.pageExecute(request);
            System.Web.HttpContext.Current.Response.Write(response.Body);
        }
    }
}
