using System;
using System.Diagnostics;
using System.Text;
using MessageBroker;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBroker.RabbitMQ
{
    /// <summary>
    /// Not fully implemented. Just for testing purposes.
    /// </summary>
    public class SimpleMessageRabbitMQ : CommonRabbitMQ, ISimpleMessageBus
    {
        public SimpleMessageRabbitMQ()
            : base()
        {
            ExchangeName = "SimpleMessageBus";
        }
        public SimpleMessageRabbitMQ(string hostname, int port, string user, string password)
            : base(hostname, port, user, password)
        {
            ExchangeName = "SimpleMessageBus";
        }

        public void Publish<T>(T message)
            where T : SimpleMessage
        {
            var messageName = message.GetType().Name;

            Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);

            var routingKey = messageName;

            string json = System.Text.Json.JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            Channel.BasicPublish(exchange: ExchangeName,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
            Debug.WriteLine(" [x] Sent '{0}' : '{1}' : '{2}'", ExchangeName, routingKey, body);

        }

        public void Subscribe<T, TH>(TH handler)
            where T : SimpleMessage
            where TH : ISimpleMessageHandler<T>
        {
            var messageName = typeof(T).Name;

            Channel.ExchangeDeclare(exchange: ExchangeName,
                type: ExchangeType.Topic);

            var queueName = Channel.QueueDeclare().QueueName;

            var bindingKey = typeof(T).Name;

            Channel.QueueBind(queue: queueName,
                exchange: ExchangeName,
                routingKey: bindingKey);



            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var routingKey = ea.RoutingKey;
                Debug.WriteLine(" [x] Received '{0}' : '{1}' : '{2}'",
                    ExchangeName,
                    routingKey,
                    message);

                Console.WriteLine(typeof(T).Name);

                handler.Handle((T)System.Text.Json.JsonSerializer.Deserialize(message, typeof(T)));
                Debug.WriteLine("Received in SimpleMessagerabbitMQ");
            };

            Channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

            Debug.WriteLine(" Listening for messages on '{0}' : '{1}'", ExchangeName, bindingKey);

        }

        public void Unsubscribe<T, TH>()
            where T : SimpleMessage
            where TH : ISimpleMessageHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
