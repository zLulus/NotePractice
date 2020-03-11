using CodeLibraryForDotNetCore.UseLock.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.UseLock
{
    public class UseLockDemo
    {
        internal int inStock;
        private readonly object inStockLock = new object();
        Random random = new Random();
        internal List<Customer> Customers { get; set; }
        public void Run()
        {
            var customerCount = 1000;
            var stock = 10;
            //假设有商品库存为10
            Console.WriteLine($"假设有商品库存为10");
            //顾客100名进行抢购
            Customers = new List<Customer>();
            for (int i = 0; i < customerCount; i++)
            {
                Customers.Add(new Customer() { Id = i, Name = $"顾客{i}" });
            }
            Console.WriteLine($"顾客{customerCount}名进行抢购");
            //不加lock的情况
            NormalMethod(customerCount, stock);
            //加lock的情况
            UseLock(customerCount, stock);

            //加Monitor方法
            UseMonitor(customerCount, stock);
        }

        private void UseMonitor(int customerCount, int stock)
        {
            inStock = stock;//重置库存
            Console.WriteLine($"加Monitor方法的情况:");
            Parallel.For(0, customerCount, (i) =>
            {
                bool lockWasTaken = false;
                try
                {
                    System.Threading.Monitor.Enter(inStockLock, ref lockWasTaken);
                    TryToBuySth(customerCount);
                }
                finally
                {
                    if (lockWasTaken)
                        System.Threading.Monitor.Exit(inStockLock);
                }
            });
            Console.WriteLine($"最终库存：{inStock}");
        }

        private void UseLock(int customerCount, int stock)
        {
            inStock = stock;//重置库存
            Console.WriteLine($"加lock的情况:");
            Parallel.For(0, customerCount, (i) =>
            {
                lock (inStockLock)
                {
                    TryToBuySth(customerCount);
                }
            });
            Console.WriteLine($"最终库存：{inStock}");
        }

        private void NormalMethod(int customerCount, int stock)
        {
            inStock = stock;//重置库存
            Console.WriteLine($"不加lock的情况:");
            //var actions = new Action[customerCount];
            //for (int i = 0; i < customerCount; i++)
            //{
            //    var action = new Action(() =>
            //     {
            //         var buyCustomer = Customers[random.Next(0, customerCount)];
            //         if (inStock > 0)
            //         {
            //             inStock--;
            //             Console.WriteLine($"顾客{buyCustomer.Name}购买了一件商品");
            //         }
            //     });
            //    actions[i]=action;
            //}
            ////配置选项
            //ParallelOptions parallelOptions = new ParallelOptions();
            ////设置并发任务最大数目
            //parallelOptions.MaxDegreeOfParallelism = customerCount;

            //Parallel.Invoke(parallelOptions, actions);

            Parallel.For(0, customerCount, (i) =>
            {
                TryToBuySth(customerCount);
            });

            Console.WriteLine($"最终库存：{inStock}");
        }

        private void TryToBuySth(int customerCount)
        {
            var buyCustomer = Customers[random.Next(0, customerCount)];
            if (inStock > 0)
            {
                inStock--;
                Console.WriteLine($"顾客{buyCustomer.Name}购买了一件商品");
            }
        }
    }
}
