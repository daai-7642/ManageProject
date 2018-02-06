using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.AliPay
{
    public class AsynNotifyModel
    {
        /// <summary>
        /// 通知时间
        /// </summary>
        public string notify_time { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public string notify_type { get; set; }
        /// <summary>
        /// 通知校验ID
        /// </summary>
        public string notify_id { get; set; }
        /// <summary>
        /// 开发者appid
        /// </summary>
        public string app_id { get; set; }
        public string charset { get; set; }
        public string version { get; set; }
        public string sign_type { get; set; }
        public string sign { get; set; }
        public string trade_no { get; set; }
        public string out_trade_no { get; set; }
        public string out_biz_no { get; set; }
        public string buyer_id { get; set; }
        public string buyer_logon_id { get; set; }
        public string seller_id { get; set; }
        public string seller_email { get; set; }
        /// <summary>
        /// 交易状态
        /// WAIT_BUYER_PAY	交易创建，等待买家付款
        /// TRADE_CLOSED 未付款交易超时关闭，或支付完成后全额退款
        /// TRADE_SUCCESS   交易支付成功
        /// TRADE_FINISHED  交易结束，不可退款
        /// </summary>
        public string trade_status { get; set; }
        public string total_amount { get; set; }
        public string receipt_amount { get; set; }
        public string invoice_amount { get; set; }
        public string buyer_pay_amount { get; set; }
        public string point_amount { get; set; }
        public string refund_fee { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string gmt_create { get; set; }
        public string gmt_payment { get; set; }
        public string gmt_refund { get; set; }
        public string gmt_close { get; set; }
        public string fund_bill_list { get; set; }
        public string passback_params { get; set; }
        public string voucher_detail_list { get; set; }

    }
}
