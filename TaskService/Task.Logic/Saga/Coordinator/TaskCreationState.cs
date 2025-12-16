using MassTransit;
using System;

namespace TaskService.Task.Logic.Saga.Coordinator
{
    public class TaskCreationState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = string.Empty;
    }
}
