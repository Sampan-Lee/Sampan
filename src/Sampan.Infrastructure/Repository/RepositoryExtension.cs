using System;
using System.Linq.Expressions;
using FreeSql;

namespace Sampan.Infrastructure.Repository
{
    public static class RepositoryExtension
    {
        public static ISelect<TEntity> LeftJoin<TEntity>(this IRepository<TEntity> repository,
            Expression<Func<TEntity, bool>> exp) where TEntity : class
        {
            return repository.Select.LeftJoin(exp);
        }
    }
}