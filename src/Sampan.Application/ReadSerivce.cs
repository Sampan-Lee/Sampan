using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using FreeSql;
using Sampan.Common.Extension;
using Sampan.Infrastructure.Repository;
using Sampan.Public.Dto;
using Sampan.Public.Entity;
using Sampan.Service.Contract;

namespace Sampan.Application
{
    /// <summary>
    /// 读取服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TListDto"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public abstract class ReadSerivce<TEntity, TDto, TListDto, TGetListInput> : Service,
        IReadService<TDto, TListDto, TGetListInput>
        where TEntity : class, IEntity
        where TDto : class, IDto
        where TListDto : class, IDto
        where TGetListInput : GetPageDto
    {
        protected IRepository<TEntity> Repository { get; }

        protected ReadSerivce(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TDto> GetAsync(int id)
        {
            var entity = await Repository.GetAsync(id);
            return Mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<PageResultDto<TListDto>> GetAsync(TGetListInput input)
        {
            var query = CreateFilteredQuery(input);

            query = ApplySorting(query, input);

            query = DefaultSorting(query);

            var pageEntity = await query.Count(out var total).Page(input.Index, input.Size).ToListAsync();

            return await GetPageDto(total, pageEntity);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual ISelect<TEntity> CreateFilteredQuery(TGetListInput input)
        {
            return Repository.Select;
        }

        /// <summary>
        /// 请求排序，以外链接表字段为排序条件是在具体实现中重写
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual ISelect<TEntity> ApplySorting(ISelect<TEntity> query, TGetListInput input)
        {
            query.OrderByPropertyNameIf(!input.Sort.IsNullOrWhiteSpace(), input.Sort, input.Asc);

            return query;
        }

        /// <summary>
        /// 默认排序
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected ISelect<TEntity> DefaultSorting(ISelect<TEntity> query)
        {
            query.OrderByIf(typeof(ISortEntity).IsAssignableFrom(typeof(TEntity)), a => (a as ISortEntity).Sort);

            query.OrderByIf(typeof(ICreateEntity).IsAssignableFrom(typeof(TEntity)),
                a => (a as ICreateEntity).CreateTime, true);

            return query;
        }

        /// <summary>
        /// 分页结果
        /// </summary>
        /// <param name="total"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected async Task<PageResultDto<TListDto>> GetPageDto(long total, List<TEntity> data)
        {
            var result = new PageResultDto<TListDto>();

            if (data.Any())
            {
                result.Data = await MapToListDtosAsync(data);
                result.Total = total;
            }

            return result;
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetOutputDto"/>.
        /// It internally calls the <see cref="MapToGetOutputDto"/> by default.
        /// It can be overriden for custom mapping.
        /// Overriding this has higher priority than overriding the <see cref="MapToGetOutputDto"/>
        /// </summary>
        protected virtual Task<TDto> MapToDtoAsync(TEntity entity)
        {
            return Task.FromResult(MapToDto(entity));
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetOutputDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual TDto MapToDto(TEntity entity)
        {
            return Mapper.Map<TEntity, TDto>(entity);
        }

        /// <summary>
        /// Maps a list of <see cref="TEntity"/> to <see cref="TGetListOutputDto"/> objects.
        /// It uses <see cref="MapToGetListOutputDtoAsync"/> method for each item in the list.
        /// </summary>
        protected virtual async Task<List<TListDto>> MapToListDtosAsync(List<TEntity> entities)
        {
            var dtos = new List<TListDto>();

            foreach (var entity in entities)
            {
                dtos.Add(await MapToListDtoAsync(entity));
            }

            return dtos;
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetListOutputDto"/>.
        /// It internally calls the <see cref="MapToGetListOutputDto"/> by default.
        /// It can be overriden for custom mapping.
        /// Overriding this has higher priority than overriding the <see cref="MapToGetListOutputDto"/>
        /// </summary>
        protected virtual Task<TListDto> MapToListDtoAsync(TEntity entity)
        {
            return Task.FromResult(MapToListDto(entity));
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetListOutputDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual TListDto MapToListDto(TEntity entity)
        {
            return Mapper.Map<TEntity, TListDto>(entity);
        }
    }
}