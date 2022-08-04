using CodeLibrary;
using CodeLibrary.BitmapConvert;
using CodeLibrary.BitmapWithMultiTheads;
using CodeLibrary.CancleRequest;
using CodeLibrary.ComputerPerformanceMonitor;
using CodeLibrary.ConnectOracle;
using CodeLibrary.CSharpUsingPython;
using CodeLibrary.DateTimeTest;
using CodeLibrary.DeleteFileInUse;
using CodeLibrary.ExcuteJs;
using CodeLibrary.ExcuteJsByPhantomjs;
using CodeLibrary.HexAndBytes;
using CodeLibrary.IPAddresses;
using CodeLibrary.ReadMdbFiles;
using CodeLibrary.SendEmail;
using CodeLibrary.SimulateMouseAndKeyboardEvent;
using CodeLibrary.SpoofIpAddress;
using CodeLibrary.UsePostgresql;
using CodeLibrary.UsePostgresql.Enums;
using CodeLibrary.UsePostgresql.Models;
using CodeLibrary.UseRabbitMQ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnumTest();

            //在正则表达式中给取得的值命名
            //RegexDemo.Demo();

            //关闭之前进行一些操作
            //handler = new ConsoleEventDelegate(ConsoleEventCallback);
            //SetConsoleCtrlHandler(handler, true);
            //Console.ReadLine();

            //C# 元组、匿名对象、ref&out
            //ReturnsMultipleValuesTest test = new ReturnsMultipleValuesTest();
            //test.Test();

            //C#调用js Jint库
            //ExcuteJsDemo.ExcuteJs();

            //C#调用js phantomjs
            //ExcuteJsByPhantomjsDemo.ExcuteJs();

            //i++,++i
            //TestCycle();

            //Aggregate
            //TestAggregate();

            //C#调用Python
            //保证已安装任意版本的python，并将其添加到环境变量(或者拷贝python.exe至bin目录根目录)
            //CSharpUsingPythonDemo.ExcutePython();

            //Postgresql测试
            //Task.Run(async () =>
            //{
            //    await UsePostgresqlDemo.PostgresqlTest();
            //});

            //send email
            //SendEmailDemo.Run();

            //Spoof Ip Address
            //string url = "";
            //for (int i = 0; i < 100; i++)
            //{
            //    SpoofIpAddressDemo.get(url);
            //    Thread.Sleep(500);
            //    Console.WriteLine(i);
            //}

            //Spoof Ip Address 3
            //免费代理IP:
            //需要翻墙：https://free-proxy-list.net/
            //http://ip.zdaye.com/dayProxy/ip/220993.html
            //string ip = "";
            //int port = 80;
            //string url = "";
            ////验证代理IP地址是否可用
            //SpoofIpAddressDemo3.ChecKedForIP((result) => { Console.WriteLine($"ChecKedForIP:{result}"); }, ip, port);
            ////在请求时设置代理
            //SpoofIpAddressDemo3.SpoofIpAddressBySetProxyWhileRequest(ip, port,url);
            ////全局设置代理
            //SpoofIpAddressDemo3.SpoofIpAddressBySetProxyForSystem(ip, port,url);

            //模拟键盘、鼠标操作
            //SimulateMouseAndKeyboardEventDemo.Run();

            //网络延迟，取消请求
            //CancleRequestDemo.CancleRequestByTimeout("http://localhost:1107");
            //CancleRequestDemo.CancleRequestByTask("http://localhost:1107");

            ////RabbitMQ Receiver
            //var host = "localhost";
            //var port = 5673;
            //var userName = "guest";
            //var password = "guest";
            ////Receive.Run(host, port, userName, password);
            ////Worker.Run(host, port, userName, password);
            //ReceiveLogs.Run(host, port, userName, password);

            //获得IPAddress列表
            //IPAddressTool.GetIPAddressList();

            //读取mdb文件
            //ReadMdbFileDemo.Run();

            //连接oracle数据库
            //ConnectOracleDemo.Run();

            //byte[] and number
            //HexAndBytesDemo.Run();

            //电脑性能监控
            //ComputerPerformanceMonitorTool.Run();

            //删除正在被使用的文件
            //DeleteFileInUseDemo.Run();

            //BitmapConvertDemo.Run();

            //DateTimeTestDemo.Run();

            BitmapWithMultiTheadsTestDemo.Run();

            Console.ReadLine();
        }

        private static void TestAggregate()
        {
            var list = Enumerable.Range(1, 100);
            var result = list.Aggregate((a, b) => (a + b));
            Console.WriteLine($"1到100的和为{result}");
            var nums = Enumerable.Range(2, 4);
            var sum = nums.Aggregate(1, (a, b) => a * b);
            Console.WriteLine($"2到5的积为{sum}");
        }

        private static void TestCycle()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine(i);
            }
            Console.Read();
        }

        #region 关闭之前进行一些操作
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                //这是关闭之前
                Console.WriteLine("Console window closing, death imminent");
            }
            return false;
        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
                                               // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        #endregion

        private static void EnumTest()
        {
            int i = 0;
            Console.WriteLine(i);
            Console.WriteLine(i.ToString());
            Console.WriteLine();

            Student s = new Student() {Name = "YYY"};
            Console.WriteLine(s);
            Console.WriteLine(s.ToString());
            Console.WriteLine();

            Console.WriteLine(Days.Friday);
            Console.WriteLine(Days.Friday.ToString());
            Console.WriteLine((int) Days.Friday);

            Console.Read();
        }
    }

    public enum Days
    {
        Sunday, Monday, Tuesday, Wednesday, Thursday, Friday=2, Saturday
    }

    public class Student
    {
        public string Name { get; set; }
    }
}
