using Dima.Core.Requests.Account;
using FluentValidation;

namespace Dima.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        Include(new LoginRequestValidator());
    }
}