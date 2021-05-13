using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using Sampan.Common.Extension;
using Sampan.Common.Log;
using Sampan.Infrastructure.DistributedCache;

namespace Sampan.Infrastructure.AOP.Cacheable
{
    public class CacheableInterceptor : IInterceptor
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly IDistributedCache _cache;

        public CacheableInterceptor(IDistributedCache cache)
        {
            _cache = cache;
        }

        //Intercept方法是拦截的关键所在，也是IInterceptor接口中的唯一定义
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //对当前方法的特性验证
            var cacheableAttribute =
                method.GetCustomAttributes(true)
                    .FirstOrDefault(x => x.GetType() == typeof(CacheableAttribute)) as CacheableAttribute;
            //如果需要验证
            if (cacheableAttribute != null)
            {
                if (method.Name.Contains("Get"))
                {
                    GetCacheIntercept(invocation, cacheableAttribute);
                }
                else
                {
                    var keyPrefix = invocation.TargetType.Name;
                    var keys = _cache.GetKeysAsync(keyPrefix);
                    if (!keys.IsNullOrEmpty())
                    {
                        _cache.RemoveAsync(keys);
                    }

                    invocation.Proceed(); //直接执行被拦截方法
                }
            }
            else
            {
                invocation.Proceed(); //直接执行被拦截方法
            }
        }

        /// <summary>
        /// 拦截操作
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="cacheAttribute"></param>
        private void GetCacheIntercept(IInvocation invocation, CacheableAttribute attribute)
        {
            //获取自定义缓存键
            var cacheKey = string.IsNullOrWhiteSpace(attribute?.Key)
                ? GenerateCacheKey(invocation)
                : attribute.Key;

            var cacheValue = _cache.ExistAsync(cacheKey).Result;
            //判断redis中是否存在值
            try
            {
                if (cacheValue)
                {
                    //将当前获取到的缓存值，赋值给当前执行方法
                    var type = invocation.Method.ReturnType;
                    var resultTypes = type.GenericTypeArguments;
                    if (type.FullName == "System.Void")
                    {
                        return;
                    }

                    object response;
                    if (typeof(Task).IsAssignableFrom(type))
                    {
                        //返回Task<T>
                        if (resultTypes.Any())
                        {
                            var resultType = resultTypes.FirstOrDefault();
                            var data = _cache.GetAsync(cacheKey).Result;
                            dynamic temp = JsonConvert.DeserializeObject(data, resultType!);
                            response = Task.FromResult(temp);
                        }
                        else
                        {
                            //Task 无返回方法 指定时间内不允许重新运行
                            response = Task.Yield();
                        }
                    }
                    else
                    {
                        var data = _cache.GetAsync(cacheKey).Result;
                        dynamic temp = JsonConvert.DeserializeObject(data, type); //不存task返回类型就直接用返回类型反序列化
                        response = System.Convert.ChangeType(temp, type);
                    }

                    invocation.ReturnValue = response;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "【缓存切面：获取】");
            }

            //去执行当前的方法 不能影响这块的正常运行
            invocation.Proceed();

            //存入缓存
            try
            {
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    object response;

                    //Type type = invocation.ReturnValue?.GetType();
                    var type = invocation.Method.ReturnType;
                    if (type != null && typeof(Task).IsAssignableFrom(type))
                    {
                        var resultProperty = type.GetProperty("Result");
                        response = resultProperty.GetValue(invocation.ReturnValue);
                    }
                    else
                    {
                        response = invocation.ReturnValue;
                    }

                    if (response == null) response = string.Empty;
                    if (attribute != null)
                        _cache.SetAsync(cacheKey, response,
                            attribute.ExpiryTime.HasValue ? TimeSpan.FromMinutes(attribute.ExpiryTime.Value) : null);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "【缓存切面：设置】");
            }
        }

        //自定义缓存键
        private string GenerateCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).ToList(); //获取参数列表，最多三个

            string key = $"{typeName}_{methodName}";
            foreach (var param in methodArguments)
            {
                key += $"_{param}";
            }

            return key;
        }

        //object 转 string
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is string)
                return arg.ToString();

            if (arg is DateTime)
                return ((DateTime) arg).ToString("yyyyMMddHHmmss");

            return "";
        }
    }
}