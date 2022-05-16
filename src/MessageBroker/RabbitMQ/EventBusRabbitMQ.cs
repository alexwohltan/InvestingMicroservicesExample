using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace MessageBroker.RabbitMQ
{
    public class EventBusRabbitMQ : CommonRabbitMQ, IEventBus, IDisposable
    {
        List<Subscription> Subscribers { get; set; }

        public EventBusRabbitMQ()
            : base()
        {
            Configure();
        }
        public EventBusRabbitMQ(string hostname, int port, string user, string password)
            : base(hostname, port, user, password)
        {
            Configure();
        }

        public void Configure()
        {
            ExchangeName = "InvestingMicroservicesEventBus";
            Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);
            Subscribers = new List<Subscription>();
        }

        public void Publish<T>(T @event)
            where T : IntegrationEvent
        {
            var eventName = @event.GetType().Name;

            //string json = JsonConvert.SerializeObject(@event);
            string json = System.Text.Json.JsonSerializer.Serialize(@event);

            var body = Encoding.UTF8.GetBytes(json);

            Channel.BasicPublish(exchange: ExchangeName,
                                 routingKey: eventName,
                                 basicProperties: null,
                                 body: body);

            Debug.WriteLine(" [x] Sent '{0}' : '{1}' : '{2}'", ExchangeName, eventName, @event);
        }

        public void Subscribe<T, TH>(TH handler)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            var queueName = Channel.QueueDeclare().QueueName;

            Channel.QueueBind(queue: queueName,
                exchange: ExchangeName,
                routingKey: eventName);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var routingKey = ea.RoutingKey;

                Debug.WriteLine(" [x] Received '{0}' : '{1}' : '{2}'",
                    ExchangeName,
                    routingKey,
                    message);

                T eventObject = (T)System.Text.Json.JsonSerializer.Deserialize<T>(message);

                //var eventObject1 = JsonConvert.DeserializeObject<T>(message);

                //T eventObject2 = System.Text.Json.JsonSerializer.Deserialize<T>(message);

                //Debug.WriteLine(eventObject);
                //Debug.WriteLine(eventObject2);
                //Debug.WriteLine(eventObject1);

                //var handlerType = typeof(TH).MakeGenericType(typeof(T));
                //var handler = Activator.CreateInstance(handlerType);
                //await (Task)handlerType.GetMethod("Handle").Invoke(handler, new object[] { eventObject });

                handler.Handle(eventObject);

                Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            string consumerTag = Channel.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer);

            Subscribers.Add(new Subscription()
            {
                ConsumerTag = consumerTag,
                Handler = handler,
                EventName = eventName
            });

            Debug.WriteLine(" Subscribed {0} to {1}.", typeof(TH).Name, typeof(T).Name);
        }

        public void SubscribeDynamic<TH>(string eventName, TH handler) where TH : IDynamicIntegrationEventHandler
        {
            var queueName = Channel.QueueDeclare().QueueName;

            Channel.QueueBind(queue: queueName,
                exchange: ExchangeName,
                routingKey: eventName);

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

                handler.Handle(message);

                Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            string consumerTag = Channel.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer);

            Subscribers.Add(new Subscription()
            {
                ConsumerTag = consumerTag,
                Handler = handler,
                EventName = eventName
            });

            Debug.WriteLine(" Subscribed {0} to {1}.", typeof(TH).Name, eventName);
        }

        public void Unsubscribe<T, TH>(TH handler)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            var subscriptions = Subscribers.Where(e => e.EventName == typeof(T).Name && e.Handler.Equals(handler));

            foreach (var subscription in subscriptions)
            {
                Channel.BasicCancel(subscription.ConsumerTag);
                Subscribers.Remove(subscription);
            }
        }

        public void UnsubscribeDynamic<TH>(string eventName, TH handler) where TH : IDynamicIntegrationEventHandler
        {
            var subscriptions = Subscribers.Where(e => e.EventName == eventName && e.Handler.Equals(handler));

            foreach (var subscription in subscriptions)
            {
                Channel.BasicCancel(subscription.ConsumerTag);
                Subscribers.Remove(subscription);
            }
        }

        public new void Dispose()
        {
            Subscribers.Clear();
            Subscribers = null;

            base.Dispose();
        }
    }

    internal class Subscription
    {
        public string ConsumerTag { get; set; }
        public IIntegrationEventHandler Handler { get; set; }
        public string EventName { get; set; }
    }
}
