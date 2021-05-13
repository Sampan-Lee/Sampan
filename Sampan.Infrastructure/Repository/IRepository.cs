using FreeSql;

namespace Sampan.Infrastructure.Repository
{
    public interface IRepository<TEntity> : IBaseRepository<TEntity, int> where TEntity : class
    {
    }
}