using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Domain.Enums;
using TodoAssistant.API.Executors.CommandExecutors.CreateTask;
using TodoAssistant.API.Infrastructure.Persistence;
using TodoAssistant.API.Infrastructure.Persistence.MemoryDb;

namespace TodoAssistant.Tests.Executors;

using static TaskMotherObject;

[TestClass]
public class CreateTaskCommandTests
{
    private ITaskRepository _repo;

    [TestMethod]
    public void Execute_works()
    {
        var sut = GetSut();
        var newTask = Task();

        sut.Execute(newTask);

        _repo.TryGetTask(newTask.Id).IsSuccess.Should().BeTrue();
    }

    [TestMethod]
    public void Uniqueness_is_considered()
    {
        var sut = GetSut();
        var newTask = Task();

        sut.Execute(newTask);
        var res = sut.Execute(newTask);

        res.IsSuccess.Should().BeFalse();
    }

    private CreateTaskCommand GetSut()
    {
        _repo = new TaskRepository();

        return new(_repo);
    }        
}
