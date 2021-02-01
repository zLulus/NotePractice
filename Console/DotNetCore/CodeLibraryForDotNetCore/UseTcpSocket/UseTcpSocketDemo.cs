using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseTcpSocket
{
    public class UseTcpSocketDemo
    {
        //https://blog.csdn.net/mss359681091/article/details/51790931
        public static void Run()
        {
            string ipaddress = "127.0.0.1";
            int port = 20006;
            new Server().Run(ipaddress, port);
            new Client().Run(ipaddress, port);
        }
    }
}
