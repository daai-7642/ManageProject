using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4net
{
    public class ErrorLogHelper
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("errorLogger");
        public static void WriteLog(object info)
        {
            
                loginfo.Error(info);
           
        }

    }
}
