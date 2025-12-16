using TaskService.Dal.Repositories;
using TaskService.Core.Models;

namespace TaskService.Logic.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async System.Threading.Tasks.Task<Project> GetByIdAsync(Guid id)
    {
        return await _projectRepository.GetByIdAsync(id);
    }

    public async System.Threading.Tasks.Task CreateAsync(Project project)
    {
        await _projectRepository.AddAsync(project);
    }

    public async System.Threading.Tasks.Task DeleteAsync(Guid id)
    {
        await _projectRepository.DeleteAsync(id);
    }
}
