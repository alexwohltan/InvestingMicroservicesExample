namespace IntegrationEvents
{
    public record NewCompanyEvent : IntegrationEvent
	{
		public virtual Company? NewCompany { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
		public virtual string? IndustryName { get; set; }
	}
}

