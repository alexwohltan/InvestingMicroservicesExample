using System;
using System.Text.Json.Serialization;

namespace MessageBroker
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public override string ToString()
        {
            return String.Format("Integration Event (GUID={0}, Created At {1} {2}", Id, CreationDate.ToShortDateString(), CreationDate.ToShortTimeString());
        }
    }
}
