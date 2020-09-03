using Ditto.Common.Spa;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ditto.Common.Middlewares
{
    public class DittoMiddleware
    {
        private readonly RequestDelegate _next;

        public DittoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            DittoOptions dittoOptions = context.RequestServices.GetRequiredService<DittoOptions>();
            if(!context.Request.PathBase.ToString().ToLower().Equals(dittoOptions.Path.ToString().ToLower()))
            {
                await _next(context);
                return;
            }    

            string transformName = context.Request.Query["key"].ToString().ToLower();
            Transform transform = dittoOptions?.Transforms?.FirstOrDefault(t => t.TransformName.Equals(transformName));

            if (transform == null)
            {
                await context.Response.WriteAsync(Resources.GetHtml(dittoOptions));
            }
            else
            {
                IIdentity identity = new ClaimsIdentity(transform.Claims, dittoOptions.AuthenticationType);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                AuthenticationProperties authenticationProperties = new AuthenticationProperties();

                await context.SignInAsync(dittoOptions.AuthenticationType, claimsPrincipal, authenticationProperties);

                await context.Response.WriteAsync($"You are logged in with profile: {JsonConvert.SerializeObject(transform)}");
            }
        }
    }
}
