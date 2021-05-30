using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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

        public static Task<List<TEntity>> ToTreeListAsync<TEntity>(
            this IRepository<TEntity> repository,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class
        {
            return repository.Select.ToTreeListAsync(cancellationToken);
        }
    }
}