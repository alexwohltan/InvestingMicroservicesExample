using System;
using System.Threading.Tasks;

namespace MessageBroker
{
    public interface IDynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
