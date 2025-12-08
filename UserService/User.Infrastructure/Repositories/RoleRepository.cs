using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Core.Interfaces;
using User.Infrastructure.Data;
using RoleEntity = User.Core.Entities.Role;

namespace User.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserDbContext _db;
        public RoleRepository(UserDbContext db) => _db = db;

        public async Task<RoleEntity> AddAsync(RoleEntity role)
        {
            _db.Roles.Add(role);
            await _db.SaveChangesAsync();
            return role;
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Roles.FindAsync(id);
            if (e != null) { _db.Roles.Remove(e); await _db.SaveChangesAsync(); }
        }

        public async Task<RoleEntity?> GetAsync(Guid id) => await _db.Roles.FindAsync(id);

        public async Task<IEnumerable<RoleEntity>> ListAsync() => await _db.Roles.ToListAsync();

        public async Task UpdateAsync(RoleEntity role)
        {
            _db.Roles.Update(role);
            await _db.SaveChangesAsync();
        }
    }
}
