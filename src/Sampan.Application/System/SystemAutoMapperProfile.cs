using AutoMapper;
using Sampan.Domain.System;
using Sampan.Service.Contract.System.Logs;
using Sampan.Service.Contract.System.Menus;
using Sampan.Service.Contract.System.Permissions.Dtos;
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

            CreateMap<Menu, MenuListDto>()
                .ForMember(dest => dest.PermissionName,
                    opt => opt.MapFrom(src => src.Permission.DisplayName)
                );
            CreateMap<Menu, MenuDto>();
            CreateMap<CreateMenuDto, Menu>();
            CreateMap<UpdateMenuDto, Menu>();

            #endregion

            #region 权限

            CreateMap<Permission, PermissionDto>();

            #endregion

            #region 日志

            CreateMap<Log, LogDto>();
            CreateMap<GetLogListDto, Log>();
            CreateMap<Log, LogListDto>();

            #endregion
        }
    }
}