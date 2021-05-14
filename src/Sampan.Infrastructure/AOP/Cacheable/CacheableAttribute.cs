using System;

namespace Sampan.Infrastructure.AOP.Cacheable
{
    /// <summary>
    /// 标记这个特性的方法，它的执行结果将会存入Redis缓存中
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
        /// </summary>
        public CacheableAttribute()
        {
        }

        public CacheableAttribute(int expiryTime)
        {
            ExpiryTime = expiryTime;
        }

        public string Key { get; set; }

        /// <summary>
        /// 缓存过期时间累加值（单位：分钟）
        /// 当前时间+缓存过期累加值=过期时间
        /// </summary>
        public int? ExpiryTime { get; set; }
    }
}