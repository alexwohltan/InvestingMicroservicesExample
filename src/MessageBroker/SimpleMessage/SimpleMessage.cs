using System;
namespace MessageBroker
{
    public class SimpleMessage
    {
        public SimpleMessage()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public SimpleMessage(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
