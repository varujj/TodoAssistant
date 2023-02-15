using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Domain.Enums;
using TodoAssistant.API.Executors.CommandExecutors.CreateTask;
using TodoAssistant.API.Executors.CommandExecutors.DeleteTask;
using TodoAssistant.API.Infrastructure.Persistence;
using TodoAssistant.API.Infrastructure.Persistence.MemoryDb;

namespace TodoAssistant.Tests.Executors;

using static TaskMotherObject;

[TestClass]
public class DeleteTaskCommandTests
{
    private ITaskRepository _repo;
    private CreateTaskCommand _createCommand;
    private DeleteTaskCommand _deleteCommand;

    [TestMethod]
    public void Execute_works()
    {
        InitSut();

        var taskOne = Task();

        taskOne = _createCommand.Execute(taskOne).Value;

        _deleteCommand.Execute(taskOne.Id);

        _repo.GetAll().Should().NotContain(a => a.Name == "New name");
    }

    private void InitSut()
    {
        _repo = new TaskRepository();
        _createCommand = new CreateTaskCommand(_repo);
        _deleteCommand = new DeleteTaskCommand(_repo);
    }
}
