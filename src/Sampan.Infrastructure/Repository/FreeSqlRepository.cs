using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FreeSql;
using Sampan.Infrastructure.CurrentUser;
using Sampan.Public.Entity;

namespace Sampan.Infrastructure.Repository
{
    public class FreeSqlRepository<TEntity> :
        DefaultRepository<TEntity, int>,
        IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ICurrentUser _currentUser;

        public FreeSqlRepository(ICurrentUser currentUser, UnitOfWorkManager uowManger)
            : base(uowManger?.Orm, uowManger)
        {
            _currentUser = currentUser;
        }

        #region 查询构造

        public ISelect<TEntity> Include<TNavigate>(Expression<Func<TEntity, TNavigate>> navigateSelector)
            where TNavigate : class
        {
            return Select.Include(navigateSelector);
        }

        public ISelect<TEntity> IncludeMany<TNavigate>(
            Expression<Func<TEntity, IEnumerable<TNavigate>>> navigateSelector, Action<ISelect<TNavigate>> then = null)
            where TNavigate : class
        {
            return Select.IncludeMany(navigateSelector);
        }

        public Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(Func<TEntity, TKey> keySelector,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Select.ToDictionaryAsync(keySelector, cancellationToken);
        }

        public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(Func<TEntity, TKey> keySelector,
            Func<TEntity, TElement> elementSelector,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Select.ToDictionaryAsync(keySelector, elementSelector, cancellationToken);
        }

        public Task<List<TReturn>> ToListAsync<TReturn>(Expression<Func<TEntity, TReturn>> @select,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Select.ToListAsync(@select, cancellationToken);
        }

        public Task<List<TDto>> ToListAsync<TDto>(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Select.ToListAsync<TDto>(cancellationToken);
        }

        #endregion

        #region 插入数据

        private void BeforeExecute(TEntity entity)
        {
            if (entity.Id == 0 && entity is ICreateEntity createEntity)
            {
                createEntity.CreateTime = DateTime.Now;
                if (createEntity.CreateUserId == 0)
                {
                    createEntity.CreateUserId = _currentUser?.Id ?? 0;
                }
            }

            if (entity is IUpdateEntity updateEntity)
            {
                updateEntity.UpdateTime = DateTime.Now;
                updateEntity.UpdateUserId = _currentUser?.Id ?? 0;
            }
        }

        public override Task<TEntity> InsertAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            BeforeExecute(entity);
            return base.InsertAsync(entity, cancellationToken);
        }

        public override Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            foreach (var entity in enumerable)
            {
                BeforeExecute(entity);
            }

            return base.InsertAsync(enumerable, cancellationToken);
        }

        #endregion

        #region 修改数据

        public override Task<int> UpdateAsync(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeExecute(entity);
            return base.UpdateAsync(entity, cancellationToken);
        }

        public override Task<int> UpdateAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            foreach (var entity in enumerable)
            {
                BeforeExecute(entity);
            }

            return base.UpdateAsync(enumerable, cancellationToken);
        }

        #endregion

        #region 删除数据

        public override Task<int> DeleteAsync(int id,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ISoftDeleteEntity)))
            {
                return Orm.Update<TEntity>()
                    .Set(a => (a as ISoftDeleteEntity).IsDelete, true)
                    .Set(a => (a as ISoftDeleteEntity).DeleteUserId, _currentUser.Id)
                    .Set(a => (a as ISoftDeleteEntity).DeleteTime, DateTime.Now)
                    .Where(a => a.Id == id)
                    .ExecuteAffrowsAsync(cancellationToken);
            }

            return base.DeleteAsync(id, cancellationToken);
        }

        public Task<int> DeleteAsync(IEnumerable<int> ids,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ISoftDeleteEntity)))
            {
                return Orm.Update<TEntity>()
                    .Set(a => (a as ISoftDeleteEntity).IsDelete, true)
                    .Set(a => (a as ISoftDeleteEntity).DeleteUserId, _currentUser.Id)
                    .Set(a => (a as ISoftDeleteEntity).DeleteTime, DateTime.Now)
                    .Where(a => ids.Contains(a.Id))
                    .ExecuteAffrowsAsync(cancellationToken);
            }

            return Orm.Delete<TEntity>(ids)
                .ExecuteAffrowsAsync(cancellationToken);
        }

        public override Task<int> DeleteAsync(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (entity is ISoftDeleteEntity)
            {
                return Orm.Update<TEntity>(entity)
                    .Set(a => (a as ISoftDeleteEntity).IsDelete, true)
                    .Set(a => (a as ISoftDeleteEntity).DeleteUserId, _currentUser.Id)
                    .Set(a => (a as ISoftDeleteEntity).DeleteTime, DateTime.Now)
                    .ExecuteAffrowsAsync(cancellationToken);
            }

            return base.DeleteAsync(entity, cancellationToken);
        }

        public override Task<int> DeleteAsync(IEnumerable<TEntity> entitys,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ISoftDeleteEntity)))
            {
                return Orm.Update<TEntity>(entitys)
                    .Set(a => (a as ISoftDeleteEntity).IsDelete, true)
                    .Set(a => (a as ISoftDeleteEntity).DeleteUserId, _currentUser.Id)
                    .Set(a => (a as ISoftDeleteEntity).DeleteTime, DateTime.Now)
                    .ExecuteAffrowsAsync(cancellationToken);
            }


            return base.DeleteAsync(entitys, cancellationToken);
        }

        public override Task<int> DeleteAsync(Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ISoftDeleteEntity)))
            {
                return Orm.Update<TEntity>()
                    .Set(a => (a as ISoftDeleteEntity).IsDelete, true)
                    .Set(a => (a as ISoftDeleteEntity).DeleteUserId, _currentUser.Id)
                    .Set(a => (a as ISoftDeleteEntity).DeleteTime, DateTime.Now)
                    .Where(expression)
                    .ExecuteAffrowsAsync(cancellationToken);
            }

            return base.DeleteAsync(expression, cancellationToken);
        }

        public override Task<TEntity> InsertOrUpdateAsync(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeExecute(entity);
            return base.InsertOrUpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}