using Microsoft.AspNetCore.Mvc;
using System;
using TodoAssistant.API.Domain.Dtos;
using TodoAssistant.API.Executors.CommandExecutors.CreateTask;
using TodoAssistant.API.Executors.CommandExecutors.DeleteTask;
using TodoAssistant.API.Executors.CommandExecutors.EditTask;
using TodoAssistant.API.Executors.QueryExecutors.GetTasks;
using TodoAssistant.API.Extensions;

namespace TodoAssistant.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(
           [FromServices] GetTasksQuery query,
           Guid id)
        {
            return query.GetTask(id).ToActionResult();
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromServices] GetTasksQuery query)
        {
            return query.GetTasks().ToActionResult();
        }

        [HttpPost]
        public IActionResult Create(
            [FromServices] CreateTaskCommand command,
            [FromBody] UpsertTaskRequestDto requestDto)
        {
            return command.Execute(requestDto.Task).ToActionResult();
        }

        [HttpPut]
        public IActionResult Edit(
            [FromServices] EditTaskCommand command,
            [FromBody] UpsertTaskRequestDto requestDto)
        {
            return command.Execute(requestDto.Task).ToActionResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(
            [FromServices] DeleteTaskCommand command,
            Guid id)
        {
            return command.Execute(id).ToActionResult();
        }
    }
}