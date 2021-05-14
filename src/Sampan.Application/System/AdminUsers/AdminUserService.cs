using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Longbow.Security.Cryptography;
using Sampan.Common.Extension;
using Sampan.Domain.System;
using Sampan.Infrastructure.Repository;
using Sampan.Service.Contract.System.SystemUsers;

namespace Sampan.Application.System.Users
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class SystemUserService : CrudService<AdminUser, AdminUserDto, AdminUserListDto, GetAdminUserListDto,
            CreateAdminUserDto, UpdateAdminUserDto>,
        IAdminUserService
    {
        private readonly AdminUserManager _userManager;
        private readonly IRepository<UserRole> _userRoleRepository;

        public SystemUserService(AdminUserManager userManager,
            IRepository<UserRole> userRoleRepository,
            IRepository<AdminUser> repository) :
            base(repository)
        {
            _userManager = userManager;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 列表查询条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override ISelect<AdminUser> CreateFilteredQuery(GetAdminUserListDto input)
        {
            return Repository.Select.IncludeMany(a => a.Roles)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), a => a.Phone.Contains(input.Phone))
                .WhereIf(input.IsAdmin.HasValue, a => a.IsAdmin == input.IsAdmin.Value)
                .WhereIf(input.IsEnable.HasValue, a => a.IsEnable == input.IsEnable.Value);
        }

        /// <summary>
        /// 列表查询结果映射
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override async Task<AdminUserListDto> MapToListDtoAsync(AdminUser entity)
        {
            var userListDto = await base.MapToListDtoAsync(entity);
            userListDto.Roles = entity.Roles.Select(a => a.Name).ToList();
            return userListDto;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task CreateAsync(CreateAdminUserDto input)
        {
            var user = await _userManager.CreateAsync(input.LoginName,
                input.Password,
                input.Name,
                input.Phone,
                input.IsAdmin
            );

            await Repository.InsertAsync(user);
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AssignRoleAsync(AssignRoleDto input)
        {
            await _userRoleRepository.DeleteAsync(input.UserId);

            var entities = input.RoleIds.Select(a => new UserRole
            {
                UserId = input.UserId,
                RoleId = a
            });

            await Cache.RemoveAsync(SystemCacheKeyPrefixDefinition.UserPermission + input.UserId);

            await _userRoleRepository.InsertAsync(entities);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ResetPasswordAsync(ResetPasswordDto input)
        {
            var user = await Repository.Where(a => a.Id == input.Id).FirstAsync();
            ThrowIf(user == null, new AdminUserNotExistsException(input.Id.ToString()));

            user.PasswordSalt = LgbCryptography.GenerateSalt(); //生成密码盐
            user.Password = LgbCryptography.ComputeHash(input.Password, user.PasswordSalt);

            await Repository.UpdateAsync(user);
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task SetIsEnableAsync(SetEnableDto input)
        {
            var user = await Repository.GetAsync(input.Id);
            ThrowIf(user == null, new AdminUserNotExistsException(input.Id.ToString()));
            user.IsEnable = input.IsEnable;
            await Repository.UpdateAsync(user);
        }
    }
}