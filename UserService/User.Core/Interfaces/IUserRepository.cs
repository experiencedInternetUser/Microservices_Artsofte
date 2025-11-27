using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Core.Entities;

namespace User.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User?> GetAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> ListAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
