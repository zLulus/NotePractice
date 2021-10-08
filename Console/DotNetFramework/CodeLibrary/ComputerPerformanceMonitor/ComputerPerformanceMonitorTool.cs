using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.ComputerPerformanceMonitor
{
    /// <summary>
    /// https://www.cnblogs.com/RainFate/p/11518412.html
    /// https://blog.csdn.net/WZh0316/article/details/100118036
    /// </summary>
    public class ComputerPerformanceMonitorTool
    {
        public static void Run()
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;
            cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("电脑CPU使用率：" + cpuCounter.NextValue() + " %");
                Console.WriteLine("电脑可使用内存：" + ramCounter.NextValue() + "MB");
                Console.WriteLine();

                if ((int)cpuCounter.NextValue() > 80)
                {
                    System.Threading.Thread.Sleep(1000 * 60);
                }
            }

            //Console.WriteLine($"物理内存:{GetPhysicalMemory()}");
            //Console.WriteLine($"可用内存:{GetAvailableMemory()}");
        }

        /// <summary>
        /// 物理内存
        /// </summary>
        /// <returns></returns>
        public static double GetPhysicalMemory()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    return double.Parse(mo["TotalPhysicalMemory"].ToString()) / 1024.0 / 1024.0 / 1024.0;
                }
            }

            return 0;
        }

        /// <summary>
        /// 可用内存
        /// </summary>
        /// <returns></returns>
        public static double GetAvailableMemory()
        {
            double availablebytes = 0;
            try
            {
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = double.Parse(mo["FreePhysicalMemory"].ToString()) / 1024.0 / 1024.0;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(new StringBuilder("获取可用内存失败：").Append(ex.Message).ToString());
            }

            return availablebytes;
        }
    }
}
