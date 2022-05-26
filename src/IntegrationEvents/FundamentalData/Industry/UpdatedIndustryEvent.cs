namespace IntegrationEvents
{
    public record UpdatedIndustryEvent : IntegrationEvent
	{
		public virtual DataStructures.Industry? NewIndustry { get; set; }
		public virtual DataStructures.Industry? OldIndustry { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
	}
}

