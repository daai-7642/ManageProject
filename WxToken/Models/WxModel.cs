using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxToken.Models
{
    public class WxModel
    {
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
    }
}