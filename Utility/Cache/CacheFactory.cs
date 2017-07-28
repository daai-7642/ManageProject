using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utility.Cache
{
    public class CacheFactory
    {
        public static BaseCache Cache
        {
            get
            {
                var nspace = typeof(BaseCache);
                var fullName = nspace.FullName;
                var nowspace = fullName.Substring(0, fullName.LastIndexOf('.') + 1);
                string cacheType = System.Configuration.ConfigurationManager.AppSettings["CacheType"];
                return Assembly.GetExecutingAssembly().CreateInstance(nowspace + cacheType, true) as BaseCache;
            }
        }
    }
}
