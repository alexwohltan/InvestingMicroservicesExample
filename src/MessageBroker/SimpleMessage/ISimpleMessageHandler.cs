using System;
using System.Threading.Tasks;

namespace MessageBroker
{
    public interface ISimpleMessageHandler<T>
        where T : SimpleMessage
    {
        Task Handle(T message);
    }
}
