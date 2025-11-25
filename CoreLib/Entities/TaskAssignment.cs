using System;

namespace CoreLib.Entities
{
    public class TaskAssignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = "assignee";
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
