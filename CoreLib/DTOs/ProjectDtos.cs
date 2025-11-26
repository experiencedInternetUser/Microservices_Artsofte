using System;

namespace CoreLib.DTOs
{
    public record ProjectDto(Guid Id, string Name, string? Description, Guid OwnerId, DateTime CreatedAt);
    public record CreateProjectRequest(string Name, string? Description, Guid OwnerId);
    public record UpdateProjectRequest(string? Name, string? Description);
}
