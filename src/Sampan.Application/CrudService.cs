using System.Threading.Tasks;
using Sampan.Infrastructure.Repository;
using Sampan.Public.Dto;
using Sampan.Public.Entity;
using Sampan.Service.Contract;

namespace Sampan.Application
{
    /// <summary>
    /// 审计服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TListDto"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TCreateInput"></typeparam>
    /// <typeparam name="TUpdateInput"></typeparam>
    public abstract class CrudService<TEntity, TDto, TListDto, TGetListInput, TCreateInput, TUpdateInput> :
        ReadSerivce<TEntity, TDto, TListDto, TGetListInput>,
        ICrudService<TDto, TListDto, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity
        where TDto : class, IDto
        where TListDto : class, IDto
        where TGetListInput : GetPageDto
    {
        protected CrudService(IRepository<TEntity> repository) : base(repository)
        {
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual Task CreateAsync(TCreateInput input)
        {
            var entity = Mapper.Map<TEntity>(input);
            return Repository.InsertAsync(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(int id, TUpdateInput input)
        {
            var entity = await Repository.Where(a => a.Id == id).ToOneAsync();
            Mapper.Map(input, entity);
            await Repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(int id)
        {
            return Repository.DeleteAsync(id);
        }
    }
}