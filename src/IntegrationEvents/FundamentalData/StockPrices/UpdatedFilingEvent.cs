namespace IntegrationEvents
{
	public record UpdatedStockPricesEvent : IntegrationEvent
	{
		public virtual IEnumerable<StockPrice>? NewPrices { get; set; }
		public virtual string? MarketName { get; set; }
		public virtual string? SectorName { get; set; }
		public virtual string? IndustryName { get; set; }
		public virtual string? CompanyTicker { get; set; }
	}
}

