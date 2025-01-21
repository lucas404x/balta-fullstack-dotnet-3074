using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Dima.Web.Auth;

internal class CookieHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.SameOrigin);
        request.Headers.Add("X-Requested-With", ["XMLHttpRequest"]);
        return base.SendAsync(request, cancellationToken);
    }
}