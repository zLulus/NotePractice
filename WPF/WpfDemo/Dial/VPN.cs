using DotRas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Dial
{
    public class VPN
    {
        private string serverIP;
        private string adapterName;
        private string userName;
        private string passWord;
        private string vpnProtocol;
        private string preSharedKey;
        public VPN(string serverIP = "", string adapterName = "", string userName = "", string passWord = "", string vpnProtocol = "", string preSharedKey = "")
        {
            setParameters(serverIP, adapterName, userName, passWord, vpnProtocol, preSharedKey);
        }

        public void setParameters(string serverIP, string adapterName, string userName, string passWord, string vpnProtocol, string preSharedKey)
        {
            setServerIP(serverIP);
            setAdapterName(adapterName);
            setUserName(userName);
            setPassWord(passWord);
            setVPNProtocol(vpnProtocol);
            setPreSharedKey(preSharedKey);
        }

        public void setServerIP(string serverIP)
        {
            this.serverIP = serverIP;
        }

        public void setAdapterName(string adapterName)
        {
            this.adapterName = adapterName;
        }

        public void setUserName(string userName)
        {
            this.userName = userName;
        }

        public void setPassWord(string passWord)
        {
            this.passWord = passWord;
        }

        public void setVPNProtocol(string vpnProtocol)
        {
            this.vpnProtocol = vpnProtocol;
        }

        public void setPreSharedKey(string preSharedKey)
        {
            this.preSharedKey = preSharedKey;
        }

        private RasDialer dialer { get; set; }
        private RasHandle handle { get; set; }
        public bool Connect()
        {
            if (dialer == null)
                dialer = new RasDialer();
            using (RasPhoneBook PhoneBook = new RasPhoneBook())
            {
                PhoneBook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers));
                RasEntry Entry;

                if (PhoneBook.Entries.Contains(adapterName))
                {
                    //如果正在连接删不掉
                    //try
                    //{
                    //    PhoneBook.Entries.Remove(adapterName);
                    //}
                    //catch(Exception ex)
                    //{

                    //}
                    Entry = PhoneBook.Entries.Where(x => x.Name == adapterName).FirstOrDefault();
                }
                else
                {
                    if (vpnProtocol.Equals("PPTP"))
                    {
                        Entry = RasEntry.CreateVpnEntry(adapterName, serverIP, RasVpnStrategy.PptpOnly, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));
                    }
                    else
                    {
                        Entry = RasEntry.CreateVpnEntry(adapterName, serverIP, RasVpnStrategy.L2tpOnly, RasDevice.GetDeviceByName("(L2TP)", RasDeviceType.Vpn));
                    }
                    PhoneBook.Entries.Add(Entry);
                }



                Entry.Options.PreviewDomain = false;
                Entry.Options.ShowDialingProgress = false;
                Entry.Options.PromoteAlternates = false;
                Entry.Options.DoNotNegotiateMultilink = false;

                if (vpnProtocol.Equals("L2TP"))
                {
                    Entry.Options.UsePreSharedKey = true;
                    Entry.UpdateCredentials(RasPreSharedKey.Client, preSharedKey);
                    Entry.Update();
                }

                dialer.EntryName = adapterName;
                dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                dialer.Credentials = new NetworkCredential(userName, passWord);


            }

            handle = dialer.DialAsync();
            if (handle.IsInvalid)
            {
                //失败
                return false;
            }
            return true;
        }

        public void Disconnect()
        {
            if (dialer == null)
                return;
            if (dialer.IsBusy)
            {
                dialer.DialAsyncCancel();
            }
            else
            {
                if (handle != null)
                {
                    RasConnection Connection = RasConnection.GetActiveConnectionByHandle(handle);
                    if (Connection != null)
                    {
                        Connection.HangUp();
                    }
                }
            }

            using (RasPhoneBook PhoneBook = new RasPhoneBook())
            {
                PhoneBook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers));

                if (PhoneBook.Entries.Contains(adapterName))
                {
                    PhoneBook.Entries.Remove(adapterName);
                }
            }
        }

        public void Dispose()
        {
            serverIP = null;
            adapterName = null;
            userName = null;
            passWord = null;
            vpnProtocol = null;
            preSharedKey = null;
            dialer = null;
            handle = null;
        }
    }
}
