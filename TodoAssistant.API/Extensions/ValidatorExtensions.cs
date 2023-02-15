using CSharpFunctionalExtensions;
using FluentValidation;
using System.Linq;

namespace TodoAssistant.API.Extensions
{
    public static class ValidatorExtensions
    {
        public static Result<T> ValidateToResult<T>(this AbstractValidator<T> validator, T instance)
        {
            var result = validator.Validate(instance);
            if (result.IsValid)
            {
                return Result.Success(instance);
            }
            else
            {
                return Result.Failure<T>(GetCombinedErrorMessage(result));
            }
        }

        public static string GetCombinedErrorMessage(FluentValidation.Results.ValidationResult result)
        {
            var errorNumber = 1;

            var errors = result.Errors
                .ToDictionary(
                    e => e.PropertyName + " " + errorNumber++,
                    e => e.ErrorMessage);

            return string.Join(", ", errors.Values);
        }
    }
}
