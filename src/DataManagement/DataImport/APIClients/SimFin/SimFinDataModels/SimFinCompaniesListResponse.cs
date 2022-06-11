using System;
namespace DataImport.APIClients.SimFin
{
	public class SimFinCompaniesListResponse : SimFinBasic2dDataView
	{
        public IEnumerable<SimFinCompaniesListEntry> CompaniesList => data.Select(e => new SimFinCompaniesListEntry(e, this["SimFinId"], this["Ticker"]));
        public IEnumerable<string> Tickers => this[this["Ticker"]].Select(e => ((JsonElement)e).GetString());
	}
	public class SimFinCompaniesListEntry
    {
        public int SimFinId { get; set; }
        public string Ticker { get; set; }

        public SimFinCompaniesListEntry(IList<object> dataPoints, int SimFinIdIndex, int TickerIndex)
        {
            SimFinId = ((JsonElement)dataPoints[SimFinIdIndex]).GetInt32();
            Ticker = ((JsonElement)dataPoints[TickerIndex]).GetString();
        }
    }
}

