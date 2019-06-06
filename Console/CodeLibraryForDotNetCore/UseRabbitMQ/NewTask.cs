using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace CodeLibraryForDotNetCore.UseRabbitMQ
{
    public class NewTask
    {
        private static int count = 0;
        public static IConnection connection { get; set; }
        public static IModel channel { get; set; }
        public static void Run(string hostName, int port, string userName, string password)
        {
            var factory = new ConnectionFactory() { HostName = hostName, Port = port, UserName = userName, Password = password };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "task_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Timer aTimer = new Timer(2000);
            aTimer.Elapsed += (sender, e) =>
            {
                var message = GetMessage();
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: "task_queue",
                                     basicProperties: properties,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            };
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
           

        }

        private static string GetMessage()
        {
            count++;
            return $"Hello World:{count}";
        }
    }
}
