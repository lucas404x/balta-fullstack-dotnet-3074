using Dima.Core.Abstractions;
using Dima.Core.Extensions;
using Dima.Core.Validators;

namespace Dima.Core.Requests.Account;

public class LoginRequest : IRequestValidate
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string? Validate() 
        => new LoginRequestValidator().Validate(this).GetErrorMessages().FirstOrDefault();
}