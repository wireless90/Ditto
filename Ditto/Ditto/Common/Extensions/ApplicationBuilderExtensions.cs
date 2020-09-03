using Ditto.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ditto.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDitto(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            DittoOptions dittoOptions = app.ApplicationServices.GetRequiredService<DittoOptions>();

            app.Map(dittoOptions.Path, configuration => configuration.UseMiddleware<DittoMiddleware>());
        }
    }
}
