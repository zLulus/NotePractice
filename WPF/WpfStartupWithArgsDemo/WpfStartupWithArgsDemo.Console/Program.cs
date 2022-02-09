using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStartupWithArgsDemo.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //启动WpfStartupWithArgsDemo.exe
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\WpfStartupWithArgsDemo\\bin\\Debug\\WpfStartupWithArgsDemo.exe");
            if (File.Exists(exePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(exePath, string.Format("{0} {1} {2}", "FirstArg", exePath, 3))
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                Process.Start(startInfo);

                Environment.Exit(0);
            }
            else
                System.Console.WriteLine($"找不到文件{exePath}");
            System.Console.WriteLine($"按下任意按键结束程序");
            System.Console.ReadLine();
        }
    }
}
