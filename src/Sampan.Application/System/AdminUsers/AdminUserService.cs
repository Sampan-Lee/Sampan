using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Longbow.Security.Cryptography;
using Sampan.Common.Extension;
using Sampan.Domain.System;
using Sampan.Infrastructure.AOP.Transactional;
using Sampan.Infrastructure.Repository;
using Sampan.Public.Dto;
using Sampan.Service.Contract.System.SystemUsers;

namespace Sampan.Application.System.Users
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class AdminUserService : CrudService<AdminUser, AdminUserDto, AdminUserListDto, GetAdminUserListDto,
            CreateAdminUserDto, UpdateAdminUserDto>,
        IAdminUserService
    {
        private readonly AdminUserManager _userManager;
        private readonly IRepository<UserRole> _userRoleRepository;

        public AdminUserService(AdminUserManager userManager,
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
            return Repository.Include(a => a.CreateUser)
                .IncludeMany(a => a.Roles)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), a => a.Phone.Contains(input.Phone))
                .WhereIf(input.IsEnable.HasValue, a => a.IsEnable == input.IsEnable.Value);
        }

        /// <summary>
        /// 请求排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override ISelect<AdminUser> ApplySorting(ISelect<AdminUser> query, GetAdminUserListDto input)
        {
            if (input.Sort.ToInitialUpper() == nameof(AdminUserListDto.CreateUserName))
            {
                query.OrderByIf(true, a => a.CreateUser.Name, input.Asc);
                return query;
            }

            return base.ApplySorting(query, input);
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

        public override async Task<AdminUserDto> GetAsync(int id)
        {
            var adminUser = await Repository
                .IncludeMany(a => a.Roles)
                .Where(a => a.Id == id)
                .FirstAsync();

            var adminUserDto = Mapper.Map<AdminUserDto>(adminUser);
            adminUserDto.RoleIds = adminUser.Roles.Select(a => a.Id).ToList();

            return adminUserDto;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Transactional]
        public override async Task CreateAsync(CreateAdminUserDto input)
        {
            var user = await _userManager.CreateAsync(input.LoginName,
                input.Password,
                input.Name,
                input.Phone
            );
            await Repository.InsertAsync(user);

            var userRole = input.RoleIds.Select(a => new UserRole
            {
                UserId = user.Id,
                RoleId = a
            });
            await _userRoleRepository.InsertAsync(userRole);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        [Transactional]
        public override async Task UpdateAsync(int id, UpdateAdminUserDto input)
        {
            var user = await Repository.GetAsync(id);
            await _userManager.ChangeNameAsync(user, input.Name);
            await _userManager.ChangePhoneAsync(user, input.Phone);
            await _userManager.ChangeEmailAsync(user, input.Email);
            await Repository.UpdateAsync(user);

            var assignRoleDto = new AssignRoleDto
            {
                UserId = id,
                RoleIds = input.RoleIds
            };
            await AssignRoleAsync(assignRoleDto);
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AssignRoleAsync(AssignRoleDto input)
        {
            await _userRoleRepository.Where(a => a.UserId == input.UserId)
                .ToDelete()
                .ExecuteAffrowsAsync();

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