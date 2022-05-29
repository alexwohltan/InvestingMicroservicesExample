namespace IntegrationEvents
{
    public record UpdatedIndustryEvent : IntegrationEvent
	{
		public virtual Industry? NewIndustry { get; set; }
		public virtual Industry? OldIndustry { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
	}
}

