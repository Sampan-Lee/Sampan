using System;

namespace Sampan.Public.Entity
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 逻辑主键
        /// </summary>
        int Id { get; set; }
    }

    /// <summary>
    /// 多租户实体接口
    /// </summary>
    public interface ITenantEntity
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        int? TenantId { get; set; }
    }

    /// <summary>
    /// 排序实体接口
    /// </summary>
    public interface ISortEntity
    {
        int Sort { get; set; }
    }

    /// <summary>
    /// 启用/禁用实体接口
    /// </summary>
    public interface IEnableEntity
    {
        bool IsEnable { get; set; }
    }


    /// <summary>
    /// 创建实体接口
    /// </summary>
    public interface ICreateEntity
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        int CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 修改实体接口
    /// </summary>
    public interface IUpdateEntity
    {
        /// <summary>
        /// 修改人ID
        /// </summary>
        int UpdateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// 软删除实体接口
    /// </summary>
    public interface ISoftDeleteEntity
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDelete { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        int? DeleteUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeleteTime { get; set; }
    }
}