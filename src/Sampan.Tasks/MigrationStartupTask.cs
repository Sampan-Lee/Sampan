using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sampan.Common.Extension;
using Sampan.Domain.System;
using Sampan.Infrastructure.Repository;
using Sampan.Public.Permission;

namespace Sampan.Tasks
{
    public class MigrationStartupTask
    {
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly ILogger<MigrationStartupTask> _logger;

        public MigrationStartupTask(IRepository<Permission> permissionRepository,
            IRepository<RolePermission> rolePermissionRepository, ILogger<MigrationStartupTask> logger)
        {
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await SeedPermissionAsync();
                await InitAdminPermission();
            }
            catch (Exception ex)
            {
                var error = $"初始化数据失败！！！{ex.Message}{ex.StackTrace}{ex.InnerException}";
                _logger.LogError(error);
            }
        }

        private async Task SeedPermissionAsync()
        {
            //程序定义的权限
            List<PermissionDefinition> permissions = PermissionManager.GetPermissionDefinition();

            List<Permission> allPermissions = await _permissionRepository.Select.ToListAsync();

            await deletePermission(allPermissions, permissions);

            List<Permission> insertModulePermissions = new List<Permission>();
            List<Permission> updateModulePermissions = new List<Permission>();
            permissions.Where(a => a.ParentName.IsNullOrWhiteSpace()).ToList().ForEach(r =>
            {
                var query = allPermissions.Where(u => u.Module == r.Module)
                    .Where(u => u.Name == r.Name)
                    .Where(u => !u.ParentId.HasValue);

                if (!query.Any())
                {
                    insertModulePermissions.Add(new Permission
                    {
                        Module = r.Module,
                        Name = r.Name,
                        DisplayName = r.DisplayName
                    });
                }
                else
                {
                    if (query.Any(u => u.DisplayName != r.DisplayName))
                    {
                        var permission = query.FirstOrDefault();
                        permission.DisplayName = r.DisplayName;
                        updateModulePermissions.Add(permission);
                    }
                }
            });

            await _permissionRepository.InsertAsync(insertModulePermissions);
            _logger.LogInformation($"新增了{insertModulePermissions.Count}条菜单权限数据");

            await _permissionRepository.UpdateAsync(updateModulePermissions);
            _logger.LogInformation($"修改了{updateModulePermissions.Count}条菜单权限数据");

            var allModulePermissions = await _permissionRepository.Where(a => !a.ParentId.HasValue).ToListAsync();
            List<Permission> insertMenuPermissions = new List<Permission>();
            List<Permission> updateMenuPermissions = new List<Permission>();
            permissions.Where(a => !a.ParentName.IsNullOrWhiteSpace())
                .Where(a => allModulePermissions.Exists(m => m.Name == a.ParentName))
                .ToList()
                .ForEach(r =>
                {
                    var query = allPermissions.Where(u => u.Module == r.Module)
                        .Where(u => u.Name == r.Name)
                        .Where(u => u.ParentId.HasValue)
                        .Where(u => allModulePermissions.FirstOrDefault(a => a.Id == u.ParentId.Value)?.Name ==
                                    r.ParentName);
                    if (!query.Any())
                    {
                        insertMenuPermissions.Add(new Permission
                        {
                            ParentId = allModulePermissions
                                .FirstOrDefault(a => a.Module == r.Module && a.Name == r.ParentName).Id,
                            Module = r.Module,
                            Name = r.Name,
                            DisplayName = r.DisplayName
                        });
                    }
                    else
                    {
                        if (query.Any(u => u.DisplayName != r.DisplayName))
                        {
                            var permission = query.FirstOrDefault();
                            permission.DisplayName = r.DisplayName;
                            updateMenuPermissions.Add(permission);
                        }
                    }
                });
            
            await _permissionRepository.InsertAsync(insertMenuPermissions);
            _logger.LogInformation($"新增了{insertMenuPermissions.Count}条菜单权限数据");
            
            await _permissionRepository.UpdateAsync(updateMenuPermissions);
            _logger.LogInformation($"新增了{updateMenuPermissions.Count}条菜单权限数据");

            // var allMenuPermissions = await _permissionRepository
            //     .Where(a => a.ParentId.HasValue)
            //     .Where(a => !a.Parent.ParentId.HasValue)
            //     .ToListAsync();
            // List<Permission> insertOperationPermissions = new List<Permission>();
            // List<Permission> updateOperationPermissions = new List<Permission>();
            // permissions.Where(a => !a.ParentName.IsNullOrWhiteSpace())
            //     .Where(a => allMenuPermissions.Exists(m => m.Name == a.ParentName))
            //     .ToList()
            //     .ForEach(r =>
            //     {
            //         var query = allPermissions.Where(u => u.Module == r.Module)
            //             .Where(u => u.Name == r.Name)
            //             .Where(u => u.ParentId.HasValue)
            //             .Where(u => allMenuPermissions.FirstOrDefault(a => a.Id == u.ParentId.Value)?.Name ==
            //                         r.ParentName);
            //         if (!query.Any())
            //         {
            //             insertOperationPermissions.Add(new Permission
            //             {
            //                 ParentId = allMenuPermissions
            //                     .FirstOrDefault(a => a.Module == r.Module && a.Name == r.ParentName).Id,
            //                 Module = r.Module,
            //                 Name = r.Name,
            //                 DisplayName = r.DisplayName
            //             });
            //         }
            //         else
            //         {
            //             if (query.Any(u => u.DisplayName != r.DisplayName))
            //             {
            //                 var permission = query.FirstOrDefault();
            //                 permission.DisplayName = r.DisplayName;
            //                 updateOperationPermissions.Add(permission);
            //             }
            //         }
            //     });
            //
            // await _permissionRepository.InsertAsync(insertOperationPermissions);
            // _logger.LogInformation($"新增了{insertOperationPermissions.Count}条功能权限数据");
            //
            // await _permissionRepository.UpdateAsync(updateOperationPermissions);
            // _logger.LogInformation($"新增了{updateOperationPermissions.Count}条功能权限数据");
        }

        private async Task deletePermission(List<Permission> allPermissions,
            List<PermissionDefinition> permissions)
        {
            Expression<Func<RolePermission, bool>> expression = u => false;
            Expression<Func<Permission, bool>> permissionExpression = u => false;

            allPermissions.ForEach(permission =>
            {
                if (!permission.ParentId.HasValue)
                {
                    if (!permissions.Any(r => permission.Module == r.Module && r.Name == permission.Name))
                    {
                        permissionExpression = permissionExpression.Or(r => r.Id == permission.Id)
                            .Or(r => r.ParentId == permission.Id);

                        var itemIds = allPermissions.Where(a => a.ParentId == permission.Id)
                            .Select(a => a.Id);
                        expression = expression.Or(u => u.PermissionId == permission.Id)
                            .Or(u => itemIds.Contains(u.PermissionId));
                    }
                }
                else
                {
                    if (!permissions.Any(r =>
                        permission.Module == r.Module && r.Name == permission.Name &&
                        allPermissions.FirstOrDefault(a => a.Id == permission.ParentId.Value)?.Name == r.ParentName))
                    {
                        permissionExpression = permissionExpression.Or(r => r.Id == permission.Id);
                        expression = expression.Or(r => r.PermissionId == permission.Id);
                    }
                }
            });
            int effectRows = await _permissionRepository.DeleteAsync(permissionExpression);
            effectRows += await _rolePermissionRepository.DeleteAsync(expression);
            _logger.LogInformation($"删除了{effectRows}条数据");
        }

        private async Task InitAdminPermission()
        {
            //所有权限
            List<Permission> permissions = await _permissionRepository.Select.ToListAsync();

            //管理员权限
            var adminRolePermissionIds = await _rolePermissionRepository
                .Where(a => a.RoleId == 1)
                .ToListAsync(a => a.PermissionId);

            List<RolePermission> insertRolePermissions;
            if (adminRolePermissionIds.IsNullOrEmpty())
            {
                insertRolePermissions =
                    permissions.Select(u => new RolePermission
                    {
                        RoleId = 1,
                        PermissionId = u.Id
                    }).ToList();
            }
            else
            {
                insertRolePermissions = permissions
                    .Where(a => !adminRolePermissionIds.Contains(a.Id))
                    .Select(a =>
                        new RolePermission
                        {
                            RoleId = 1,
                            PermissionId = a.Id
                        }
                    ).ToList();
            }

            await _rolePermissionRepository.InsertAsync(insertRolePermissions);

            _logger.LogInformation($"新增了{insertRolePermissions.Count}条管理员权限数据");
        }
    }
}