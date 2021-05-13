using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sampan.Infrastructure.DistributedCache
{
    public interface IDistributedCache
    {
        /// <summary>
        /// 存在
        /// </summary>
        /// <param name="entityHash"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(string entityHash);

        /// <summary>
        /// 获取前缀为prefix的key值
        /// </summary>
        /// <param name="entityHash"></param>
        /// <returns></returns>
        List<string> GetKeysAsync(string prefix);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime"></param>
        /// <returns></returns>
        Task SetAsync(string key, object value, TimeSpan? expiryTime = null);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(List<string> keys);

        /// <summary>
        /// 清除
        /// </summary>
        /// <returns></returns>
        Task ClearAsync();

        Task RemoveByKeyPrefixAsync(string userPermission);
    }
}