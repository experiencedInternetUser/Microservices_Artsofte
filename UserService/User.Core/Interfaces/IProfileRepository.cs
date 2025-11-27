using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User.Core.Interfaces
{
    public interface IProfileRepository
    {
        Task<User.Core.Entities.Profile> AddAsync(User.Core.Entities.Profile profile);
        Task<User.Core.Entities.Profile?> GetAsync(Guid id);
        Task<User.Core.Entities.Profile?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<User.Core.Entities.Profile>> ListAsync();
        Task UpdateAsync(User.Core.Entities.Profile profile);
        Task DeleteAsync(Guid id);
    }
}
