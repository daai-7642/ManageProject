using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utility
{
    public static class ReadXmlHelper
    {
        /// <summary>
        /// 通过xml节点，获取value
        /// </summary>
        /// <param name="xmlurl">xml路径</param>
        /// <param name="xpath">节点名称 </param>
        /// <param name="key">需要获取的对象key</param>
        /// <returns></returns>
        public static string GetXmlValue(string xmlUrl, string nodeName, string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //加载指定xml文件
            xmlDoc.Load(xmlUrl);
            XmlNode xn = xmlDoc.SelectSingleNode(nodeName);
            Dictionary<string, string> list = new Dictionary<string, string>();
            List<string> li = new List<string>();
            string value = xn.SelectNodes(key)[0].InnerText;
            return value;
        }
        public static string GetXmlValue(string xmlUrl, string nodeName,bool IsReplace=true)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //加载指定xml文件
            xmlDoc.Load(xmlUrl);
            XmlNode parentxn = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.NextSibling.Name);
            XmlNode xn = parentxn.SelectSingleNode(nodeName);
             
            string value = xn.InnerText;
            if(IsReplace)
            {
                value = value.Replace("\r", "");
                value = value.Replace("\n", "");
            }
            return value;
        }
        /// <summary>
        /// 获取制定xml节点下的所有子
        /// </summary>
        /// <param name="xmlUrl"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetXmlList(string xmlUrl, string nodeName)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            if(System.IO.File.Exists(xmlUrl))
            {
                //加载指定xml文件
                xmlDoc.Load(xmlUrl);
                //XmlNode xn = xmlDoc.SelectSingleNode(nodeName);
                XmlNode parentxn = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.NextSibling.Name);
                XmlNode xn = parentxn.SelectSingleNode(nodeName);
                foreach (XmlNode item in xn.ChildNodes)
                {
                    XmlElement xe = (XmlElement)item;
                    list.Add(item.Name, item.InnerText);
                }
            }
            
            return list;
        }

        public static DataTable GetXmlAttrList(string xmlUrl, string nodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //加载指定xml文件
            xmlDoc.Load(xmlUrl);
            XmlNode parentxn = xmlDoc.SelectSingleNode(xmlDoc.FirstChild.NextSibling.Name);
            XmlNode xn = parentxn.SelectSingleNode(nodeName);
            if (xn!=null)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new XmlNodeReader(xn));
                return ds.Tables[0];
            }
            else
            {
                return new DataTable();
            }
            
        }
    }

}