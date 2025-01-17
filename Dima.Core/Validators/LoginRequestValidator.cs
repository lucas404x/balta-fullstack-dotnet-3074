using Dima.Core.Requests.Account;
using Dima.Core.Validators.Shared;
using FluentValidation;

namespace Dima.Core.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("O campo 'E-mail' é necessário.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("O campo 'Senha' é necessário")
            .MinimumLength(8).MaximumLength(64).WithMessage("A senha deve conter entre 8 e 64 caracteres.")
            .HasSpecialCharacters()
            .HasUpperAndLowerCaseCharacters();
    }
}