using CSharpFunctionalExtensions;
using System;
using TodoAssistant.API.Extensions;
using TodoAssistant.API.Infrastructure.Persistence;

namespace TodoAssistant.API.Executors.CommandExecutors.DeleteTask;

public class DeleteTaskCommand
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskCommand(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Result Execute(Guid id)
    {
        var validationResult = new DeleteTaskValidator().ValidateToResult(id);

        if (validationResult.IsFailure)
        {
            return Result.Failure(validationResult.Error);
        }

        return _taskRepository.Delete(id);
    }
}
