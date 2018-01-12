using System;
using System.Collections.Generic;
using System.Text;
using CoreConsole.Config.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoreConsole
{
    public class ConfigReadDemo
    {
        private readonly ConfigTest configTestByOptions;

        public static void ReadConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("ConfigTest.json");
            var config = builder.Build();
            Console.WriteLine(config["TotalCount"]);
            //索引
            Console.WriteLine(config["Students:0:Name"]);
            Console.WriteLine(config["Students:0:Sex"]);
            Console.WriteLine(config["Students:1:Name"]);
            Console.WriteLine(config["Students:1:Sex"]);
        }

        public static void ReadConfigByBind()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("ConfigTest.json");
            ConfigTest configTest=new ConfigTest();
            var config = builder.Build();
            //Microsoft.Extensions.Configuration.Binder
            config.Bind(configTest);
            Console.WriteLine(configTest.TotalCount);
            Console.WriteLine(configTest.Students[0].Name);
            Console.WriteLine(configTest.Students[0].Sex);
            Console.WriteLine(configTest.Students[1].Name);
            Console.WriteLine(configTest.Students[1].Sex);
        }

        public static void ReadConfigHotUpdate()
        {
            var builder = new ConfigurationBuilder()
                //optional:是否可选
                //reloadOnChange:修改后重新更新(热更新)
                .AddJsonFile("ConfigTest.json",false,true);
            ConfigTest configTest = new ConfigTest();
            var config = builder.Build();
            //Microsoft.Extensions.Configuration.Binder
            config.Bind(configTest);
            Console.WriteLine(configTest.TotalCount);
            Console.WriteLine(configTest.Students[0].Name);
            Console.WriteLine(configTest.Students[0].Sex);
            Console.WriteLine(configTest.Students[1].Name);
            Console.WriteLine(configTest.Students[1].Sex);
            while (true)
            {
                Console.ReadLine();
                //修改bin目录下的config数据，重新读一次，TotalCount生效，Students不生效
                config = builder.Build();
                //Microsoft.Extensions.Configuration.Binder
                config.Bind(configTest);
                Console.WriteLine(configTest.TotalCount);
                Console.WriteLine(configTest.Students[0].Name);
                Console.WriteLine(configTest.Students[0].Sex);
                Console.WriteLine(configTest.Students[1].Name);
                Console.WriteLine(configTest.Students[1].Sex);
            }
        }
    }
}
