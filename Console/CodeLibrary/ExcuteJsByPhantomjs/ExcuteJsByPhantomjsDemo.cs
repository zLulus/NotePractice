using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.ExcuteJsByPhantomjs
{
    public class ExcuteJsByPhantomjsDemo
    {
        public static void ExcuteJs()
        {
            //注意：保证phantomjs.exe和js在生成目录下存在
            string url = "1.5";
            //这里调用cmd.exe
            Process pProcess = new Process();
            //调用phantomjs.exe
            pProcess.StartInfo.FileName = $"{Environment.CurrentDirectory}//ExcuteJsByPhantomjs//phantomjs.exe";
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.EnableRaisingEvents = false;
            //在phantomjs.exe里面执行的命令
            //Js文件中先抄写了jQuery，最后是自己的业务代码(希望有更好的解决方案???如果有请联系我~)
            pProcess.StartInfo.Arguments = $"//ExcuteJsByPhantomjs//Test2.js {url}";
            pProcess.Start();
            
            char[] spliter = { '\r' };
            StreamReader sReader = pProcess.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);

            foreach (string s in output)
                Console.WriteLine(s);

            pProcess.WaitForExit();

            //取出计算结果
            Console.WriteLine(output[0]);
            pProcess.Close();
        }
    }
}
