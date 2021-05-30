using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FreeSql;

namespace Sampan.Infrastructure.Repository
{
    public interface IRepository<TEntity> : IBaseRepository<TEntity, int> where TEntity : class
    {
        /// <summary>贪婪加载导航属性，如果查询中已经使用了 a.Parent.Parent 类似表达式，则可以无需此操作</summary>
        /// <typeparam name="TNavigate"></typeparam>
        /// <param name="navigateSelector">选择一个导航属性</param>
        /// <returns></returns>
        ISelect<TEntity> Include<TNavigate>(Expression<Func<TEntity, TNavigate>> navigateSelector)
            where TNavigate : class;

        /// <summary>
        /// 贪婪加载集合的导航属性，其实是分两次查询，ToList 后进行了数据重装<para></para>
        /// 文档：https://github.com/2881099/FreeSql/wiki/%e8%b4%aa%e5%a9%aa%e5%8a%a0%e8%bd%bd#%E5%AF%BC%E8%88%AA%E5%B1%9E%E6%80%A7-onetomanymanytomany
        /// </summary>
        /// <typeparam name="TNavigate"></typeparam>
        /// <param name="navigateSelector">选择一个集合的导航属性，如： .IncludeMany(a =&gt; a.Tags)<para></para>
        /// 可以 .Where 设置临时的关系映射，如： .IncludeMany(a =&gt; a.Tags.Where(tag =&gt; tag.TypeId == a.Id))<para></para>
        /// 可以 .Take(5) 每个子集合只取5条，如： .IncludeMany(a =&gt; a.Tags.Take(5))<para></para>
        /// 可以 .Select 设置只查询部分字段，如： (a =&gt; new TNavigate { Title = a.Title })
        /// </param>
        /// <param name="then">即能 ThenInclude，还可以二次过滤（这个 EFCore 做不到？）</param>
        /// <returns></returns>
        ISelect<TEntity> IncludeMany<TNavigate>(
            Expression<Func<TEntity, IEnumerable<TNavigate>>> navigateSelector,
            Action<ISelect<TNavigate>> then = null)
            where TNavigate : class;

        Task<Dictionary<TKey, TEntity>> ToDictionaryAsync<TKey>(
            Func<TEntity, TKey> keySelector,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<TEntity, TKey> keySelector,
            Func<TEntity, TElement> elementSelector,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<List<TReturn>> ToListAsync<TReturn>(
            Expression<Func<TEntity, TReturn>> select,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<List<TDto>> ToListAsync<TDto>(CancellationToken cancellationToken = default(CancellationToken));
        
        
    }
}