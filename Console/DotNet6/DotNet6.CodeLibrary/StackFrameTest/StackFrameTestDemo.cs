using System.Diagnostics;

namespace DotNet6.CodeLibrary.StackFrameTest
{
    public static class StackFrameTestDemo
    {
        public static void Run()
        {
            SecondLevel();
		}

        static void FirstLevel()
        {
            //https://docs.microsoft.com/zh-cn/dotnet/api/system.diagnostics.stackframe?view=net-6.0&WT.mc_id=DT-MVP-5003010
            //https://blog.csdn.net/jwq18734130053/article/details/103760964
            //skipFrames:堆栈帧中跳过的帧数
            StackFrame stackFrame = new StackFrame(1, true);
            Console.WriteLine("跳过1帧");
            Console.WriteLine(stackFrame.GetFileName()); //获取包含所执行代码的文件名。
            Console.WriteLine(stackFrame.GetFileLineNumber().ToString());   //也就是FirstLevel()被调用地方的行号
            Console.WriteLine(stackFrame.GetFileColumnNumber().ToString());   //也就是FirstLevel()被调用地方的第一个字母“F”所处的列
            Console.WriteLine(stackFrame.GetMethod().Module);   //stackFrame.GetMethod()获取在其中执行帧的方法。
            Console.WriteLine(stackFrame.GetMethod().ReflectedType);
            Console.WriteLine(stackFrame.GetMethod().ToString());

            stackFrame = new StackFrame(2, true);
            Console.WriteLine("跳过2帧");
            Console.WriteLine(stackFrame.GetFileName()); //获取包含所执行代码的文件名。
            Console.WriteLine(stackFrame.GetFileLineNumber().ToString());   //也就是FirstLevel()被调用地方的行号
            Console.WriteLine(stackFrame.GetFileColumnNumber().ToString());   //也就是FirstLevel()被调用地方的第一个字母“F”所处的列
            Console.WriteLine(stackFrame.GetMethod().Module);   //stackFrame.GetMethod()获取在其中执行帧的方法。
            Console.WriteLine(stackFrame.GetMethod().ReflectedType);
            Console.WriteLine(stackFrame.GetMethod().ToString());

            stackFrame = new StackFrame(0, true);
            Console.WriteLine("跳过0帧");
            Console.WriteLine(stackFrame.GetFileName()); //获取包含所执行代码的文件名。
            Console.WriteLine(stackFrame.GetFileLineNumber().ToString());   //也就是FirstLevel()被调用地方的行号
            Console.WriteLine(stackFrame.GetFileColumnNumber().ToString());   //也就是FirstLevel()被调用地方的第一个字母“F”所处的列
            Console.WriteLine(stackFrame.GetMethod().Module);   //stackFrame.GetMethod()获取在其中执行帧的方法。
            Console.WriteLine(stackFrame.GetMethod().ReflectedType);
            Console.WriteLine(stackFrame.GetMethod().ToString());
        }

        static void SecondLevel()
        {
            FirstLevel();
        }
    }
}
