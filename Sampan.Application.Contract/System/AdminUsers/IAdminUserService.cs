using System.Threading.Tasks;

namespace Sampan.Service.Contract.System.SystemUsers
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IAdminUserService : ICrudService<AdminUserDto, AdminUserListDto, GetAdminUserListDto,
        CreateAdminUserDto, UpdateAdminUserDto>
    {
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AssignRoleAsync(AssignRoleDto input);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <returns></returns>
        Task ResetPasswordAsync(ResetPasswordDto input);

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <returns></returns>
        Task SetIsEnableAsync(SetEnableDto input);
    }
}