using MassTransit;

namespace Logic.Saga.Coordinator;

public class TaskCreationState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = null!;
    public Guid TaskId { get; set; }
}
