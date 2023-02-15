using CSharpFunctionalExtensions;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Extensions;
using TodoAssistant.API.Infrastructure.Persistence;

namespace TodoAssistant.API.Executors.CommandExecutors.CreateTask;

public class CreateTaskCommand
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommand(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public Result<TaskDto> Execute(TaskDto taskDto)
    {
        var validationResult = new UpsertTaskValidator().ValidateToResult(taskDto);

        if (validationResult.IsFailure)
        {
            return Result.Failure<TaskDto>(validationResult.Error);
        }

        var result = _taskRepository.Create(taskDto);

        if (result.IsSuccess)
        {
            return Result.Success(TaskDto.FromTask(result.Value));
        }

        return Result.Failure<TaskDto>(result.Error);
    }
}
