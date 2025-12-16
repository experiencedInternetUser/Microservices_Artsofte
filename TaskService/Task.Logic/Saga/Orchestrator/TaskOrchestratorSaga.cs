using MassTransit;
using System;
using TaskService.Task.Contracts;
using TaskService.Task.Core.Interfaces;
using TaskEntity = TaskService.Task.Core.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace TaskService.Task.Logic.Saga.Orchestrator
{
    public class TaskOrchestratorSaga : MassTransitStateMachine<TaskOrchestratorState>
    {
        public State Processing { get; private set; } = null!;
        public Event<CreateTaskRequested> CreateRequested { get; private set; } = null!;

        public TaskOrchestratorSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CreateRequested, x =>
                x.CorrelateById(ctx => ctx.Message.TaskId));

            Initially(
                When(CreateRequested)
                    .ThenAsync(async ctx =>
                    {
                        // Здесь вызывается бизнес-логика
                        // Реальная логика в consumer-е, не в saga
                        await Task.CompletedTask;
                    })
                    .Publish(ctx => new TaskCreated
                    {
                        TaskId = ctx.Message.TaskId
                    })
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
