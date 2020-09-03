using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Ditto.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDitto(this IApplicationBuilder app, DittoOptions dittoOptions)
        {
            app.UseAuthentication();

            app.Map(dittoOptions.Path, configuration =>
            {
                configuration.Use(async (HttpContext httpContext, Func<Task> requestDelegate) =>
                {
                    string transformName =  httpContext.Request.Query["key"];
                    Transform transform = dittoOptions.Transforms.FirstOrDefault(t => t.TransformName.Equals(transformName));

                    if (transform == null)
                    {
                        await requestDelegate.Invoke();
                    }
                    else
                    {
                        IIdentity identity = new ClaimsIdentity(transform.Claims, dittoOptions.AuthenticationType);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                        AuthenticationProperties authenticationProperties = new AuthenticationProperties();

                        await httpContext.SignInAsync(dittoOptions.AuthenticationType, claimsPrincipal, authenticationProperties);
                    }
                });
            });
        }
    }
}
