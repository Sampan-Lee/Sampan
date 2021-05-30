using System;

namespace Sampan.Public.Dto
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IDto
    {
        /// <summary>
        /// 逻辑主键
        /// </summary>
        int Id { get; set; }
    }

    /// <summary>
    /// 多租户实体接口
    /// </summary>
    public interface ITenantDto
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        int? TenantId { get; set; }
    }

    /// <summary>
    /// 排序实体接口
    /// </summary>
    public interface ISortDto
    {
        int Sort { get; set; }
    }

    /// <summary>
    /// 启用/禁用实体接口
    /// </summary>
    public interface IEnableDto
    {
        bool IsEnable { get; set; }
    }


    /// <summary>
    /// 创建实体接口
    /// </summary>
    public interface ICreateDto
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        int CreateUserId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 修改实体接口
    /// </summary>
    public interface IUpdateDto
    {
        /// <summary>
        /// 修改人ID
        /// </summary>
        int UpdateUserId { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        string UpdateUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}