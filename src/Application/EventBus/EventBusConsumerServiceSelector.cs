using Abstraction.Events;
using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application.EventBus
{
    internal sealed class EventBusConsumerServiceSelector : ConsumerServiceSelector
    {
        private static List<ConsumerExecutorDescriptor> consumerExecutorDescriptors = new List<ConsumerExecutorDescriptor>();

        public EventBusConsumerServiceSelector(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public static void Subscribe<TIntegrationEventHandler, TIntegrationEvent>(DispatchMode broadcastMode = DispatchMode.Unicast, string groupName = null)
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IEventHandler<TIntegrationEvent>
        {
            var descriptor = PrepareDescriptor<TIntegrationEventHandler, TIntegrationEvent>(broadcastMode, groupName);
            consumerExecutorDescriptors.Add(descriptor);
        }

        protected override IEnumerable<ConsumerExecutorDescriptor> FindConsumersFromControllerTypes()
        {
            var defaultConsumers = base.FindConsumersFromControllerTypes().ToList();
            defaultConsumers.AddRange(consumerExecutorDescriptors);

            return defaultConsumers;
        }

        private static ConsumerExecutorDescriptor PrepareDescriptor<TIntegrationEventHandler, TIntegrationEvent>(DispatchMode broadcastMode, string groupName)
                    where TIntegrationEventHandler : IEventHandler<TIntegrationEvent>
            where TIntegrationEvent : IntegrationEvent
        {
            var consExecDesc = new ConsumerExecutorDescriptor();

            consExecDesc.ServiceTypeInfo = typeof(IEventHandler<TIntegrationEvent>).GetTypeInfo();
            consExecDesc.MethodInfo = typeof(TIntegrationEventHandler).GetMethod("HandleAsync");
            consExecDesc.ImplTypeInfo = typeof(TIntegrationEventHandler).GetTypeInfo();
            consExecDesc.Attribute = new CapSubscribeAttribute(typeof(TIntegrationEvent).FullName);

            if (broadcastMode == DispatchMode.Broadcast && string.IsNullOrEmpty(groupName) == false)
            {
                consExecDesc.Attribute.Group = $"{typeof(TIntegrationEvent).FullName}-{groupName}";
            }
            else
            {
                consExecDesc.Attribute.Group = typeof(TIntegrationEvent).FullName;
            }

            consExecDesc.MethodInfo.GetParameters()
                                   .Select(parameter => new ParameterDescriptor
                                   {
                                       Name = parameter.Name,
                                       ParameterType = parameter.ParameterType,
                                   })
                                   .ToList()
                                   .ForEach(pDesc => consExecDesc.Parameters = new List<ParameterDescriptor> { pDesc });

            return consExecDesc;
        }
    }
}