namespace IntegrationEvents
{
    public record DeletedIndustryEvent : IntegrationEvent
	{
		public virtual Industry? OldIndustry { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
	}
}

