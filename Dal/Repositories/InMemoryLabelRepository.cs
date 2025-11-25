using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.Entities;
using CoreLib.Interfaces;

namespace Dal.Repositories
{
    public class InMemoryLabelRepository : ILabelRepository
    {
        private readonly ConcurrentDictionary<Guid, Label> _store = new();

        public Task<Label> AddAsync(Label label)
        {
            _store[label.Id] = label;
            return Task.FromResult(label);
        }

        public Task DeleteAsync(Guid id)
        {
            _store.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<Label?> GetAsync(Guid id)
        {
            _store.TryGetValue(id, out var l);
            return Task.FromResult(l);
        }

        public Task<IEnumerable<Label>> ListAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task UpdateAsync(Label label)
        {
            _store[label.Id] = label;
            return Task.CompletedTask;
        }
    }
}
