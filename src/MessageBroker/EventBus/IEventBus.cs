using System;
using System.Text.Json;

namespace MessageBroker
{
    public interface IEventBus
    {
        void Publish<T>(T @event)
            where T : IntegrationEvent;

        void Subscribe<T, TH>(TH handler)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName, TH handler)
            where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName, TH handler)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>(TH handler)
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
