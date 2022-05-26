namespace WebUI
{
	public class APICommunication
	{
		public FundamentalDataClient Client { get; set; }

        public APICommunication()
        {
			Client = new FundamentalDataClient("localhost", 60958, "api/");
        }

		public async Task<IEnumerable<Market>> GetMarketsForIndexView()
        {
			var marketsData = await Client.GetMarkets();

			if (marketsData == null)
				return null;

			return marketsData;
        }
	}
}

