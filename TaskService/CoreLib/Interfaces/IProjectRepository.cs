using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLib.Entities;

namespace CoreLib.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<Project?> GetAsync(Guid id);
        Task<IEnumerable<Project>> ListAsync();
        Task UpdateAsync(Project project);
        Task DeleteAsync(Guid id);
    }
}
