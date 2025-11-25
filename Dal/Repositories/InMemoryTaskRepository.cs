using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.Entities;
using CoreLib.Interfaces;

namespace Dal.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly ConcurrentDictionary<Guid, TaskEntity> _store = new();

        public Task<TaskEntity> AddAsync(TaskEntity task)
        {
            _store[task.Id] = task;
            return Task.FromResult(task);
        }

        public Task DeleteAsync(Guid id)
        {
            _store.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<TaskEntity?> GetAsync(Guid id)
        {
            _store.TryGetValue(id, out var t);
            return Task.FromResult(t);
        }

        public Task<IEnumerable<TaskEntity>> ListAsync(Guid? projectId = null)
        {
            var items = _store.Values.AsEnumerable();
            if (projectId.HasValue) items = items.Where(x => x.ProjectId == projectId.Value);
            return Task.FromResult(items);
        }

        public Task UpdateAsync(TaskEntity task)
        {
            task.UpdatedAt = DateTime.UtcNow;
            _store[task.Id] = task;
            return Task.CompletedTask;
        }
    }
}
