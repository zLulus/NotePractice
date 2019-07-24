using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CodeLibraryForDotNetCore.IPAddresses
{
    public static class IPAddressTool
    {
        public static void GetIPAddressList()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine(ipa.ToString());
            }
        }
    }
}
