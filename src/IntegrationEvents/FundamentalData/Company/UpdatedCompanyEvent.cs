namespace IntegrationEvents
{
	public record UpdatedCompanyEvent : IntegrationEvent
	{
		public virtual Company? NewCompany { get; set; }
		public virtual Company? OldCompany { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
		public virtual string? IndustryName { get; set; }
	}
}

