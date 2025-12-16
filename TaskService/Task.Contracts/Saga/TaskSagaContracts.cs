namespace Task.Contracts.Saga;

public record CreateTaskRequested(
    Guid CorrelationId,
    Guid ProjectId,
    string Title
);

public record TaskCreated(
    Guid CorrelationId,
    Guid TaskId
);

public record UserValidationFailed(
    Guid CorrelationId,
    string Reason
);

public record TaskCreationCompleted(
    Guid CorrelationId
);

public record StartTaskOrchestration(
    Guid CorrelationId,
    Guid ProjectId,
    string Title
);

public record CreateTaskCommand(
    Guid CorrelationId,
    Guid ProjectId,
    string Title
);

public record TaskCreatedResponse(
    Guid CorrelationId,
    Guid TaskId
);

public record ValidateUserCommand(
    Guid CorrelationId,
    Guid UserId
);

public record UserValidated(
    Guid CorrelationId
);
