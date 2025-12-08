using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Core.DTOs;
using User.Core.Entities;
using User.Core.Interfaces;

namespace User.Application.Services
{
    public interface IProfileService
    {
        Task<ProfileDto> CreateAsync(CreateProfileRequest req);
        Task<ProfileDto?> GetAsync(Guid id);
        Task<ProfileDto?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<ProfileDto>> ListAsync();
        Task UpdateAsync(Guid id, UpdateProfileRequest req);
        Task DeleteAsync(Guid id);
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repo;
        public ProfileService(IProfileRepository repo) => _repo = repo;

        public async Task<ProfileDto> CreateAsync(CreateProfileRequest req)
        {
            var p = new Profile { UserId = req.UserId, FullName = req.FullName, Bio = req.Bio, AvatarUrl = req.AvatarUrl };
            var created = await _repo.AddAsync(p);
            return new ProfileDto(created.Id, created.UserId, created.FullName, created.Bio, created.AvatarUrl);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<ProfileDto?> GetAsync(Guid id)
        {
            var p = await _repo.GetAsync(id);
            return p == null ? null : new ProfileDto(p.Id, p.UserId, p.FullName, p.Bio, p.AvatarUrl);
        }

        public async Task<ProfileDto?> GetByUserIdAsync(Guid userId)
        {
            var p = await _repo.GetByUserIdAsync(userId);
            return p == null ? null : new ProfileDto(p.Id, p.UserId, p.FullName, p.Bio, p.AvatarUrl);
        }

        public async Task<IEnumerable<ProfileDto>> ListAsync()
        {
            var list = await _repo.ListAsync();
            return list.Select(p => new ProfileDto(p.Id, p.UserId, p.FullName, p.Bio, p.AvatarUrl));
        }

        public async Task UpdateAsync(Guid id, UpdateProfileRequest req)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) throw new KeyNotFoundException("Profile not found");
            if (req.FullName != null) existing.FullName = req.FullName;
            if (req.Bio != null) existing.Bio = req.Bio;
            if (req.AvatarUrl != null) existing.AvatarUrl = req.AvatarUrl;
            await _repo.UpdateAsync(existing);
        }
    }
}
