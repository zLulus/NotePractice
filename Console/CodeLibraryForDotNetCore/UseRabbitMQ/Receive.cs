using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseRabbitMQ
{
    public class Receive
    {
        public static IConnection connection { get; set; }
        public static EventingBasicConsumer consumer { get; set; }
        public static void Run(string ipaddress, int port,string userName,string password)
        {
            var factory = new ConnectionFactory() { HostName = ipaddress,Port=port, UserName = userName, Password = password };
            using (connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                if (consumer == null)
                {
                    consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: "hello",
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
        }
    }
}
