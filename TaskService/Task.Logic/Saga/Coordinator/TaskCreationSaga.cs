using MassTransit;
using System;
using TaskService.Task.Contracts;

namespace TaskService.Task.Logic.Saga.Coordinator
{
    public class TaskCreationSaga : MassTransitStateMachine<TaskCreationState>
    {
        public State Creating { get; private set; } = null!;

        public Event<CreateTaskRequested> CreateRequested { get; private set; } = null!;
        public Event<TaskCreated> TaskCreated { get; private set; } = null!;
        public Event<TaskCreationFailed> TaskFailed { get; private set; } = null!;

        public TaskCreationSaga()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CreateRequested, x =>
                x.CorrelateById(ctx => ctx.Message.TaskId));

            Event(() => TaskCreated, x =>
                x.CorrelateById(ctx => ctx.Message.TaskId));

            Event(() => TaskFailed, x =>
                x.CorrelateById(ctx => ctx.Message.TaskId));

            Initially(
                When(CreateRequested)
                    .TransitionTo(Creating)
                    .Publish(ctx => new CreateTaskRequested
                    {
                        TaskId = ctx.Message.TaskId,
                        Title = ctx.Message.Title,
                        UserId = ctx.Message.UserId
                    })
            );

            During(Creating,
                When(TaskCreated)
                    .Finalize(),

                When(TaskFailed)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
