using System.Threading.Tasks;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract
{
    /// <summary>
    /// 基础审计服务接口
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TListDto"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TCreateInput"></typeparam>
    /// <typeparam name="TUpdateInput"></typeparam>
    public interface ICrudService<TDto, TListDto, in TGetListInput, in TCreateInput, in TUpdateInput> :
        IReadService<TDto, TListDto, TGetListInput>
        where TDto : class, IDto
        where TListDto : class, IDto
        where TGetListInput : GetPageDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(TCreateInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, TUpdateInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}