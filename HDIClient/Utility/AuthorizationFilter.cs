using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using HDIClient.Controllers;

namespace HDIClient.Utility
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IMemoryCache _cache;

        public AuthorizationFilter(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_cache.TryGetValue("Token", out _))
            {
                // Redireccionar al usuario a la página de inicio de sesión
                context.Result = new RedirectToActionResult("LoginView", "Login", null);
            }
        }
    }
}
