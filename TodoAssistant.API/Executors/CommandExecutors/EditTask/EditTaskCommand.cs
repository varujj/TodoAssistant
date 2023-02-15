using CSharpFunctionalExtensions;
using TodoAssistant.API.Executors.CommandExecutors.CreateTask;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Extensions;
using TodoAssistant.API.Infrastructure.Persistence;

namespace TodoAssistant.API.Executors.CommandExecutors.EditTask;

public class EditTaskCommand
{
    private readonly ITaskRepository _taskRepository;

    public EditTaskCommand(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Result Execute(TaskDto taskDto)
    {
        var validationResult = new UpsertTaskValidator().ValidateToResult(taskDto);

        if (validationResult.IsFailure)
        {
            return Result.Failure(validationResult.Error);
        }

        return _taskRepository.Edit(taskDto);
    }
}
