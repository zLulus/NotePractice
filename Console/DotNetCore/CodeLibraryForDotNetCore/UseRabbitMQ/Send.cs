using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace CodeLibraryForDotNetCore.UseRabbitMQ
{
    public class Send
    {
        public static IConnection connection { get; set; }
        public static IModel channel { get; set; }

        public static void Run(string hostName, int port, string userName, string password)
        {
            var factory = new ConnectionFactory() { HostName = hostName, Port = port, UserName = userName, Password = password };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            Timer aTimer = new Timer(2000);
            int count = 1;
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (sender, e) =>
            {
                string message = $"Hello World:{count}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                routingKey: "hello",
                                basicProperties: null,
                                body: body);
                Console.WriteLine($"{DateTime.Now} [x] Sent {message}" );
                count++;
            };
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }
    }
}
