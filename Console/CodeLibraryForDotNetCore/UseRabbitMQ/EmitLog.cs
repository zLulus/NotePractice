using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseRabbitMQ
{
    public class EmitLog
    {
        private static int count = 0;
        public static void Run(string hostName, int port, string userName, string password)
        {
            var factory = new ConnectionFactory() { HostName = hostName, Port = port, UserName = userName, Password = password };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                var message = GetMessage();
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        private static string GetMessage()
        {
            return $"info: Hello World:{count}";
        }
    }
}
