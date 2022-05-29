namespace IntegrationEvents
{
    public record UpdatedSectorEvent : IntegrationEvent
	{
		public virtual Sector? NewSector { get; set; }
		public virtual Sector? OldSector { get; set; }
		public virtual string? MarketName { get; set; }
	}
}

