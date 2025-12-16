using MassTransit;
using Task.Contracts.Saga;

namespace Logic.Saga.Orchestrator;

public class TaskOrchestratorSaga : MassTransitStateMachine<TaskOrchestratorState>
{
    public State CreatingTask { get; private set; }

    public Event<StartTaskOrchestration> Start { get; private set; }
    public Event<TaskCreatedResponse> TaskCreated { get; private set; }
    public Event<UserValidated> UserValidated { get; private set; }

    public TaskOrchestratorSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => Start, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => TaskCreated, x => x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => UserValidated, x => x.CorrelateById(m => m.Message.CorrelationId));

        Initially(
            When(Start)
                .Send(
                    new Uri("queue:create-task"),
                    ctx => new CreateTaskCommand(
                        ctx.Instance.CorrelationId,
                        ctx.Message.ProjectId,
                        ctx.Message.Title
                    )
                )
                .TransitionTo(CreatingTask)
        );

        During(CreatingTask,
            When(TaskCreated)
                .Then(ctx => ctx.Instance.TaskId = ctx.Message.TaskId)
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}
