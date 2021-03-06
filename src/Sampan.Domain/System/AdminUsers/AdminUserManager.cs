using System.Threading.Tasks;
using JetBrains.Annotations;
using Longbow.Security.Cryptography;
using Sampan.Common.Extension;
using Sampan.Common.Util;
using Sampan.Infrastructure.Repository;
using NotImplementedException = System.NotImplementedException;

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
            [NotNull] string phone
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
                IsEnable = true
            };
        }

        public async Task ChangeNameAsync([NotNull] AdminUser user, [NotNull] string newName)
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingUser = await _repository.Where(a => a.Name == newName).FirstAsync();
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new AdminUserAlreadyExistsException(newName);
            }

            user.ChangeName(newName);
        }

        public async Task ChangePhoneAsync([NotNull] AdminUser user, [NotNull] string newPhone)
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrWhiteSpace(newPhone, nameof(newPhone));

            var existingUser = await _repository.Where(a => a.Phone == newPhone).FirstAsync();
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new AdminUserAlreadyExistsSamePhoneException(newPhone);
            }

            user.ChangePhone(newPhone);
        }

        public async Task ChangeEmailAsync(AdminUser user, string newEmail)
        {
            Check.NotNull(user, nameof(user));
            if (newEmail.IsNullOrEmpty()) return;

            var existingUser = await _repository.Where(a => a.Email == newEmail).FirstAsync();
            if (existingUser != null && existingUser.Id != user.Id)
            {
                throw new AdminUserAlreadyExistsSameEmailException(newEmail);
            }

            user.ChangeEmail(newEmail);
        }
    }
}