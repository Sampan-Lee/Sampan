using System;
using FreeSql.DataAnnotations;

namespace Sampan.Domain.System
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table(Name = "SystemLog")]
    public class Log : Entity
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// guid链路
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 具体action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 日志记录路径
        /// </summary>
        public string CallSite { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 客户端代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [Navigate(nameof(UserId))]
        public virtual AdminUser AdminUser { get; set; }
    }
}