using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.ConcurrentCollection
{
    public class ConcurrentCollectionDemo
    {
        /// <summary>
        /// 无序元素集合的线程安全实现
        /// </summary>
        public static void ConcurrentBagDemo()
        {
            var random = new Random();
            var totalCount = 100;
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            Parallel.For(0, totalCount*10, item =>
            {
                if(bag.Count<= totalCount)
                {
                    bag.Add(item);
                }
            });
            //有时会超过100个
            Console.WriteLine("ConcurrentBag's count is {0}", bag.Count());
            int n = 0;
            foreach (int i in bag)
            {
                if (n > 10)
                    break;
                n++;
                //输出结果随机，因为是并行的
                Console.WriteLine("Item[{0}] = {1}", n, i);
            }
            Console.WriteLine("ConcurrentBag's max item is {0}", bag.Max());
        }

        /// <summary>
        /// 线程安全队列 先进先出 (FIFO) 
        /// </summary>
        public static void ConcurrentQueueDemo()
        {
            // Construct a ConcurrentQueue.
            ConcurrentQueue<int> cq = new ConcurrentQueue<int>();

            // Populate the queue.
            //按顺序排队
            for (int i = 0; i < 10000; i++)
            {
                cq.Enqueue(i);
            }

            // Peek at the first element.
            //查看第一个元素
            int result;
            if (!cq.TryPeek(out result))
            {
                Console.WriteLine("CQ: TryPeek failed when it should have succeeded");
            }
            else if (result != 0)
            {
                Console.WriteLine("CQ: Expected TryPeek result of 0, got {0}", result);
            }

            int outerSum = 0;
            // An action to consume the ConcurrentQueue.
            //消耗ConcurrentQueue的操作
            Action action = () =>
            {
                int localSum = 0;
                int localValue;
                //取消排队
                while (cq.TryDequeue(out localValue)) 
                    localSum += localValue;
                //Interlocked为多个线程共享的变量提供原子操作
                //作为原子运算，将两个32位整数相加并将和赋值给第一个整数
                Interlocked.Add(ref outerSum, localSum);
            };

            // Start 4 concurrent consuming actions.
            //4个并发消费操作
            Parallel.Invoke(action, action, action, action);

            Console.WriteLine("outerSum = {0}, should be 49995000", outerSum);
        }

        /// <summary>
        /// 线程安全的堆栈 后进先出 (LIFO) 集合
        /// </summary>
        public static async Task ConcurrentStackDemo()
        {
            int items = 10000;

            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            // Create an action to push items onto the stack
            //将数据推入堆栈
            Action pusher = () =>
            {
                for (int i = 0; i < items; i++)
                {
                    stack.Push(i);
                }
            };

            // Run the action once
            pusher();

            //查看栈顶数据
            if (stack.TryPeek(out int result))
            {
                Console.WriteLine($"TryPeek() saw {result} on top of the stack.");
            }
            else
            {
                Console.WriteLine("Could not peek most recently added number.");
            }

            // Empty the stack
            //清空栈
            stack.Clear();

            if (stack.IsEmpty)
            {
                Console.WriteLine("Cleared the stack.");
            }

            // Create an action to push and pop items
            //将数据推入/弹出栈中
            Action pushAndPop = () =>
            {
                Console.WriteLine($"Task started on {Task.CurrentId}");

                int item;
                for (int i = 0; i < items; i++)
                    stack.Push(i);
                for (int i = 0; i < items; i++)
                    stack.TryPop(out item);

                Console.WriteLine($"Task ended on {Task.CurrentId}");
            };

            // Spin up five concurrent tasks of the action
            //5个并发任务
            var tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
                tasks[i] = Task.Factory.StartNew(pushAndPop);

            // Wait for all the tasks to finish up
            //等待所有任务完成
            await Task.WhenAll(tasks);

            //检查堆栈是否清空
            if (!stack.IsEmpty)
            {
                Console.WriteLine("Did not take all the items off the stack");
            }
        }

        /// <summary>
        /// 由多个线程同时访问的键/值对的线程安全集合
        /// </summary>
        public static void ConcurrentDictionaryDemo()
        {
            // We know how many items we want to insert into the ConcurrentDictionary.
            //我们知道要在ConcurrentDictionary中插入多少个数据
            // So set the initial capacity to some prime number above that, to ensure that
            // the ConcurrentDictionary does not need to be resized while initializing it.
            //因此，请将初始容量设置为高于该容量的质数，以确保初始化时无需调整ConcurrentDictionary的大小。
            //预计容量
            int NUMITEMS = 64;
            int initialCapacity = 101;

            // The higher the concurrencyLevel, the higher the theoretical number of operations
            // that could be performed concurrently on the ConcurrentDictionary.  However, global
            // operations like resizing the dictionary take longer as the concurrencyLevel rises. 
            // For the purposes of this example, we'll compromise at numCores * 2.
            //并发级别越高，理论上的操作数就越高
            //但是，随着concurrencyLevel的提高，调整字典大小等全局操作会花费更长的时间
            //出于本示例的目的，我们将以numCores * 2为代价
            int numProcs = Environment.ProcessorCount;
            //预计线程数
            int concurrencyLevel = numProcs * 2;

            // Construct the dictionary with the desired concurrencyLevel and initialCapacity
            //使用所需的concurrencyLevel和initialCapacity构造字典
            ConcurrentDictionary<int, int> cd = new ConcurrentDictionary<int, int>(concurrencyLevel, initialCapacity);

            // Initialize the dictionary
            //初始化字典
            for (int i = 0; i < NUMITEMS; i++) 
                cd[i] = i * i;

            Console.WriteLine("The square of 23 is {0} (should be {1})", cd[23], 23 * 23);
        }
    }
}
