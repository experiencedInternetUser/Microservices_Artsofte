using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.Entities;
using CoreLib.Interfaces;

namespace Dal.Repositories
{
    public class InMemoryProjectRepository : IProjectRepository
    {
        private readonly ConcurrentDictionary<Guid, Project> _store = new();

        public Task<Project> AddAsync(Project project)
        {
            _store[project.Id] = project;
            return Task.FromResult(project);
        }

        public Task DeleteAsync(Guid id)
        {
            _store.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<Project?> GetAsync(Guid id)
        {
            _store.TryGetValue(id, out var proj);
            return Task.FromResult(proj);
        }

        public Task<IEnumerable<Project>> ListAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task UpdateAsync(Project project)
        {
            _store[project.Id] = project;
            return Task.CompletedTask;
        }
    }
}
