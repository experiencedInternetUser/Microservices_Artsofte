using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dal.Repositories;
using Logic.Services;
using Logic.Http;
using Logic.Trace;
using Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// HTTP client factory
builder.Services.AddHttpClient();

// DI registrations for repositories (как было)
builder.Services.AddSingleton<InMemoryProjectRepository>();
builder.Services.AddSingleton<InMemoryLabelRepository>();
builder.Services.AddSingleton<InMemoryTaskRepository>();
builder.Services.AddSingleton<CoreLib.Interfaces.IProjectRepository>(sp => sp.GetRequiredService<InMemoryProjectRepository>());
builder.Services.AddSingleton<CoreLib.Interfaces.ILabelRepository>(sp => sp.GetRequiredService<InMemoryLabelRepository>());
builder.Services.AddSingleton<CoreLib.Interfaces.ITaskRepository>(sp => sp.GetRequiredService<InMemoryTaskRepository>());

// TraceId accessor (scoped per request) - регистрируем конкретный тип и интерфейсы через него
builder.Services.AddScoped<TraceIdAccessor>();
builder.Services.AddScoped<ITraceReader>(sp => sp.GetRequiredService<TraceIdAccessor>());
builder.Services.AddScoped<ITraceWriter>(sp => sp.GetRequiredService<TraceIdAccessor>());

// Http services
builder.Services.AddSingleton<IHttpConnectionService, HttpConnectionService>();
builder.Services.AddScoped<IHttpRequestService, HttpRequestService>();

// Services
builder.Services.AddScoped<Logic.Services.IProjectService, Logic.Services.ProjectService>();
builder.Services.AddScoped<Logic.Services.ILabelService, Logic.Services.LabelService>();
builder.Services.AddScoped<Logic.Services.ITaskService, Logic.Services.TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Trace middleware must come early
app.UseMiddleware<TraceMiddleware>();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
