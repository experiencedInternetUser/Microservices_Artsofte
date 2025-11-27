using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User.Core.Entities.User> AddAsync(User.Core.Entities.User user);
        Task<User.Core.Entities.User?> GetAsync(Guid id);
        Task<User.Core.Entities.User?> GetByEmailAsync(string email);
        Task<IEnumerable<User.Core.Entities.User>> ListAsync();
        Task UpdateAsync(User.Core.Entities.User user);
        Task DeleteAsync(Guid id);
    }
}
