using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.CallerTest
{
    public class CallerTestDemo
    {
        public static void Run()
        {
            //跟踪侦听
            System.Diagnostics.Trace.Listeners.Add(new TextWriterTraceListener(System.Console.Out));

            TraceMessage("First");
            TraceMessage("Second");
            TraceMessage("Third");
        }

        public static void TraceMessage(string? message = null,
            //获取方法调用方的方法或属性名称
            [CallerMemberName] string? memberName = "",
            //获取包含调用方的源文件的完整路径
            [CallerFilePath] string? sourceFilePath = "",
            //获取源文件中调用方法的行号
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            Trace.WriteLine("message: " + message);
            Trace.WriteLine("member name: " + memberName);
            Trace.WriteLine("source file path: " + sourceFilePath);
            Trace.WriteLine("source line number: " + sourceLineNumber);
        }
    }
}
