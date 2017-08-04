using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public  class HttpHelper
    {
        public static string Get(string url)
        {
            try
            {
                System.Net.HttpWebRequest xhr = (HttpWebRequest)HttpWebRequest.Create(url);
                System.IO.Stream stream = xhr.GetResponse().GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.Default);
                string result = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                return result;
            }
            catch (Exception ex)
            {
                Log4net.ErrorLogHelper.WriteLog(url,ex);
                return "err";
            }
        }
        public static string Post(string url,string postDataStr)
        {
            string retString = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                retString = "err";
                Log4net.ErrorLogHelper.WriteLog("post错误",ex);
            }
            return retString;
        }
    }
}
