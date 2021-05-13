using System.Threading.Tasks;
using JetBrains.Annotations;
using Longbow.Security.Cryptography;
using Sampan.Common.Util;
using Sampan.Infrastructure.Repository;

namespace Sampan.Domain.System
{
    public class AdminUserManager : DomainManager
    {
        private readonly IRepository<AdminUser> _repository;

        public AdminUserManager(IRepository<AdminUser> repository)
        {
            _repository = repository;
        }

        public async Task<AdminUser> CreateAsync(
            [NotNull] string loginName,
            [CanBeNull] string password,
            [NotNull] string name,
            [NotNull] string phone,
            [CanBeNull] bool? isAdmin
        )
        {
            Check.NotNullOrWhiteSpace(loginName, nameof(loginName));

            var loginNameExist = await _repository
                .Where(a => a.LoginName == loginName)
                .AnyAsync();
            ThrowIf(loginNameExist, new AdminUserAlreadyExistsException(loginName));

            Check.NotNullOrWhiteSpace(phone, nameof(phone));
            var phoneExist = await _repository.Where(a => a.Phone == phone).AnyAsync();
            ThrowIf(phoneExist, new AdminUserAlreadyExistsException(phone));


            var passwordSalt = LgbCryptography.GenerateSalt(); //生成密码盐

            return new AdminUser
            {
                LoginName = loginName,
                PasswordSalt = passwordSalt,
                Password = LgbCryptography.ComputeHash(password ?? loginName, passwordSalt), //创建用户时，密码跟用户名一致
                Name = name,
                Phone = phone,
                IsAdmin = isAdmin ?? false,
                IsEnable = true
            };
        }
    }
}