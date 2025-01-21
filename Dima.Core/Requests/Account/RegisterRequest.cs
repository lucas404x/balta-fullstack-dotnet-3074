using Dima.Core.Extensions;
using Dima.Core.Validators;

namespace Dima.Core.Requests.Account;

public class RegisterRequest : LoginRequest
{
    public override string? Validate() 
        => new RegisterRequestValidator().Validate(this).GetErrorMessages().FirstOrDefault();
}