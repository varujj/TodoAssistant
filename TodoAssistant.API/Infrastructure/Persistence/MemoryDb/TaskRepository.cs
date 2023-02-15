using CSharpFunctionalExtensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Domain.Entities;
using TodoAssistant.API.Domain.Enums;

namespace TodoAssistant.API.Infrastructure.Persistence.MemoryDb;

public class TaskRepository : ITaskRepository
{
    public TaskRepository()
    {
        InitExampleData();
    }

    /// <summary>
    /// Key is <see cref="Task.Name"/>
    /// </summary>
    private readonly ConcurrentDictionary<string, Task> _tasks = new();

    public List<Task> GetAll()
    {
        return _tasks.Values.ToList();
    }

    public Result<Task> TryGetTask(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Value.Id == id).Value;

        if (task is null)
        {
            return NotExists;
        }

        return Result.Success(task);
    }

    public Result<Task> Create(TaskDto taskDto)
    {
        if (TaskNameExists(taskDto))
        {
            return AlreadyExists;
        }

        taskDto.Id = Guid.NewGuid();
        var newTask = taskDto.ToTask();
        var added = _tasks.TryAdd(taskDto.Name, newTask);

        return added
               ? Result.Success(newTask)
               : AlreadyExists;
    }

    public Result Edit(TaskDto taskDto)
    {
        var result = TryGetTask(taskDto.Id);

        if (result.IsFailure)
        {
            return NotExists;
        }

        Delete(taskDto.Id);

        return Create(taskDto);
    }

    public Result Delete(Guid id)
    {
        var result = TryGetTask(id);

        if (result.IsFailure)
        {
            return NotExists;
        }

        var task = result.Value;

        if (!task.CanBeDeleted())
        {
            return Result.Failure("Not completed tasks cannot be deleted");
        }

        var removed = _tasks.TryRemove(KeyValuePair.Create(task.Name, task));

        return removed
              ? Result.Success()
              : NotExists;
    }

    private bool TaskNameExists(TaskDto taskDto)
        => TaskNameExists(taskDto.Name);

    private bool TaskNameExists(string taskName)
        => _tasks.ContainsKey(taskName);

    private static Result<Task> AlreadyExists
        => Result.Failure<Task>("Task with provided name already exists");

    private static Result<Task> NotExists
        => Result.Failure<Task>("Task with provided data doesn't exist");

    private void InitExampleData()
    {
        var tasks = new List<Task>
        {
            new Task { Id = Guid.NewGuid(), Name = "Task 1", Priority = 1, Status = Status.NotStarted },
            new Task { Id = Guid.NewGuid(), Name = "Task 2", Priority = 2, Status = Status.InProgress },
            new Task { Id = Guid.NewGuid(), Name = "Task 3", Priority = 3, Status = Status.Completed }
        };

        tasks.ForEach(t => _tasks.TryAdd(t.Name, t));
    }
}
