using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseRabbitMQ
{
    public class Send
    {
        public static IConnection connection  { get; set; }
        public static void Run(string ipaddress, int port, string userName, string password)
        {
            var factory = new ConnectionFactory() { HostName = ipaddress,Port=port,UserName= userName, Password= password };
            using (connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

        }
    }
}
