using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.Application.Services;
using User.Infrastructure.Data;
using User.Infrastructure.Repositories;
using User.Api.Trace;
using User.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// DbContext (InMemory for demo)
builder.Services.AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("UserDb"));

// Trace accessor
builder.Services.AddScoped<ITraceReader, TraceIdAccessor>();
builder.Services.AddScoped<ITraceWriter>(sp => sp.GetRequiredService<ITraceReader>() as ITraceWriter);

// Repositories
builder.Services.AddScoped<User.Core.Interfaces.IUserRepository, UserRepository>();
builder.Services.AddScoped<User.Core.Interfaces.IRoleRepository, RoleRepository>();
builder.Services.AddScoped<User.Core.Interfaces.IProfileRepository, ProfileRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

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

// use Trace middleware
app.UseMiddleware<TraceMiddleware>();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
