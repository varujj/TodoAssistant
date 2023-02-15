using FluentValidation;
using System;

namespace TodoAssistant.API.Executors.CommandExecutors.DeleteTask;

public class DeleteTaskValidator : AbstractValidator<Guid>
{
    public DeleteTaskValidator()
    {
        RuleFor(id => id)
           .NotEmpty()
           .WithMessage("Id must not be empty");
    }
}
