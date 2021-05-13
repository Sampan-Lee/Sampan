using System.Threading.Tasks;

namespace Sampan.Infrastructure.TaskScheduler
{
    public interface ISchedulerCenter
    {
        /// <summary>
        /// 添加一个计划任务
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> AddAsync(object item);
    }
}