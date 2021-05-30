using Sampan.Public.Dto;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 获取用户业务实体
    /// </summary>
    public class GetAdminUserListDto : GetPageDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public bool? IsEnable { get; set; }
    }
}