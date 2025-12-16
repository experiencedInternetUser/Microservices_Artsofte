using System;

namespace TaskService.Task.Contracts
{
    public record TaskCreationFailed
    {
        public Guid TaskId { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
}
