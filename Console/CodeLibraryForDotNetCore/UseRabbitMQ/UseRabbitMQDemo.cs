using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseRabbitMQ
{
    public class UseRabbitMQDemo
    {
        public static void Run()
        {
            string ipaddress = "localhost";
            int port = 5673;
            string userName = "guest";
            string password = "guest";
            Receive.Run(ipaddress, port, userName,password);
            Send.Run(ipaddress,port, userName, password);
        }
    }
}
