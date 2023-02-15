using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoAssistant.API.Executors.CommandExecutors.CreateTask;
using TodoAssistant.API.Executors.CommandExecutors.EditTask;
using TodoAssistant.API.Infrastructure.Persistence;
using TodoAssistant.API.Infrastructure.Persistence.MemoryDb;

namespace TodoAssistant.Tests.Executors;

using static TaskMotherObject;

[TestClass]
public class EditTaskCommandTests
{
    private ITaskRepository _repo;
    private CreateTaskCommand _createCommand;
    private EditTaskCommand _editCommand;

    [TestMethod]
    public void Execute_works()
    {
        InitSut();

        var taskOne = Task();
        var taskTwo = Task();

        taskOne = _createCommand.Execute(taskOne).Value;

        taskTwo.Id = taskOne.Id;
        taskTwo.Name = "New name";

        _editCommand.Execute(taskTwo);

         _repo.GetAll().Should().ContainSingle(a => a.Name == "New name");
    }

    private void InitSut()
    {
        _repo = new TaskRepository();
        _createCommand = new CreateTaskCommand(_repo);
        _editCommand = new EditTaskCommand(_repo);
    }
}
