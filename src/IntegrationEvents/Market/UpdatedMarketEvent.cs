namespace IntegrationEvents
{
    public record UpdatedMarketEvent : IntegrationEvent
    {
        public virtual Market? NewMarket { get; set; }
        public virtual Market? OldMarket { get; set; }
    }
}
