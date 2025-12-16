using MassTransit;
using Task.Contracts.Saga;

namespace Logic.Saga.Coordinator;

public class TaskCreationSaga : MassTransitStateMachine<TaskCreationState>
{
    public State WaitingForTask { get; private set; }

    public Event<CreateTaskRequested> CreateRequested { get; private set; }
    public Event<TaskCreated> TaskCreated { get; private set; }
    public Event<UserValidationFailed> UserFailed { get; private set; }

    public TaskCreationSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreateRequested, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => TaskCreated, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => UserFailed, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(CreateRequested)
                .TransitionTo(WaitingForTask)
        );

        During(WaitingForTask,
            When(TaskCreated)
                .Then(ctx => ctx.Instance.TaskId = ctx.Message.TaskId)
                .Finalize(),

            When(UserFailed)
                .ThenAsync(_ =>
                {
                    // compensation (например, лог или удаление задачи)
                    return Task.CompletedTask;
                })
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}
