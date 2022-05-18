namespace IntegrationEvents
{
	public record DeletedFilingEvent : IntegrationEvent
	{
		public virtual DataStructures.Filing? OldFiling { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
		public virtual string? IndustryName { get; set; }
		public virtual string? CompanyTicker { get; set; }
	}
}

