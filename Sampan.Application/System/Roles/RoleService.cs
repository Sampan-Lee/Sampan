using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sampan.Domain.System;
using Sampan.Infrastructure.AOP.Transactional;
using Sampan.Infrastructure.Repository;
using Sampan.Service.Contract.System.Roles;

namespace Sampan.Application.System.Roles
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService :
        CrudService<Role, RoleDto, RoleListDto, GetRoleListDto, CreateRoleDto, UpdateRoleDto>,
        IRoleService
    {
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IRepository<Permission> _permissionRepository;

        public RoleService(IRepository<RolePermission> rolePermissionRepository,
            IRepository<Permission> permissionRepository,
            IRepository<Role> repository) : base(repository)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<List<RolePermissionDto>> GetPermissionAsync(int id)
        {
            var rolePermissions = await _rolePermissionRepository
                .Where(a => a.RoleId == id)
                .ToListAsync<int>(a => a.PermissionId);

            var permissions = await _permissionRepository
                .WhereIf(rolePermissions.Any(), a => rolePermissions.Contains(a.Id))
                .ToListAsync();

            return Mapper.Map<List<RolePermissionDto>>(permissions);
        }

        [Transactional]
        public async Task AssignPermissionAsync(AssignPermissionDto input)
        {
            await _rolePermissionRepository.DeleteAsync(a => a.RoleId == input.Id);

            var entities = input.PermissionIds.Select(
                a => new RolePermission
                {
                    RoleId = input.Id,
                    PermissionId = a
                }
            );

            await _rolePermissionRepository.InsertAsync(entities);

            /*
             * 角色重新分配权限，对应的用户的所有权限都产生改变
             * 在这里把用户权限缓存清除，以保证角色分配权限实时生效
             */
            await Cache.RemoveByKeyPrefixAsync(SystemCacheKeyPrefixDefinition.UserPermission);
        }
    }
}