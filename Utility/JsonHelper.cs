using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static  class JsonHelper
    {
        public static string ObjectToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
