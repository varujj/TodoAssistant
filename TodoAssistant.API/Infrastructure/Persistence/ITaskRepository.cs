using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Domain.Entities;

namespace TodoAssistant.API.Infrastructure.Persistence;

public interface ITaskRepository
{
    Result<Task> Create(TaskDto taskDto);
    Result Delete(Guid id);
    Result Edit(TaskDto taskDto);
    List<Task> GetAll();
    Result<Task> TryGetTask(Guid id);
}
