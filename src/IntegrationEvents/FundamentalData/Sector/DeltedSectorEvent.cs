namespace IntegrationEvents
{
    public record DeletedSectorEvent : IntegrationEvent
	{
		public virtual DataStructures.Sector? OldSector { get; set; }
		public virtual string? MarketName { get; set; }
	}
}

