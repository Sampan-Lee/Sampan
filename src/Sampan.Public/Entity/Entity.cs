using System;
using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace Sampan.Public.Entity
{
    public class Entity : IEntity
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }
    }

    public class TenantEntity : Entity, ITenantEntity
    {
        public int? TenantId { get; set; }
    }

    public class EnableEntity : Entity, IEnableEntity
    {
        public bool IsEnable { get; set; }
    }

    public class EnableBaseEntity : BaseEntity, IEnableEntity
    {
        public bool IsEnable { get; set; }
    }

    public class SortEntity : ISortEntity
    {
        public int Sort { get; set; }
    }

    public class SortBaseEntity : BaseEntity, ISortEntity
    {
        public int Sort { get; set; }
    }

    public class SortEnableBaseEntity : BaseEntity, ISortEntity, IEnableEntity
    {
        public int Sort { get; set; }
        public bool IsEnable { get; set; }
    }

    public class CreateEntity : ICreateEntity
    {
        public int CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class UpdateEntity : IUpdateEntity
    {
        public int UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class SoftDeleteEntity : ISoftDeleteEntity
    {
        public bool IsDelete { get; set; }
        public int? DeleteUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
    }

    public class BaseEntity : Entity, ITenantEntity, ICreateEntity, IUpdateEntity, ISoftDeleteEntity
    {
        public int? TenantId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDelete { get; set; }
        public int? DeleteUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
    }

    public class PageEntity<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<TEntity> Data { get; set; }
    }
}