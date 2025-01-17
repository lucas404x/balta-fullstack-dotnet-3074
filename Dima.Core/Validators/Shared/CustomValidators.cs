using System.Text.RegularExpressions;
using FluentValidation;

namespace Dima.Core.Validators.Shared;

public static partial class CustomValidators
{
    #region Regex
    
    [GeneratedRegex(@"[^\w\s]")]
    private static partial Regex SpecialCharactersRegex();
    
    [GeneratedRegex(@"(?=.*[a-z])(?=.*[A-Z])")]
    private static partial Regex UpperAndLowerCharsRegex();
    
    #endregion
    
    public static IRuleBuilderOptions<T, string?> HasSpecialCharacters<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(s => !string.IsNullOrWhiteSpace(s) && SpecialCharactersRegex().IsMatch(s))
            .WithMessage("O campo deve conter pelo menos um caracter especial.");
    }
    
    public static IRuleBuilderOptions<T, string?> HasUpperAndLowerCaseCharacters<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(s => !string.IsNullOrWhiteSpace(s) && UpperAndLowerCharsRegex().IsMatch(s))
            .WithMessage("O campo deve conter pelo menos um caracter maiúsculo e minúsculo.");
    }
}