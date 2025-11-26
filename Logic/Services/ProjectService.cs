using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.DTOs;
using CoreLib.Entities;
using CoreLib.Interfaces;

namespace Logic.Services
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateAsync(CreateProjectRequest req);
        Task<ProjectDto?> GetAsync(Guid id);
        Task<IEnumerable<ProjectDto>> ListAsync();
        Task UpdateAsync(Guid id, UpdateProjectRequest req);
        Task DeleteAsync(Guid id);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;
        public ProjectService(IProjectRepository repo) => _repo = repo;

        public async Task<ProjectDto> CreateAsync(CreateProjectRequest req)
        {
            var p = new Project { Name = req.Name, Description = req.Description, OwnerId = req.OwnerId };
            var created = await _repo.AddAsync(p);
            return new ProjectDto(created.Id, created.Name, created.Description, created.OwnerId, created.CreatedAt);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<ProjectDto?> GetAsync(Guid id)
        {
            var p = await _repo.GetAsync(id);
            if (p == null) return null;
            return new ProjectDto(p.Id, p.Name, p.Description, p.OwnerId, p.CreatedAt);
        }

        public async Task<IEnumerable<ProjectDto>> ListAsync()
        {
            var list = await _repo.ListAsync();
            return list.Select(p => new ProjectDto(p.Id, p.Name, p.Description, p.OwnerId, p.CreatedAt));
        }

        public async Task UpdateAsync(Guid id, UpdateProjectRequest req)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) throw new KeyNotFoundException("Project not found");
            if (req.Name != null) existing.Name = req.Name;
            if (req.Description != null) existing.Description = req.Description;
            await _repo.UpdateAsync(existing);
        }
    }
}
