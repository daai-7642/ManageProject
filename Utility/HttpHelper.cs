using System;
using System.Collections.Generic;
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
                Log4net.LogHelper.WriteLog("链接请求错误:"+url,ex);
                return "err";
            }
        }
    }
}
