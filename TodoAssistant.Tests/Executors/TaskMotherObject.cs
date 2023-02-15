using System;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Domain.Enums;

namespace TodoAssistant.Tests.Executors
{
    public static class TaskMotherObject
    {
        public static TaskDto Task()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = "Task 123",
                Priority = 1,
                Status = Status.NotStarted
            };
        }
    }
}
