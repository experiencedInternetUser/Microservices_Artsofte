using System;
using System.Collections.Generic;

namespace CoreLib.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Status { get; set; } = "backlog";
        public string Priority { get; set; } = "medium";
        public Guid? ReporterId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public int? Estimate { get; set; }

        public List<TaskAssignment> Assignments { get; set; } = new();
        public List<TaskLabel> TaskLabels { get; set; } = new();
    }
}
