using MassTransit;

namespace Logic.Saga.Orchestrator;

public class TaskOrchestratorState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = null!;
    public Guid TaskId { get; set; }
}
