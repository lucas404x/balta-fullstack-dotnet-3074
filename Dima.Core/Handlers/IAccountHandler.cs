using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface IAccountHandler
{
    Task<ApiResponse<string>> Login(LoginRequest request);
    Task<ApiResponse<string>> Register(RegisterRequest request);
    Task Logout();
}