using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.WatchFileTest
{
    public class WatchByFileProviderDemo
    {
        static readonly string fileName = "testfile.json";
        static string testFile;
        public static void Run()
        {
            string root = Path.Combine(Directory.GetCurrentDirectory(), "WatchFileTest");
            testFile = $"{root}\\{fileName}";

            Console.WriteLine($"根目录: {root}\n测试文件:{testFile}");

            //引用Microsoft.Extensions.FileProviders
            //建立映射
            //https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.fileproviders.physicalfileprovider?view=dotnet-plat-ext-6.0&WT.mc_id=DT-MVP-5003010
            IFileProvider fileProvider = new PhysicalFileProvider(root);

            var testData = ReadData(testFile);
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 原始数据:\n{testData}");
            Console.WriteLine();

            //注册改变时的事件
            //https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.primitives.changetoken?view=dotnet-plat-ext-6.0&WT.mc_id=DT-MVP-5003010
            ChangeToken.OnChange(
                () => fileProvider.Watch(fileName),
                () => ShowChange(fileProvider)
                );

            Console.WriteLine($"开始监控文件变化:{testFile}");
        }

        public static void ShowChange(IFileProvider fileProvider)
        {
            var testData = ReadData(testFile);
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 改变后的数据:\n{testData}");
            Console.WriteLine();
        }

        public static string? ReadData(string filePath)
        {
            using(FileStream stream = new FileStream(filePath, FileMode.Open))
            using(StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
