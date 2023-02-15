using System;
using TodoAssistant.API.Domain.Entities;
using TodoAssistant.API.Domain.Enums;

namespace TodoAssistant.API.Domain.Dtos;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public Status Status { get; set; }

    public Task ToTask()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            Priority = Priority,
            Status = Status
        };
    }

    public static TaskDto FromTask(Task task)
    {
        return new()
        {
            Id = task.Id,
            Name = task.Name,
            Priority = task.Priority,
            Status = task.Status
        };
    }
}