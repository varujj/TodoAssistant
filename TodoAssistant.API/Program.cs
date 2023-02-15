using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TodoAssistant.API.Executors.CommandExecutors.CreateTask;
using TodoAssistant.API.Executors.CommandExecutors.DeleteTask;
using TodoAssistant.API.Executors.CommandExecutors.EditTask;
using TodoAssistant.API.Executors.QueryExecutors.GetTasks;
using TodoAssistant.API.Infrastructure.Persistence;
using TodoAssistant.API.Infrastructure.Persistence.MemoryDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();

builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<CreateTaskCommand>();
builder.Services.AddTransient<EditTaskCommand>();
builder.Services.AddTransient<DeleteTaskCommand>();
builder.Services.AddTransient<GetTasksQuery>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "FrontEnd.Policy",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:44487")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("FrontEnd.Policy");
app.Run();
