namespace IntegrationEvents
{
    public record NewSectorEvent : IntegrationEvent
	{
		public virtual DataStructures.Sector? NewSector { get; set; }
		public virtual string? MarketName { get; set; }
	}
}

