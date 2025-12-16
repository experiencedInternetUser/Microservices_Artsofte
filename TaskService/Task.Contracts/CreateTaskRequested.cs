using System;

namespace TaskService.Task.Contracts
{
    public record CreateTaskRequested
    {
        public Guid TaskId { get; init; }
        public string Title { get; init; } = string.Empty;
        public Guid UserId { get; init; }
    }
}
