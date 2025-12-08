using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Core.Interfaces;
using User.Infrastructure.Data;
using UserEntity = User.Core.Entities.User;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _db;
        public UserRepository(UserDbContext db) => _db = db;

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Users.FindAsync(id);
            if (e != null) { _db.Users.Remove(e); await _db.SaveChangesAsync(); }
        }

        public async Task<UserEntity?> GetAsync(Guid id) => await _db.Users.FindAsync(id);

        public async Task<UserEntity?> GetByEmailAsync(string email) => await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<IEnumerable<UserEntity>> ListAsync() => await _db.Users.ToListAsync();

        public async Task UpdateAsync(UserEntity user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
