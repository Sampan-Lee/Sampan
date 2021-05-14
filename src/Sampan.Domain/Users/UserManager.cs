using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sampan.Common.Extension;
using Sampan.Common.Util;
using Sampan.Infrastructure.Repository;

namespace Sampan.Domain.Users
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager : DomainManager
    {
        private readonly IRepository<User> _repository;

        public UserManager(IRepository<User> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<User> CreateAsync([NotNull] string phone, [CanBeNull] string name)
        {
            Check.NotNullOrEmpty(phone, nameof(phone));

            var exists = await _repository.Where(a => a.Phone == phone).AnyAsync();
            ThrowIf(exists, new UserAlreadyExistsException(phone));
            if (!name.IsNullOrEmpty())
            {
                exists = await _repository.Where(a => a.Name == name).AnyAsync();
                ThrowIf(exists, new UserAlreadyExistsException(name));
            }

            return new User
            {
                Phone = phone,
                Name = name ?? Guid.NewGuid().ToString(),
                RegisterTime = DateTime.Now
            };
        }
    }
}