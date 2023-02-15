using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Infrastructure.Persistence;

namespace TodoAssistant.API.Executors.QueryExecutors.GetTasks;

public class GetTasksQuery
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public List<TaskDto> GetTasks()
    {
        return _taskRepository
                    .GetAll()
                    .Select(TaskDto.FromTask)
                    .ToList();
    }

    public Result<TaskDto> GetTask(Guid id)
    {
        var result = _taskRepository.TryGetTask(id);

        if (result.IsFailure)
        {
            return Result.Failure<TaskDto>(result.Error);
        }

        return Result.Success(TaskDto.FromTask(result.Value));
    }
}
