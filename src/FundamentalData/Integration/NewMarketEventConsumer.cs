using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IntegrationEvents;
using MessageBroker;

namespace FundamentalData.Integration
{
	public class NewMarketEventConsumer : IIntegrationEventHandler<NewMarketEvent>
	{
        private readonly IEventBus _eventBus;
		public NewMarketEventConsumer(IEventBus eventBus)
		{
            _eventBus = eventBus;
		}

        public Task Handle(NewMarketEvent @event)
        {
            Debug.WriteLine(String.Format("Received new Market. (Name = {0})", @event.NewMarket.Name));

            using (FundamentalDataContext context = new FundamentalDataContext())
            {
                if(context.Markets.FirstOrDefault(e => e.Name == @event.NewMarket.Name) == null)
                {
                    context.Markets.Add(@event.NewMarket);
                }
            }

            throw new NotImplementedException();
        }
    }
}

