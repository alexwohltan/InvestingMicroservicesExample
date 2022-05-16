using DataStructures;
using EventBus.Events;

namespace IntegrationEvents
{
    public record NewMarketEvent : IntegrationEvent
    {
        public virtual Market? NewMarket { get; set; }
    }
}
