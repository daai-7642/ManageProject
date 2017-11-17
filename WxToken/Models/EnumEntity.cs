using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxToken.Models
{
    public static class EnumEntity
    {
        /// <summary>
        /// 二维码类型
        /// </summary>
        public enum QRCodeType
        {
            /// <summary>
            /// 永久
            /// </summary>
            QR_LIMIT_SCENE = 1,
            /// <summary>
            /// 临时
            /// </summary>
            QR_SCENE = 2,//临时
        }
    }
}