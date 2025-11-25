using System;
using System.Collections.Generic;

namespace CoreLib.DTOs
{
    public record TaskDto(Guid Id, Guid ProjectId, string Title, string? Description, string Status, string Priority, Guid? ReporterId, DateTime CreatedAt, DateTime UpdatedAt, DateTime? DueDate, int? Estimate, IEnumerable<Guid> AssigneeIds, IEnumerable<Guid> LabelIds);

    public record CreateTaskRequest(Guid ProjectId, string Title, string? Description, Guid? ReporterId, IEnumerable<Guid>? AssigneeIds, IEnumerable<Guid>? LabelIds, string Priority = "medium", DateTime? DueDate = null, int? Estimate = null);

    public record UpdateTaskRequest(string? Title, string? Description, string? Status, string? Priority, DateTime? DueDate, int? Estimate);
}
