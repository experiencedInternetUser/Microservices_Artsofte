using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<User.Core.Entities.Role> AddAsync(User.Core.Entities.Role role);
        Task<User.Core.Entities.Role?> GetAsync(Guid id);
        Task<IEnumerable<User.Core.Entities.Role>> ListAsync();
        Task UpdateAsync(User.Core.Entities.Role role);
        Task DeleteAsync(Guid id);
    }
}
