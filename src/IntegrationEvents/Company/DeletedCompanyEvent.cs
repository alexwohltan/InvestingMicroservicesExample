namespace IntegrationEvents
{
	public record DeletedCompanyEvent : IntegrationEvent
	{
		public virtual DataStructures.Company? OldCompany { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
		public virtual string? IndustryName { get; set; }
	}
}

