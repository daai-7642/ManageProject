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

        public AlipayTradeQueryResponse QueryData(AlipayTradeQueryRequest request)
        { 
             var resp = client.SdkExecute(request);
             string resultData=  HttpHelper.Post(AliPayConfig.serverUrl,resp.Body);
            LogHelper.WriteLog("支付查询账单", resultData);
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
