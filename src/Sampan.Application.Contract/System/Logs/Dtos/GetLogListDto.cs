using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.Logs
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLogListDto : GetPageDto
    {
        /// <summary>
        /// 解决方案名称
        /// </summary>
        public string Application { get; set; }
        
        /// <summary>
        /// 请求链路ID
        /// </summary>
        public string TraceId { get; set; }
        
        /// <summary>
        /// 请求链路ID
        /// </summary>
        public string UserName { get; set; }
    }
}