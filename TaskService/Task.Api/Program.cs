using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dal.Repositories;
using Logic.Services;

var builder = WebApplication.CreateBuilder(args);

// DI registrations
builder.Services.AddSingleton<InMemoryProjectRepository>();
builder.Services.AddSingleton<InMemoryLabelRepository>();
builder.Services.AddSingleton<InMemoryTaskRepository>();
builder.Services.AddSingleton<CoreLib.Interfaces.IProjectRepository>(sp => sp.GetRequiredService<InMemoryProjectRepository>());
builder.Services.AddSingleton<CoreLib.Interfaces.ILabelRepository>(sp => sp.GetRequiredService<InMemoryLabelRepository>());
builder.Services.AddSingleton<CoreLib.Interfaces.ITaskRepository>(sp => sp.GetRequiredService<InMemoryTaskRepository>());

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

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
