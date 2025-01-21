using System.Net.Http.Json;
using System.Security.Claims;
using Dima.Core.Entities.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Auth;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> CheckAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthStateChanged();
}

public class CookieAuthenticationStateProvider(IHttpClientFactory httpClientFactory) 
    : AuthenticationStateProvider, ICookieAuthenticationStateProvider
{
    private bool _isAuthenticated;
    
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientName);
    
    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }
    
    public void NotifyAuthStateChanged()
       => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        var userInfo = await GetUser();
        if (userInfo is null)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        var claims = await GetClaims(userInfo);
        var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
        var user = new ClaimsPrincipal(id);
        _isAuthenticated = true;
        return new AuthenticationState(user);
    }

    private async Task<User?> GetUser()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<User?>("identity/manage/info");
        }
        catch
        {
            return null;
        }
    }
    

    private async Task<List<Claim>> GetClaims(User user)
    {
        List<Claim> claims = [
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email)
        ];
        claims.AddRange(
            user.Claims
                .Where(x => x.Key != ClaimTypes.Name && x.Key != ClaimTypes.Email)
                .Select(x => new Claim(x.Key, x.Value))
            );

        RoleClaim[]? roles;
        try
        {
            roles = await _httpClient.GetFromJsonAsync<RoleClaim[]>("identity/roles");
        }
        catch
        {
            return claims;
        }

        foreach (var role in roles ?? [])
        {
            if (!string.IsNullOrWhiteSpace(role.Type) && !string.IsNullOrWhiteSpace(role.Value))
            {
                var claim = new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer);
                claims.Add(claim);
            }
        }

        return claims;
    }
}