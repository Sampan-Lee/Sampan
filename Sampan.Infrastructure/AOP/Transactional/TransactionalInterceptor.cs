using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using FreeSql;
using Microsoft.Extensions.Logging;

namespace Sampan.Infrastructure.AOP.Transactional
{
    public class TransactionalInterceptor : IInterceptor
    {
        private readonly TransactionalAsyncInterceptor asyncInterceptor;

        public TransactionalInterceptor(TransactionalAsyncInterceptor interceptor)
        {
            asyncInterceptor = interceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            asyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }

    public class TransactionalAsyncInterceptor : IAsyncInterceptor
    {
        private readonly UnitOfWorkManager _unitOfWorkManager;
        private readonly ILogger<TransactionalAsyncInterceptor> _logger;
        private IUnitOfWork _unitOfWork;

        public TransactionalAsyncInterceptor(UnitOfWorkManager unitOfWorkManager,
            ILogger<TransactionalAsyncInterceptor> logger)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _logger = logger;
        }

        private bool TryBegin(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var attribute = method.GetCustomAttributes(typeof(TransactionalAttribute), false).FirstOrDefault();
            if (attribute is TransactionalAttribute transaction)
            {
                _unitOfWork = _unitOfWorkManager.Begin(transaction.Propagation, transaction.IsolationLevel);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 拦截返回结果为Task的方法
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            if (TryBegin(invocation))
            {
                invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
            }
            else
            {
                invocation.Proceed();
            }
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            if (TryBegin(invocation))
            {
                int? hashCode = _unitOfWork.GetHashCode();
                try
                {
                    invocation.Proceed();
                    _logger.LogInformation($"----- 拦截同步执行的方法-事务 {hashCode} 提交前----- ");
                    _unitOfWork.Commit();
                    _logger.LogInformation($"----- 拦截同步执行的方法-事务 {hashCode} 提交成功----- ");
                }
                catch
                {
                    _logger.LogError($"----- 拦截同步执行的方法-事务 {hashCode} 提交失败----- ");
                    _unitOfWork.Rollback();
                    throw;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
            else
            {
                invocation.Proceed();
            }
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            string methodName =
                $"{invocation.MethodInvocationTarget.DeclaringType?.FullName}.{invocation.Method.Name}()";
            int? hashCode = _unitOfWork.GetHashCode();

            using (_logger.BeginScope("_unitOfWork:{hashCode}", hashCode))
            {
                _logger.LogInformation($"----- async Task 开始事务{hashCode} {methodName}----- ");

                invocation.Proceed();
                try
                {
                    await (Task) invocation.ReturnValue;
                    _unitOfWork.Commit();
                    _logger.LogInformation($"----- async Task 事务 {hashCode} Commit----- ");
                }
                catch (System.Exception)
                {
                    _unitOfWork.Rollback();
                    _logger.LogError($"----- async Task 事务 {hashCode} Rollback----- ");
                    throw;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
        }

        /// <summary>
        /// 拦截返回结果为Task<TResult>的方法
        /// </summary>
        /// <param name="invocation"></param>
        /// <typeparam name="TResult"></typeparam>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            TResult result;
            if (TryBegin(invocation))
            {
                string methodName =
                    $"{invocation.MethodInvocationTarget.DeclaringType?.FullName}.{invocation.Method.Name}()";
                int hashCode = _unitOfWork.GetHashCode();
                _logger.LogInformation($"----- async Task<TResult> 开始事务{hashCode} {methodName}----- ");

                try
                {
                    invocation.Proceed();
                    result = await (Task<TResult>) invocation.ReturnValue;
                    _unitOfWork.Commit();
                    _logger.LogInformation($"----- async Task<TResult> Commit事务{hashCode}----- ");
                }
                catch (System.Exception)
                {
                    _unitOfWork.Rollback();
                    _logger.LogError($"----- async Task<TResult> Rollback事务{hashCode}----- ");
                    throw;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
            else
            {
                invocation.Proceed();
                result = await (Task<TResult>) invocation.ReturnValue;
            }

            return result;
        }
    }
}