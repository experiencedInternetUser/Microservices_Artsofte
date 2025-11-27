using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Core.Entities;

namespace User.Core.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile> AddAsync(Profile profile);
        Task<Profile?> GetAsync(Guid id);
        Task<Profile?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Profile>> ListAsync();
        Task UpdateAsync(Profile profile);
        Task DeleteAsync(Guid id);
    }
}
