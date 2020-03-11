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
            //初始化顾客
            Customers = new List<Customer>();
            for (int i = 0; i < customerCount; i++)
            {
                Customers.Add(new Customer() { Id = i, Name = $"顾客{i}" });
            }
            Console.WriteLine($"顾客{customerCount}名进行抢购");
            Console.WriteLine("---------------------------");
            //不加lock的情况
            WithoutLock(customerCount, stock);
            //加lock的情况
            Console.WriteLine("---------------------------");
            UseLock(customerCount, stock);
            //加Monitor方法
            Console.WriteLine("---------------------------");
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
                    TryToBuyGoods(customerCount);
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
                    TryToBuyGoods(customerCount);
                }
            });
            Console.WriteLine($"最终库存：{inStock}");
        }

        private void WithoutLock(int customerCount, int stock)
        {
            inStock = stock;//重置库存
            Console.WriteLine($"不加lock的情况:");
            Parallel.For(0, customerCount, (i) =>
            {
                TryToBuyGoods(customerCount);
            });

            Console.WriteLine($"最终库存：{inStock}");
        }

        private void TryToBuyGoods(int customerCount)
        {
            var buyCustomer = Customers[random.Next(0, customerCount)];
            var sleepTime = random.Next(1000, 10000);
            if (inStock > 0)
            {
                //模拟购物业务逻辑处理时间(占用资源)
                Thread.Sleep(sleepTime);
                inStock--;
                Console.WriteLine($"顾客{buyCustomer.Name}购买了一件商品");
            }
            //Console.WriteLine($"当前库存:{inStock}");
        }
    }
}
