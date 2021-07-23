using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Tools
{
    internal class LogTool
    {
        //先初始化Config
        private static LoggingConfiguration Config = new LoggingConfiguration();
        private static object LockObject = new object();
        private static void Configuration(string loggerName)
        {
            var consoleTarget = new ConsoleTarget()
            {
                Layout = @"${date:format=HH\:mm\:ss} ${message}",
                Encoding = Encoding.Default
            };
            //采用*号命名的LoggingRule，不用显示引用，启动其他任何name的LoggingRule都会自动带上它，默认起效。
            Config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));
            var now = DateTime.Now;
            var fileTarget = new FileTarget()
            {
                //按天分文件夹
                FileName = @"${basedir}/output/" + $"{now.Year}-{now.Month}-{now.Day}" + "/" + loggerName + ".log",
                Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss} ${message}",
                Encoding = Encoding.Default,
                //日志归档条件，大小为字节
                //https://stackoverflow.com/questions/50501773/how-to-set-nlog-max-file-size
                //ArchiveAboveSize
            };
            Config.LoggingRules.Add(new LoggingRule(loggerName, LogLevel.Debug, fileTarget));
            LogManager.Configuration = Config;
        }
        public static Logger GetFileLogger(string loggerName)
        {
            //Config里面有实例
            lock (LockObject)
            {
                foreach (var item in Config.LoggingRules)
                {
                    if (item.LoggerNamePattern == loggerName)
                    {
                        Config.LoggingRules.Remove(item);
                        break;
                    }
                }
                //Config里面没有实例，创建实例后返回
                Configuration(loggerName);
            }
            return LogManager.GetLogger(loggerName);
        }

        public static void LogDebugInfo(string method, string content, string paramaters)
        {
            Logger logger = GetFileLogger("Exception");
            logger.Debug($"{method}:{content}  paramaters:{paramaters}");
        }
    }
}
