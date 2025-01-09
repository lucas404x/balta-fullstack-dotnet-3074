using FluentValidation.Results;

namespace Dima.Core.Extensions;

internal static class FluentValidationExtensions
{
    internal static List<string> GetErrorMessages(this ValidationResult validationResult) 
        => [.. validationResult.Errors.Select(x => x.ErrorMessage)];
}