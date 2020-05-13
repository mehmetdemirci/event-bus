using Abstraction.Events;
using Application.EventBus;
using DotNetCore.CAP.Internal;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;
using System.Linq;

namespace Application.Extensions
{
    public static class EventBusDependencyExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddCap(x =>
            {
                x.UseInMemoryStorage();
                x.UseInMemoryMessageQueue();

                x.UseDashboard();
            });

            services.AddSingleton<EventBusConsumerServiceSelector>();
            services.AddSingleton<IConsumerServiceSelector, EventBusConsumerServiceSelector>(x => x.GetRequiredService<EventBusConsumerServiceSelector>());

            services.AddScoped<EventBusManager>();
            typeof(EventBusManager).GetInterfaces().ToList()
                                   .ForEach(i => services.AddScoped(i, x => x.GetRequiredService<EventBusManager>()));

            return services;
        }

        internal static IServiceCollection SubscribeEventBus<TIntegrationEventHandler, TIntegrationEvent>(this IServiceCollection services, DispatchMode broadcastMode = DispatchMode.Unicast, string groupName = null)
        where TIntegrationEventHandler : IEventHandler<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
        {
            services.AddScoped(typeof(IEventHandler<TIntegrationEvent>), typeof(TIntegrationEventHandler));

            EventBusConsumerServiceSelector.Subscribe<TIntegrationEventHandler, TIntegrationEvent>(broadcastMode, groupName);

            return services;
        }
    }
}