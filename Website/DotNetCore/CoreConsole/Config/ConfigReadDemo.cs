using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConsole
{
    public class ConfigReadDemo
    {
        public static void ReadConfig()
        {
            var builder = new ConfigurationBuilder();
            //引用
            //json属性-复制
            builder.AddJsonFile("ConfigTest.json");
            var config = builder.Build();
            Console.WriteLine(config["TotalCount"]);
            Console.WriteLine(config["Students:0:Name"]);
            Console.WriteLine(config["Students:0:Sex"]);
            Console.WriteLine(config["Students:1:Name"]);
            Console.WriteLine(config["Students:1:Sex"]);
        }

        public static void ReadConfigByBind()
        {

        }

        public static void ReadConfigByOptions()
        {

        }

        public static void ReadConfigHotUpdate()
        {

        }
    }
}
