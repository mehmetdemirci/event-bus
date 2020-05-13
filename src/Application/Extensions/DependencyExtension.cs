using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();
            services.AddMediatR(assm);
            services.AddEventBus();
            services.AddEventSubscription();

            return services;
        }
    }
}