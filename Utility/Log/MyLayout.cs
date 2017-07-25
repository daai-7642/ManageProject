using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using log4net.Layout.Pattern;
using log4net.Layout;
using log4net.Core;
using System.Reflection;

namespace Log4net
{
    class MyLayout : PatternLayout
    {
        public MyLayout()
        {
            this.AddConverter("property", typeof(MyMessagePatternConverter));
        }
    }
}