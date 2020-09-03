using Microsoft.Extensions.DependencyInjection;

namespace Ditto.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDitto(this IServiceCollection serviceCollection, DittoOptions dittoOptions)
        {
            serviceCollection.AddSingleton(dittoOptions);

            return serviceCollection;
        }
    }
}
