namespace Sampan.Public.Dto
{
    /// <summary>
    /// 业务实体基类接口
    /// </summary>
    public interface IBaseDto
    {
        /// <summary>
        /// 逻辑主键
        /// </summary>
        int Id { get; set; }
    }

    /// <summary>
    /// 排序业务实体接口
    /// </summary>
    public interface ISortDto
    {
        /// <summary>
        /// 排序
        /// </summary>
        int Sort { get; set; }
    }
}