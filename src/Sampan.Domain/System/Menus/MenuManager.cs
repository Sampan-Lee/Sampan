using System.Threading.Tasks;
using JetBrains.Annotations;
using Sampan.Common.Util;
using Sampan.Infrastructure.Repository;

namespace Sampan.Domain.System
{
    public class MenuManager : DomainManager
    {
        private readonly IRepository<Menu> _repository;

        public MenuManager(IRepository<Menu> repository)
        {
            _repository = repository;
        }

        public async Task<Menu> CreateAsync(
            [CanBeNull] int? parentId,
            [CanBeNull] int? permissionId,
            [NotNull] string name,
            [CanBeNull] string icon,
            [CanBeNull] string path)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var exists = await _repository.Where(a => a.Name == name).AnyAsync();
            ThrowIf(exists, new MenuAlreadyExsitsException(name));

            return new Menu
            {
                ParentId = parentId,
                PermissionId = permissionId,
                Name = name,
                Icon = icon,
                Path = path,
                IsEnable = true,
            };
        }
    }
}