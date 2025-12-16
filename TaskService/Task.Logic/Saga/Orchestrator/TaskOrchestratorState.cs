using MassTransit;
using System;

namespace TaskService.Task.Logic.Saga.Orchestrator
{
    public class TaskOrchestratorState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = string.Empty;
    }
}
