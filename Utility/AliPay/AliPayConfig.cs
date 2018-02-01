using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel;

namespace Utility.AliPay
{
    public static class AliPayConfig
    {
        public static bool IsTest
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["AliPayDebug"]);
            }
        }
        /// <summary>
        /// 支付宝私钥
        /// </summary>
        public static string merchant_private_key
        {
            get
            {
                return ConfigurationManager.AppSettings["PrivateKey"];
            }
        }
        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public static string alipay_public_key
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicKey"];
            }
        }
        /// <summary>
        /// 支付宝分配给开发者的应用ID	
        /// </summary>
        public static string app_id
        {
            get { return ConfigurationManager.AppSettings["AliPay_App_id"]; }
        }
        /// <summary>
        ///格式 仅支持JSON
        /// </summary>
        public static string format
        {
            get { return "json"; }
        }
        /// <summary>
        /// 编码
        /// </summary>

        public static string charset
        {
            get { return "utf-8"; }
        }
        /// <summary>
        /// 请求地址
        /// </summary>
        public static string serverUrl
        {
            get
            {
                if (IsTest)
                {
                    return OperateHelper.GetEnumDescription< AliPayApiUrl > (AliPayApiUrl.alipay_trade_pay_dev);
                }
                else
                {
                    return OperateHelper.GetEnumDescription<AliPayApiUrl>(AliPayApiUrl.alipay_trade_pay);
                 }
            }
        }
        public static string version
        {
            get { return "1.0"; }
        }
        public static bool keyFromsFile
        {
            get { return false; }
        }
        /// <summary>
        /// 签名类型
        /// </summary>
        public static string sginType
        {
            get { return "RSA2"; }
        }
    }
    public enum AliPayApiUrl
    {
        /// <summary>
        /// 正式
        /// </summary>
        [Description("https://openapi.alipay.com/gateway.do")]
        alipay_trade_pay,
        /// <summary>
        /// 测试
        /// </summary>
        [Description("https://openapi.alipaydev.com/gateway.do")]
        alipay_trade_pay_dev,

    }
}
