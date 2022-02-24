using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.RedisTest
{
    public class RedisTestDemo
    {
        public static async Task Run()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        }
    }
}
