using Sampan.Service.Contract.Users.Dtos;

namespace Sampan.Service.Contract.Users
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService : IReadService<UserDto, UserListDto, GetUserListDto>
    {
    }
}