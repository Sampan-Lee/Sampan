using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sampan.Service.Contract.Tasks
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAll();
    }
}