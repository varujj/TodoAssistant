using FluentValidation;
using TodoAssistant.API.Domain.Dtos;

namespace TodoAssistant.API.Executors.CommandExecutors.CreateTask;

public class UpsertTaskValidator : AbstractValidator<TaskDto>
{
    public UpsertTaskValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty");

        RuleFor(dto => dto.Priority)
            .GreaterThan(0)
            .WithMessage("Priority must not be empty");

        RuleFor(dto => dto.Status)
            .IsInEnum()
            .WithMessage("Selected status is not supported");

    }
}