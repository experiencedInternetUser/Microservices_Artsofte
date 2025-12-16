using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskService.Task.Logic.Saga.Coordinator;
using TaskService.Task.Logic.Saga.Orchestrator;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Controllers
// --------------------
builder.Services.AddControllers();

// --------------------
// Swagger
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// MassTransit + RabbitMQ
// --------------------
builder.Services.AddMassTransit(x =>
{
    // -------- Coordinator Saga --------
    x.AddSagaStateMachine<TaskCreationSaga, TaskCreationState>()
        .InMemoryRepository();

    // -------- Orchestrator Saga --------
    x.AddSagaStateMachine<TaskOrchestratorSaga, TaskOrchestratorState>()
        .InMemoryRepository();

    // -------- Transport --------
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// --------------------
// Middleware
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
