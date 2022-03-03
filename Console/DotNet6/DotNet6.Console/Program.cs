// See https://aka.ms/new-console-template for more information
using DotNet6.CodeLibrary.CallerTest;
using DotNet6.CodeLibrary.ConcurrentTest;
using DotNet6.CodeLibrary.DesignPatterns.BuilderPatternTest;
using DotNet6.CodeLibrary.EnumToDictionaryTest;
using DotNet6.CodeLibrary.FlagsTest;
using DotNet6.CodeLibrary.HigherOrderFunctionTest;
using DotNet6.CodeLibrary.ReaderWriterLockTest;
using DotNet6.CodeLibrary.RedisTest;
using DotNet6.CodeLibrary.ReferenceTest;
using DotNet6.CodeLibrary.ReflectionPerformanceTest;

//redis
//await RedisTestDemo.Run();

//线程安全
//await ConcurrentTestDemo.Run();

//锁
//ReaderWriterLock & ReaderWriterLockSlim
//ReaderWriterLockTestDemo.Run();
//ReaderWriterLockSlimTestDemo.Run();
//lock & Monitor
//LockTestDemo.Run();
//MonitorTestDemo.Run();
//乐观锁
//OptimisticLockTestDemo.Run();

//flags特性
//FlagsTestDemo.Run();

//反射性能优化
//ReflectionPerformanceTestDemo.Run();

//设计模式
//建造者模式
//BuilderPatternTestDemo.Run();

//引用类型和值类型
//ReferenceTestDemo.Run();

//枚举转字典
//EnumToDictionaryTestDemo.Run();

//高阶函数
//HigherOrderFunctionTestDemo.Run();

//获取调用者信息
CallerTestDemo.Run();


Console.ReadLine();