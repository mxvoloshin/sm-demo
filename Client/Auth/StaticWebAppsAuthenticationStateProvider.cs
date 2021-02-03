using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Client.Auth
{
    public class StaticWebAppsAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _http;

        public StaticWebAppsAuthenticationStateProvider(IConfiguration config, IWebAssemblyHostEnvironment environment)
        {
            _config = config;
            _http = new HttpClient { BaseAddress = new Uri(environment.BaseAddress) };
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var authDataUrl = _config.GetValue<string>("StaticWebAppsAuthentication:AuthenticationDataUrl", "/.auth/me");
                var data = await _http.GetFromJsonAsync<AuthenticationData>(authDataUrl);

                var principal = data.ClientPrincipal;
                principal.UserRoles = principal.UserRoles.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);

                if (!principal.UserRoles.Any())
                {
                    return new AuthenticationState(new ClaimsPrincipal());
                }

                var identity = new ClaimsIdentity(principal.IdentityProvider);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
                identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
                identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }
    }
}
