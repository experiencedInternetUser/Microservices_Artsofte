using TaskService.Task.Core.Entities;
using TaskService.Task.Core.Interfaces;

namespace TaskService.Logic.Services;

public class LabelService
{
    private readonly ILabelRepository _labelRepository;

    public LabelService(ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository;
    }

    public async System.Threading.Tasks.Task<Label> GetByIdAsync(Guid id)
    {
        return await _labelRepository.GetByIdAsync(id);
    }

    public async System.Threading.Tasks.Task CreateAsync(Label label)
    {
        await _labelRepository.AddAsync(label);
    }

    public async System.Threading.Tasks.Task DeleteAsync(Guid id)
    {
        await _labelRepository.DeleteAsync(id);
    }
}
