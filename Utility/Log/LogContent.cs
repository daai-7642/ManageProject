using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4net
{
    /// <summary>
    /// 包含了所有的自定字段属性
    /// </summary>
    public class LogContent
    {
        /// <summary>
        /// 时间类型 均为3
        /// </summary>
        public int Event_Type{get;set;}
              
        /// <summary>
        /// 日志分类描述，自定义
        /// </summary>
        public string EventCategory { get; set; }

       
        /// <summary>
        /// 日志分类号
        /// </summary>
        public int Event_ID { get; set; }
       
        /// <summary>
        /// 计算机IP
        /// </summary>
        public string ComputerName { get; set; }

        /// <summary>
        /// 计算机Mac 地址
        /// </summary>
        public string Mac_Address { get; set; }

        /// <summary>
        /// 系统登陆用户
        /// </summary>
        public string UserName { get; set; }
       
        /// <summary>
        /// Rier
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// Rier Recorder audit
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 页面url
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 日志描述信息
        /// </summary>
        public string Description { get; set; }

    }
}