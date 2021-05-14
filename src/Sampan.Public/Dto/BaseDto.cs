namespace Sampan.Public.Dto
{
    /// <summary>
    /// 业务实体基类
    /// </summary>
    public class BaseDto : IBaseDto
    {
        /// <summary>
        /// 逻辑主键
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// 排序业务实体
    /// </summary>
    public class SortDto : ISortDto
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}