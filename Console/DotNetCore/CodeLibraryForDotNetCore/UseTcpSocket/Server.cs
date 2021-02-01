using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.UseTcpSocket
{
    public class Server
    {
        private string ipaddress;
        private int port;
        public void Run(string ipaddress, int port)
        {
            this.ipaddress = ipaddress;
            this.port = port;
            Task.Run(async () =>
            {
                await Start();
            });
        }

        public async Task Start()
        {

            //创建套接字
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ipaddress), port);
            //也可以使用IPAddress.Any，监听所有网络接口上的客户端活动
            //https://docs.microsoft.com/en-us/dotnet/api/system.net.ipaddress.any?view=netframework-4.7.2
            //一般来说，服务端套接字都是直接bind端口，不会显式指明ip地址
            //否则切换了服务器还需要修改，或者也可以动态获取IP，但是没必要
            IPEndPoint ipe2 = new IPEndPoint(IPAddress.Any, port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //解决粘包问题
            socket.NoDelay = true;
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            //https://www.codeproject.com/Articles/117557/Set-Keep-Alive-Values
            //http://blog.stephencleary.com/2009/05/detection-of-half-open-dropped.html
            SetTcpKeepAlive(socket, 600000, 1000);
            //绑定端口和IP
            socket.Bind(ipe);
            //设置监听数
            socket.Listen(10);
            //连接客户端
            await AsyncAccept(socket);
        }

        public static void SetTcpKeepAlive(Socket socket, uint keepaliveTime, uint keepaliveInterval)
        {
            /* the native structure
            struct tcp_keepalive {
            ULONG onoff;
            ULONG keepalivetime;
            ULONG keepaliveinterval;
            };
            */

            // marshal the equivalent of the native structure into a byte array
            uint dummy = 0;
            byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            BitConverter.GetBytes((uint)(keepaliveTime)).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)keepaliveTime).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            BitConverter.GetBytes((uint)keepaliveInterval).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);

            // write SIO_VALS to Socket IOControl
            socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }

        /// <summary>
        /// check socket是否正常连接
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool CheckSocketConnected(Socket s)
        {
            //https://stackoverflow.com/questions/2661764/how-to-check-if-a-socket-is-connected-disconnected-in-c
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 接收client连接
        /// </summary>
        /// <param name="socket"></param>
        private async Task AsyncAccept(Socket socket)
        {
            socket.BeginAccept(async asyncResult => 
            {
                try
                {
                    //获取客户端套接字
                    Socket client = socket.EndAccept(asyncResult);
                    Console.WriteLine(string.Format("客户端{0}请求连接...", client.RemoteEndPoint));
                    await AsyncSend(client, "服务器收到连接请求");
                    await AsyncSend(client, string.Format("欢迎你{0}", client.RemoteEndPoint));
                    await AsyncReveive(client);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    //递归
                    await AsyncAccept(socket);
                }
               

                
            }, null);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="client"></param>
        private async Task AsyncReveive(Socket socket)
        {
            byte[] data = new byte[1024];
            try
            {
                //bool isHaveData = false;
                //for(int i = 0; i < data.Length; i++)
                //{
                //    if (data[i] != 0)
                //    {
                //        isHaveData = true;
                //        break;
                //    }
                //}
                //if (isHaveData)
                //{
                    //开始接收消息
                    socket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                    async asyncResult =>
                    {
                        int length = socket.EndReceive(asyncResult);
                        Console.WriteLine(string.Format("客户端发送消息:{0}", Encoding.UTF8.GetString(data)));

                    }, null);
                //}
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //递归
                //如果客户端已经断开，则不递归
                if (socket != null && CheckSocketConnected(socket))
                {
                    await AsyncReveive(socket);
                }
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="p"></param>
        private async Task AsyncSend(Socket client, string p)
        {
            if (client == null || p == string.Empty) return;
            //数据转码
            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes(p);
            try
            {
                //开始发送消息
                client.BeginSend(data, 0, data.Length, SocketFlags.None,async asyncResult =>
                {
                    //完成消息发送
                    int length = client.EndSend(asyncResult);
                    //输出消息
                    Console.WriteLine(string.Format("服务器发出消息:{0}", p));
                }, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal async Task AsyncSend(Socket socket, byte[] data)
        {
            //开始发送消息
            //解决粘包的问题
            byte[] newData = new byte[1024];
            for (int i = 0; i < data.Length; i++)
            {
                newData[i] = data[i];
            }
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
            {
                try
                {
                    //完成消息发送
                    int length = socket.EndSend(asyncResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }, null);

        }
    }
}
