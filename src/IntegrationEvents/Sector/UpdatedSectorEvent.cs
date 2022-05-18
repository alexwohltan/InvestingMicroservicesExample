namespace IntegrationEvents
{
    public record UpdatedSectorEvent : IntegrationEvent
	{
		public virtual DataStructures.Sector? NewSector { get; set; }
		public virtual DataStructures.Sector? OldSector { get; set; }
		public virtual string? MarketName { get; set; }
	}
}

