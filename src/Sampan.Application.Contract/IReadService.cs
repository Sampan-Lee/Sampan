using System.Threading.Tasks;
using Sampan.Public.Dto;

namespace Sampan.Service.Contract
{
    /// <summary>
    /// 审计读取服务接口
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TListDto"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public interface IReadService<TDto, TListDto, in TGetListInput>
        where TDto : class, IDto
        where TListDto : class, IDto
        where TGetListInput : GetPageDto
    {
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDto> GetAsync(int id);

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<TListDto>> GetAsync(TGetListInput input);
    }
}