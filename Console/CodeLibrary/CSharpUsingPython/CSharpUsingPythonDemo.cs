using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.CSharpUsingPython
{
    public class CSharpUsingPythonDemo
    {
        public static void ExcutePython()
        {
            string progToRun = "//CSharpUsingPython//test.py";
            char[] spliter = { '\r' };

            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

            string psw = "123456";
            string parameters2 = "haha";
            //文件路径+参数集合
            proc.StartInfo.Arguments = string.Concat(progToRun, " ", psw.ToString(), " ", parameters2.ToString());
            proc.Start();

            StreamReader sReader = proc.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(spliter);

            foreach (string s in output)
                Console.WriteLine(s);

            proc.WaitForExit();

            //取出计算结果
            Console.WriteLine(output[0]);

            Console.Read();
        }
    }
}
