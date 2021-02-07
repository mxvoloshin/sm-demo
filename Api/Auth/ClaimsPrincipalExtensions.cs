using System.Linq;
using System.Security.Claims;

namespace BlazorApp.Api.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static string ClientId(this ClaimsPrincipal principal)
        {
            var nameClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            return nameClaim?.Value;
        }
    }
}
