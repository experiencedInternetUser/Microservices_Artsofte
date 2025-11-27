using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Core.Entities;

namespace User.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> AddAsync(Role role);
        Task<Role?> GetAsync(Guid id);
        Task<IEnumerable<Role>> ListAsync();
        Task UpdateAsync(Role role);
        Task DeleteAsync(Guid id);
    }
}
