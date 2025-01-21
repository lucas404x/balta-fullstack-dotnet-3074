using System.Net;
using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _httpClient = 
        httpClientFactory.CreateClient(AppConfiguration.HttpClientName);
    
    public async Task<ApiResponse<string>> Login(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("identity/login?useCookies=true", request);
        return response.IsSuccessStatusCode
            ? new ApiResponse<string>(string.Empty, string.Empty)
            : new ApiResponse<string>("Não foi possível realizar o login.", HttpStatusCode.BadRequest);
    }

    public async Task<ApiResponse<string>> Register(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("identity/register", request);
        return response.IsSuccessStatusCode
            ? new ApiResponse<string>(string.Empty, "Cadastrado realizado com sucesso!")
            : new ApiResponse<string>("Não foi possível realizar o cadastro.", HttpStatusCode.BadRequest);
    }

    public async Task Logout()
        => await _httpClient.PostAsync("/identity/logout", null);
}