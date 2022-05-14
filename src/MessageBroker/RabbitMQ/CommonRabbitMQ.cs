using System;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMQ
{
    public class CommonRabbitMQ : IDisposable
    {
        public string HostName { get; set; }
        public int Port { get; set; }

        public string UserName { get; set; }
        protected string Password { get; set; }

        public ConnectionFactory Factory { get; set; }
        public IConnection Connection { get; set; }
        public IModel Channel { get; set; }

        public string ExchangeName { get; set; }

        public CommonRabbitMQ()
            : this("localhost", 5672, "alexander", "EukalyptusHantel35!")
        {

        }
        public CommonRabbitMQ(string hostname, int port, string user, string password)
        {
            HostName = hostname;
            Port = port;

            UserName = user;
            Password = password;

            Factory = new ConnectionFactory()
            {
                HostName = HostName,
                Port = Port,
                UserName = UserName,
                Password = Password
            };

            Connection = Factory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public virtual void Dispose()
        {
            Channel.Close();
            Connection.Close();
        }
    }
}
