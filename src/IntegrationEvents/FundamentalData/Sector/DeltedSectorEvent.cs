namespace IntegrationEvents
{
    public record DeletedSectorEvent : IntegrationEvent
	{
		public virtual Sector? OldSector { get; set; }
		public virtual string? MarketName { get; set; }
	}
}

