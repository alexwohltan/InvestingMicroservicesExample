namespace IntegrationEvents
{
    public record DeletedIndustryEvent : IntegrationEvent
	{
		public virtual DataStructures.Industry? OldIndustry { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
	}
}

