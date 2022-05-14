using System;
using DataStructures;
using IntegrationEvents;
using MessageBroker;
using MessageBroker.RabbitMQ;

namespace BulkImportData // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wait for FundamentalData API to load. After loading, press Enter");
            Thread.Sleep(3000);

            BulkImportData.SimFinHandler handler = new BulkImportData.SimFinHandler();
            var markets = handler.RetrieveMarkets();

            UploadMarkets(markets);

            Console.WriteLine("done");
        }

        static async void UploadMarkets(List<Market> markets)
        {
            HttpClient client = new HttpClient();

            foreach (var market in markets)
            {
                UploadMarket(market, client);
            }
        }

        static async void UploadMarket(Market market, HttpClient client)
        {
            IEventBus bus = new EventBusRabbitMQ();

            bus.Publish(new NewMarketEvent { NewMarket = market });
        }
    }
}