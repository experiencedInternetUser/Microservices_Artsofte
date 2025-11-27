using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Core.DTOs;
using User.Core.Entities;
using User.Core.Interfaces;

namespace User.Application.Services
{
    public interface IRoleService
    {
        Task<RoleDto> CreateAsync(CreateRoleRequest req);
        Task<RoleDto?> GetAsync(Guid id);
        Task<IEnumerable<RoleDto>> ListAsync();
        Task UpdateAsync(Guid id, UpdateRoleRequest req);
        Task DeleteAsync(Guid id);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        public RoleService(IRoleRepository repo) => _repo = repo;

        public async Task<RoleDto> CreateAsync(CreateRoleRequest req)
        {
            var r = new Role { Name = req.Name, Description = req.Description };
            var created = await _repo.AddAsync(r);
            return new RoleDto(created.Id, created.Name, created.Description);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<RoleDto?> GetAsync(Guid id)
        {
            var r = await _repo.GetAsync(id);
            return r == null ? null : new RoleDto(r.Id, r.Name, r.Description);
        }

        public async Task<IEnumerable<RoleDto>> ListAsync()
        {
            var list = await _repo.ListAsync();
            return list.Select(r => new RoleDto(r.Id, r.Name, r.Description));
        }

        public async Task UpdateAsync(Guid id, UpdateRoleRequest req)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) throw new KeyNotFoundException("Role not found");
            if (req.Name != null) existing.Name = req.Name;
            if (req.Description != null) existing.Description = req.Description;
            await _repo.UpdateAsync(existing);
        }
    }
}
