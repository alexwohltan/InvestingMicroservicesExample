using System;
using DataStructures;

namespace BulkImportData
{
	public class MarketTrimmed
	{
        public int ID { get; set; }
        public string Name { get; set; }
	}

	public static class MarketExtension
    {
        public static MarketTrimmed ToJson(this Market market)
        {
            return new MarketTrimmed { Name = market.Name, ID = market.ID };
        }
    }
}

