using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Core.Interfaces;
using User.Infrastructure.Data;
using ProfileEntity = User.Core.Entities.Profile;

namespace User.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UserDbContext _db;
        public ProfileRepository(UserDbContext db) => _db = db;

        public async Task<ProfileEntity> AddAsync(ProfileEntity profile)
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

        public async Task<ProfileEntity?> GetAsync(Guid id) => await _db.Profiles.FindAsync(id);

        public async Task<ProfileEntity?> GetByUserIdAsync(Guid userId) => await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task<IEnumerable<ProfileEntity>> ListAsync() => await _db.Profiles.ToListAsync();

        public async Task UpdateAsync(ProfileEntity profile)
        {
            _db.Profiles.Update(profile);
            await _db.SaveChangesAsync();
        }
    }
}
