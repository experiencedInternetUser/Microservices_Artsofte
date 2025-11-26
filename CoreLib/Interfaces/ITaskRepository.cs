using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLib.Entities;

namespace CoreLib.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> AddAsync(TaskEntity task);
        Task<TaskEntity?> GetAsync(Guid id);
        Task<IEnumerable<TaskEntity>> ListAsync(Guid? projectId = null);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid id);
    }
}
