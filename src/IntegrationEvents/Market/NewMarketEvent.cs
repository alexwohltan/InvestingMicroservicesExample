using DataStructures;
using MessageBroker;

namespace IntegrationEvents
{
    public class NewMarketEvent : IntegrationEvent
    {
        public virtual Market NewMarket { get; set; }
    }
}
