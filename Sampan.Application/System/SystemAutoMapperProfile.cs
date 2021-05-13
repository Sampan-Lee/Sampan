using AutoMapper;
using Sampan.Domain.System;
using Sampan.Service.Contract.System.Logs;
using Sampan.Service.Contract.System.Menus;
using Sampan.Service.Contract.System.Roles;
using Sampan.Service.Contract.System.SystemUsers;

namespace Sampan.Application.System.Logs
{
    public class SystemAutoMapperProfile : Profile
    {
        public SystemAutoMapperProfile()
        {
            #region 用户

            CreateMap<AdminUser, AdminUserDto>();
            CreateMap<AdminUser, AdminUserListDto>();
            CreateMap<CreateAdminUserDto, AdminUser>();
            CreateMap<UpdateAdminUserDto, AdminUser>();

            #endregion

            #region 角色

            CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleListDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
            CreateMap<Permission, RolePermissionDto>();

            #endregion

            #region 菜单

            CreateMap<CreateMenuDto, Menu>();
            CreateMap<Menu, UserMenuDto>();

            #endregion

            #region 日志

            CreateMap<Log, LogDto>();
            CreateMap<GetLogListDto, Log>();
            CreateMap<Log, LogListDto>();

            #endregion
        }
    }
}