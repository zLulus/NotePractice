using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ConnectLinuxTest
{
    public static class ConnectLinuxTestDemo
    {
        public static async Task Run()
        {
            string publicIP = "";
            int publicPort = 22;
            string userName = "root";
            string password = "";
            string command = "your command";
            using (SshClient client = new SshClient(publicIP, publicPort, userName, password))
            {
                client.Connect();

                SshCommand sshCommand = client.CreateCommand(command);


                IAsyncResult asyncResult = sshCommand.BeginExecute();

                while (!asyncResult.IsCompleted)
                    await Task.Delay(10);

                string result;

                if (sshCommand.ExitStatus == 0)
                    result = sshCommand.Result;
                else
                    result = sshCommand.Error;

                Console.WriteLine($"{sshCommand.ExitStatus}:");
                Console.WriteLine($"{result}");
            }
        }
    }
}
