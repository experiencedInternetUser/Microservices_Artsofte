using System;

namespace TaskService.Task.Contracts
{
    public record TaskCreated
    {
        public Guid TaskId { get; init; }
    }
}
