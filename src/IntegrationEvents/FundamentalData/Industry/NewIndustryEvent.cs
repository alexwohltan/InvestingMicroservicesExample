namespace IntegrationEvents
{
    public record NewIndustryEvent : IntegrationEvent
	{
		public virtual DataStructures.Industry? NewIndustry { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
	}
}

