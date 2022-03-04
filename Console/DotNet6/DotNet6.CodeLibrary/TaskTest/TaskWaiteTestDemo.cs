using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.TaskTest
{
    public class TaskWaiteTestDemo
    {
		public static void Run()
		{
			System.Console.WriteLine("***start test***");
			TaskTest taskTest = new TaskTest();
			taskTest.Test1();
			taskTest.Test2();
			taskTest.Test3();

			System.Console.WriteLine("***end test***");
		}

	}

	public class TaskTest
	{
		public void Test1()
		{
			Task.Run(() =>
			{
				Task.Delay(1000);
				System.Console.WriteLine("Test1");
				return "Test1";
			});
		}

		public string Test2()
		{
			Task<string> task = Task.Run(() =>
			{
				Task.Delay(5000);
				System.Console.WriteLine("Test2");
				return "Test2";
			});

			//等待返回，故Test2在前面输出
			Task.WaitAll(task);

			return task.Result;
		}

		public void Test3()
		{
			Task.Factory.StartNew(() =>
			{
				//输出时间随机，但一定在Test2后面
				Task.Delay(1000);
				System.Console.WriteLine("Test3");
			});
		}
	}
}
