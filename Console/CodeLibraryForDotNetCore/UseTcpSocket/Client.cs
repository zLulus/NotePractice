using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.UseTcpSocket
{
    public class Client
    {
        private string ipaddress;
        private int port;
        public void Run(string ipaddress, int port)
        {
            this.ipaddress = ipaddress;
            this.port = port;
            Task.Run(async () =>
            {
                await AsynConnect();
            });
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        public async Task AsynConnect()
        {
            
            //端口及IP
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ipaddress), port);
            //创建套接字
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //开始连接到服务器
            client.BeginConnect(ipe,async asyncResult =>
            {
                client.EndConnect(asyncResult);
                //向服务器发送消息
                await AsynSend(client, "你好我是客户端");
                await AsynSend(client, "第一条消息");
                await AsynSend(client, "第二条消息");
                //接受消息
                await AsynRecive(client);
            }, null);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="message"></param>
        public async Task AsynSend(Socket socket, string message)
        {
            if (socket == null || message == string.Empty) return;
            //编码
            byte[] data = Encoding.UTF8.GetBytes(message);
            try
            {
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, 
                async asyncResult =>
                {
                    //完成发送消息
                    int length = socket.EndSend(asyncResult);
                    Console.WriteLine(string.Format("客户端发送消息:{0}", message));
                }, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常信息：{0}", ex.Message);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="socket"></param>
        public async Task AsynRecive(Socket socket)
        {
            byte[] data = new byte[1024];
            try
            {
                //开始接收数据
                socket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                async asyncResult =>
                {
                    int length = socket.EndReceive(asyncResult);
                    Console.WriteLine(string.Format("收到服务器消息:{0}", Encoding.UTF8.GetString(data)));
                }, null);

            }
            catch (Exception ex)
            {
                Console.WriteLine("异常信息：", ex.Message);
            }
            finally
            {
                await AsynRecive(socket);
            }
        }

    }
}
