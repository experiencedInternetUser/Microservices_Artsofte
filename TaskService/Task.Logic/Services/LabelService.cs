using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.DTOs;
using CoreLib.Entities;
using CoreLib.Interfaces;

namespace Logic.Services
{
    public interface ILabelService
    {
        Task<LabelDto> CreateAsync(CreateLabelRequest req);
        Task<LabelDto?> GetAsync(Guid id);
        Task<IEnumerable<LabelDto>> ListAsync();
        Task UpdateAsync(Guid id, UpdateLabelRequest req);
        Task DeleteAsync(Guid id);
    }

    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _repo;
        public LabelService(ILabelRepository repo) => _repo = repo;

        public async Task<LabelDto> CreateAsync(CreateLabelRequest req)
        {
            var l = new Label { Name = req.Name, Color = req.Color };
            var created = await _repo.AddAsync(l);
            return new LabelDto(created.Id, created.Name, created.Color);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<LabelDto?> GetAsync(Guid id)
        {
            var l = await _repo.GetAsync(id);
            if (l == null) return null;
            return new LabelDto(l.Id, l.Name, l.Color);
        }

        public async Task<IEnumerable<LabelDto>> ListAsync()
        {
            var list = await _repo.ListAsync();
            return list.Select(l => new LabelDto(l.Id, l.Name, l.Color));
        }

        public async Task UpdateAsync(Guid id, UpdateLabelRequest req)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null) throw new KeyNotFoundException("Label not found");
            if (req.Name != null) existing.Name = req.Name;
            if (req.Color != null) existing.Color = req.Color;
            await _repo.UpdateAsync(existing);
        }
    }
}
