using TodoAssistant.API.Domain.Dtos;

namespace TodoAssistant.API.Executors.CommandExecutors.CreateTask;

public record UpsertTaskRequestDto(TaskDto Task);