using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public AlipayTradeQueryResponse QueryData()
        {
             AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"\"," +
            "\"trade_no\":\"2018020121001004630200415086\"" +
            "}";
             var resp = client.SdkExecute(request);
             string resultData=  HttpHelper.Post(AliPayConfig.serverUrl,resp.Body);
            LogHelper.WriteLog("支付查询账单", resultData);

            //{"alipay_trade_query_response":{"code":"10000","msg":"Success","buyer_logon_id":"rpx***@sandbox.com","buyer_pay_amount":"0.00","buyer_user_id":"2088102175288635","buyer_user_type":"PRIVATE","invoice_amount":"0.00","out_trade_no":"20180201163746999","point_amount":"0.00","receipt_amount":"0.00","send_pay_date":"2018-02-01 16:42:06","total_amount":"88.88","trade_no":"2018020121001004630200415086","trade_status":"TRADE_SUCCESS"},"sign":"Zn7VUAgnVoCOI9ZMPP8flhjAg6Sqhic9b4YXQwfhC8JqY5dLaP3Q9Hkj1A2VECmIHKp2ftQyOgt5vjthFcU+cpEgf1uil+XIe2D2Gwq6N2F0njCumgfLyS6jP/IgYerI+ZU3AqGdLHIo7K6RigtTamurB9RGIVmdWjWVGMP2h2W32PYa1fWzn190btp2ja8YFBse5/UPWHcQczk2wxn0rxutceEK3J7rhC5kkwqg8//LaRWZfviXure5jccUaPN8rP1Bd2snL2dP20oqQSdv1fl2fTVBpqxCTl8fFu3BYSdk4069xcEHu8mitCF0TG4KMsk7acm+ibq8ehlmHkGSQw=="}
            JObject obj =  JObject.Parse( JObject.Parse(resultData)["alipay_trade_query_response"].ToString());
            var dic= Newtonsoft.Json.JsonConvert.DeserializeObject(JObject.Parse(resultData)["alipay_trade_query_response"].ToString());
            AlipayTradeQueryResponse response = new AlipayTradeQueryResponse();
            System.Reflection.PropertyInfo[] storefield = typeof(AlipayTradeQueryResponse).GetProperties();
            //var fobj = JObject.Parse(obj["alipay_trade_query_response"].ToString());
            var aa= storefield.Where(a => a.GetCustomAttributes(typeof(XmlAttribute), true).Any());
            //JObject.Parse(resultData)["alipay_trade_query_response"].ToString()
            string str = JObject.Parse(resultData)["alipay_trade_query_response"].ToString();
            XmlDocument doc1 = Newtonsoft.Json.JsonConvert.DeserializeXmlNode("{\"AlipayTradeQueryResponse\":" + str + "}");
            System.IO.StringReader strread = new System.IO.StringReader(doc1.OuterXml);
            AlipayTradeQueryResponse response1 = new System.Xml.Serialization.XmlSerializer(typeof(AlipayTradeQueryResponse)).Deserialize(strread) as AlipayTradeQueryResponse;
            LogHelper.WriteLog("支付查询账单xml反序列化model",  JsonConvert.SerializeObject(response1));
            return response1;
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
        ///// json字符串转换为Xml对象  
        ///// </summary>  
        ///// <param name="sJson"></param>  
        ///// <returns></returns>  
        //public static XmlDocument Json2Xml(string sJson)
        //{
        //    //XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(sJson), XmlDictionaryReaderQuotas.Max);  
        //    //XmlDocument doc = new XmlDocument();  
        //    //doc.Load(reader);  

        //    JavaScriptSerializer oSerializer = new JavaScriptSerializer();
        //     Newtonsoft.Json.JsonConvert.DeserializeObject("");
        //    Dictionary<string, object> Dic = (Dictionary<string, object>)oSerializer.DeserializeObject(sJson);
        //    XmlDocument doc = new XmlDocument();
        //    XmlDeclaration xmlDec;
        //    xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", "yes");
        //    doc.InsertBefore(xmlDec, doc.DocumentElement);
        //    XmlElement nRoot = doc.CreateElement("root");
        //    doc.AppendChild(nRoot);
        //    foreach (KeyValuePair<string, object> item in Dic)
        //    {
        //        XmlElement element = doc.CreateElement(item.Key);
        //        KeyValue2Xml(element, item);
        //        nRoot.AppendChild(element);
        //    }
        //    return doc;
        //}
    }
}
