using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ReaderWriterLockTest
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.readerwriterlock?view=net-6.0&WT.mc_id=DT-MVP-5003010
    /// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Threading/src/System/Threading/ReaderWriterLock.cs
    /// </summary>
    public class ReaderWriterLockTestDemo
    {
        static ReaderWriterLock rwl = new ReaderWriterLock();
        // Define the shared resource protected by the ReaderWriterLock.
        static int resource = 0;

        const int numThreads = 26;
        static bool running = true;

        // Statistics.
        //统计
        static int readerTimeouts = 0;
        static int writerTimeouts = 0;
        static int reads = 0;
        static int writes = 0;

        public static void Run()
        {
            // Start a series of threads to randomly read from and
            // write to the shared resource.
            //开启一系列线程以随机读取和写入共享资源
            Thread[] t = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                t[i] = new Thread(new ThreadStart(ThreadProc));
                t[i].Name = new String(Convert.ToChar(i + 65), 1);
                t[i].Start();
                if (i > 10)
                    Thread.Sleep(300);
            }

            // Tell the threads to shut down and wait until they all finish.
            //停止运行，并等待线程结束任务
            running = false;
            for (int i = 0; i < numThreads; i++)
                t[i].Join();

            // Display statistics.
            //展示统计结果
            Console.WriteLine("\n{0} reads, {1} writes, {2} reader time-outs, {3} writer time-outs.",
                  reads, writes, readerTimeouts, writerTimeouts);
            Console.Write("Press ENTER to exit... ");
            Console.ReadLine();
        }

        static void ThreadProc()
        {
            Random rnd = new Random();

            // Randomly select a way for the thread to read and write from the shared
            // resource.
            //随机一种方式读取、写入共享资源
            while (running)
            {
                double action = rnd.NextDouble();
                if (action < .8)
                    ReadFromResource(10);
                else if (action < .81)
                    ReleaseAndRestore(rnd, 50);
                else if (action < .90)
                    UpgradeDowngrade(rnd, 100);
                else
                    WriteToResource(rnd, 100);
            }
        }

        /// <summary>
        /// Request and release a reader lock, and handle time-outs.
        /// 请求和释放读锁，并处理超时。
        /// </summary>
        /// <param name="timeOut"></param>
        static void ReadFromResource(int timeOut)
        {
            try
            {
                rwl.AcquireReaderLock(timeOut);
                try
                {
                    // It is safe for this thread to read from the shared resource.
                    //安全读取共享资源
                    Display("reads resource value " + resource);
                    Interlocked.Increment(ref reads);
                }
                finally
                {
                    // Ensure that the lock is released.
                    //确保锁被释放
                    rwl.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                // The reader lock request timed out.
                //读锁请求超时
                Interlocked.Increment(ref readerTimeouts);
            }
        }

        /// <summary>
        /// Request and release the writer lock, and handle time-outs.
        /// 请求和释放写锁，并处理超时。
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="timeOut"></param>
        static void WriteToResource(Random rnd, int timeOut)
        {
            try
            {
                rwl.AcquireWriterLock(timeOut);
                try
                {
                    // It's safe for this thread to access from the shared resource.
                    //安全访问，修改共享资源
                    resource = rnd.Next(500);
                    Display("writes resource value " + resource);
                    Interlocked.Increment(ref writes);
                }
                finally
                {
                    // Ensure that the lock is released.
                    rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
                // The writer lock request timed out.
                Interlocked.Increment(ref writerTimeouts);
            }
        }

        /// <summary>
        /// Requests a reader lock, upgrades the reader lock to the writer lock, and downgrades it to a reader lock again.
        /// 请求读锁，将读锁升级为写锁，并再次将其降级为读锁。
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="timeOut"></param>
        static void UpgradeDowngrade(Random rnd, int timeOut)
        {
            try
            {
                rwl.AcquireReaderLock(timeOut);
                try
                {
                    // It's safe for this thread to read from the shared resource.
                    Display("reads resource value " + resource);
                    Interlocked.Increment(ref reads);

                    // To write to the resource, either release the reader lock and
                    // request the writer lock, or upgrade the reader lock. Upgrading
                    // the reader lock puts the thread in the write queue, behind any
                    // other threads that might be waiting for the writer lock.
                    //要写入资源，需要释放读锁并请求写入器锁，或升级读取器锁。
                    //升级读锁将线程放入写入队列中，排在其他正在等待写锁的线程后。
                    try
                    {
                        //读锁升级写锁
                        LockCookie lc = rwl.UpgradeToWriterLock(timeOut);
                        try
                        {
                            // It's safe for this thread to read or write from the shared resource.
                            resource = rnd.Next(500);
                            Display("writes resource value " + resource);
                            Interlocked.Increment(ref writes);
                        }
                        finally
                        {
                            // Ensure that the lock is released.
                            //写锁降级读锁，在UpgradeToWriterLock方法后调用
                            rwl.DowngradeFromWriterLock(ref lc);
                        }
                    }
                    catch (ApplicationException)
                    {
                        // The upgrade request timed out.
                        Interlocked.Increment(ref writerTimeouts);
                    }

                    // If the lock was downgraded, it's still safe to read from the resource.
                    //降级后，依然可以安全读
                    Display("reads resource value " + resource);
                    Interlocked.Increment(ref reads);
                }
                finally
                {
                    // Ensure that the lock is released.
                    rwl.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                // The reader lock request timed out.
                Interlocked.Increment(ref readerTimeouts);
            }
        }

        /// <summary>
        /// Release all locks and later restores the lock state.
        /// Uses sequence numbers to determine whether another thread has
        /// obtained a writer lock since this thread last accessed the resource.
        /// 释放所有锁，稍后恢复锁状态。
        /// 使用序列号来确定另一个线程是否获得了写锁，自该线程上次访问该资源以来。
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="timeOut"></param>
        static void ReleaseAndRestore(Random rnd, int timeOut)
        {
            int lastWriter;

            try
            {
                rwl.AcquireReaderLock(timeOut);
                try
                {
                    // It's safe for this thread to read from the shared resource,
                    // so read and cache the resource value.
                    int resourceValue = resource;     // Cache the resource value.
                    Display("reads resource value " + resourceValue);
                    Interlocked.Increment(ref reads);

                    // Save the current writer sequence number.
                    lastWriter = rwl.WriterSeqNum;

                    // Release the lock and save a cookie so the lock can be restored later.
                    //释放锁，保存cookie
                    LockCookie lc = rwl.ReleaseLock();

                    // Wait for a random interval and then restore the previous state of the lock.
                    Thread.Sleep(rnd.Next(250));
                    //根据cookie恢复锁
                    rwl.RestoreLock(ref lc);

                    // Check whether other threads obtained the writer lock in the interval.
                    // If not, then the cached value of the resource is still valid.
                    //检查这个间隙时间是否有其他线程获得了写锁
                    //如果没有，缓存数据依然有效
                    if (rwl.AnyWritersSince(lastWriter))
                    {
                        resourceValue = resource;
                        Interlocked.Increment(ref reads);
                        Display("resource has changed " + resourceValue);
                    }
                    else
                    {
                        Display("resource has not changed " + resourceValue);
                    }
                }
                finally
                {
                    // Ensure that the lock is released.
                    rwl.ReleaseReaderLock();
                }
            }
            catch (ApplicationException)
            {
                // The reader lock request timed out.
                Interlocked.Increment(ref readerTimeouts);
            }
        }

        // Helper method briefly displays the most recent thread action.
        static void Display(string msg)
        {
            Console.Write("Thread {0} {1}.       \r", Thread.CurrentThread.Name, msg);
        }
    }
}
