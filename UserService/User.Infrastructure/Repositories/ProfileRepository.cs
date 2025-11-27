using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Core.Entities;
using User.Core.Interfaces;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UserDbContext _db;
        public ProfileRepository(UserDbContext db) => _db = db;

        public async Task<Profile> AddAsync(Profile profile)
        {
            _db.Profiles.Add(profile);
            await _db.SaveChangesAsync();
            return profile;
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _db.Profiles.FindAsync(id);
            if (e != null) { _db.Profiles.Remove(e); await _db.SaveChangesAsync(); }
        }

        public async Task<Profile?> GetAsync(Guid id) => await _db.Profiles.FindAsync(id);

        public async Task<Profile?> GetByUserIdAsync(Guid userId) => await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task<IEnumerable<Profile>> ListAsync() => await _db.Profiles.ToListAsync();

        public async Task UpdateAsync(Profile profile)
        {
            _db.Profiles.Update(profile);
            await _db.SaveChangesAsync();
        }
    }
}
