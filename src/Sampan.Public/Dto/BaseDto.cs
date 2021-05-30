using System;
using System.ComponentModel.DataAnnotations;

namespace Sampan.Public.Dto
{
    public class Dto : IDto
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// 多租户实体接口
    /// </summary>
    public class TenantDto : Dto, ITenantDto
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public int? TenantId { get; set; }
    }

    public class SortDto : Dto, ISortDto
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        [Required(ErrorMessage = "排序字段不能为空")]
        public int Sort { get; set; }
    }

    public class EnableDto : Dto, IEnableDto
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }

    public class CreateDto : Dto, ICreateDto
    {
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class UpdateDto : Dto, IUpdateDto
    {
        public int UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class BaseDto : Dto, ITenantDto, ICreateDto, IUpdateDto
    {
        public int? TenantId { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class SortBaseDto : BaseDto, ISortDto
    {
        public int Sort { get; set; }
    }

    public class EnableBaseDto : BaseDto, IEnableDto
    {
        public bool IsEnable { get; set; }
    }


    public class SortEnableBaseDto : BaseDto, ISortDto, IEnableDto
    {
        public int Sort { get; set; }
        public bool IsEnable { get; set; }
    }
}