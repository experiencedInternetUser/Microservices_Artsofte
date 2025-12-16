using TaskService.Task.Core.Entities;
using TaskService.Task.Core.Interfaces;

namespace TaskService.Logic.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async System.Threading.Tasks.Task<TaskService.Core.Models.Task> GetByIdAsync(Guid id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }

    public async System.Threading.Tasks.Task CreateAsync(TaskService.Core.Models.Task task)
    {
        await _taskRepository.AddAsync(task);
    }

    public async System.Threading.Tasks.Task UpdateAsync(TaskService.Core.Models.Task task)
    {
        await _taskRepository.UpdateAsync(task);
    }

    public async System.Threading.Tasks.Task DeleteAsync(Guid id)
    {
        await _taskRepository.DeleteAsync(id);
    }
}
