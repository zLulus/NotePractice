using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharpWpfDemo.Log
{
    public class NLogHelper
    {
        //先初始化Config
        private static LoggingConfiguration Config = new LoggingConfiguration();
        private static void Configuration(string loggerName)
        {
            var consoleTarget = new ConsoleTarget()
            {
                Layout = @"${date:format=HH\:mm\:ss} ${message}",
                Encoding = Encoding.Default
            };
            //采用*号命名的LoggingRule，不用显示引用，启动其他任何name的LoggingRule都会自动带上它，默认起效。
            Config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, consoleTarget));
            var fileTarget = new FileTarget()
            {
                FileName = @"${basedir}/output/" + loggerName + ".log",
                Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss} ${message}",
                Encoding = Encoding.Default
            };
            Config.LoggingRules.Add(new LoggingRule(loggerName, NLog.LogLevel.Debug, fileTarget));
            NLog.LogManager.Configuration = Config;
        }
        public static NLog.Logger GetFileLogger(string loggerName)
        {
            //Config里面有实例，直接返回
            foreach (var item in Config.LoggingRules)
            {
                if (item.LoggerNamePattern == loggerName)
                    return NLog.LogManager.GetLogger(loggerName);
            }
            //Config里面没有实例，创建实例后返回
            Configuration(loggerName);
            return NLog.LogManager.GetLogger(loggerName);
        }
    }
}
