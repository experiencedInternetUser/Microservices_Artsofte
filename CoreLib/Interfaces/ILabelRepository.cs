using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLib.Entities;

namespace CoreLib.Interfaces
{
    public interface ILabelRepository
    {
        Task<Label> AddAsync(Label label);
        Task<Label?> GetAsync(Guid id);
        Task<IEnumerable<Label>> ListAsync();
        Task UpdateAsync(Label label);
        Task DeleteAsync(Guid id);
    }
}
