using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ReaderWriterLockTest
{
    public class MonitorTestDemo
    {
        private static object locker = new object();

        static int resource = 0;

        const int numThreads = 50;
        static bool running = true;

        static int reads = 0;
        static int writes = 0;

        public static void Run()
        {
            Thread[] t = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                t[i] = new Thread(new ThreadStart(ThreadProc));
                t[i].Name = new String(Convert.ToChar(i + 65), 1);
                t[i].Start();
                if (i > 10)
                    Thread.Sleep(300);
            }

            running = false;
            for (int i = 0; i < numThreads; i++)
                t[i].Join();

            Console.WriteLine("\n{0} reads, {1} writes.",
                  reads, writes);
            Console.Write("Press ENTER to exit... ");
            Console.ReadLine();
        }

        private static void ThreadProc()
        {
            Random rnd = new Random();

            while (running)
            {
                double action = rnd.NextDouble();
                if (action < .3)
                    ReadFromResource(10);
                else if (action < .3001)
                    Stop(rnd, 50);
                else
                    WriteToResource(rnd, 100);
            }
        }

        private static void Stop(Random rnd, int timeout)
        {
            running = false;
        }

        private static void WriteToResource(Random rnd, int timeout)
        {
            try
            {
                if(Monitor.TryEnter(locker, timeout))
                {
                    resource = rnd.Next(500);
                    Display("writes resource value " + resource);
                    writes++;
                }
                else
                {
                    Display("can not writes resource value.");
                }
            }
            catch
            {

            }
            finally
            {
                if(Monitor.IsEntered(locker))
                    Monitor.Exit(locker);
            }
        }

        private static void ReadFromResource(int timeout)
        {
            Display("reads resource value " + resource);
            reads++;
        }

        static void Display(string msg)
        {
            Console.WriteLine("Thread {0} {1}.       \r", Thread.CurrentThread.Name, msg);
        }
    }
}
