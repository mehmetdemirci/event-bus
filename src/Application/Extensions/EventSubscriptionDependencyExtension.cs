using Abstraction.Events;
using Application.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class EventSubscriptionDependencyExtension
    {
        public static IServiceCollection AddEventSubscription(this IServiceCollection services)
        {
            services.SubscribeEventBus<SingleGroup1EventHandler, SingleIntegrationEvent>();
            services.SubscribeEventBus<SingleGroup2EventHandler, SingleIntegrationEvent>();

            services.SubscribeEventBus<MultiGroup1EventHandler, GroupIntegrationEvent>(DispatchMode.Broadcast, "Group1");
            services.SubscribeEventBus<MultiGroup2EventHandler, GroupIntegrationEvent>(DispatchMode.Broadcast, "Group2");

            return services;
        }
    }
}