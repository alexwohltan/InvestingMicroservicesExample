namespace IntegrationEvents
{
    public record DeletedMarketEvent : IntegrationEvent
    {
        public virtual Market? OldMarket { get; set; }
    }
}
