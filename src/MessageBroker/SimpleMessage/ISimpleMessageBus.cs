using System;
namespace MessageBroker
{
    public interface ISimpleMessageBus
    {
        void Publish<T>(T message)
            where T : SimpleMessage;

        void Subscribe<T, TH>(TH handler)
            where T : SimpleMessage
            where TH : ISimpleMessageHandler<T>;

        void Unsubscribe<T, TH>()
            where T : SimpleMessage
            where TH : ISimpleMessageHandler<T>;
    }
}
