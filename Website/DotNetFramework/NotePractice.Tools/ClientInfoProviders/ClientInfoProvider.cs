using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotePractice.Tools.ClientInfoProviders.Dtos;

namespace NotePractice.Tools.ClientInfoProviders
{
    public static class ClientInfoProvider
    {
        public static ClientInfo GetClientInfo()
        {
            ClientInfo clientInfo = new ClientInfo();
            clientInfo.ComputerName = System.Net.Dns.GetHostName();
            //clientIPAddress是一个数组，可能有多个数据
            var clientIPAddress = System.Net.Dns.GetHostAddresses(clientInfo.ComputerName);
            clientInfo.ClientIpAddress = clientIPAddress.GetValue(0).ToString();
            return clientInfo;
        }
    }
}
