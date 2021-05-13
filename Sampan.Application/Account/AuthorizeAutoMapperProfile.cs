using AutoMapper;
using Sampan.Domain.System;
using Sampan.Domain.Users;
using Sampan.Service.Contract.Account.AdminAccounts;
using Sampan.Service.Contract.Account.UserAccounts;

namespace Sampan.Application.Authorizes
{
    public class AuthorizeAutoMapperProfile : Profile
    {
        public AuthorizeAutoMapperProfile()
        {
            #region 管理员用户

            CreateMap<AdminUser, AdminLoginDto>();

            #endregion

            #region App用户

            CreateMap<User, UserLoginDto>();

            #endregion
        }
    }
}