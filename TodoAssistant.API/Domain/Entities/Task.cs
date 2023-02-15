using System;
using TodoAssistant.API.Domain.Enums;

namespace TodoAssistant.API.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public Status Status { get; set; }

    public bool CanBeDeleted()
        => this.Status == Status.Completed;
}