using DotNet6.CodeLibrary.AggregateTest;
using DotNet6.CodeLibrary.CalculateAngle;
using DotNet6.CodeLibrary.CallerTest;
using DotNet6.CodeLibrary.ConcurrentDictionaryTest;
using DotNet6.CodeLibrary.ConcurrentTest;
using DotNet6.CodeLibrary.ConnectLinuxTest;
using DotNet6.CodeLibrary.DesignPatterns.BuilderPatternTest;
using DotNet6.CodeLibrary.EmitTest;
using DotNet6.CodeLibrary.EnumToDictionaryTest;
using DotNet6.CodeLibrary.ExpressionTest;
using DotNet6.CodeLibrary.FlagsTest;
using DotNet6.CodeLibrary.HigherOrderFunctionTest;
using DotNet6.CodeLibrary.InterlockedTest;
using DotNet6.CodeLibrary.LazyLoadTest;
using DotNet6.CodeLibrary.MathRoundTest;
using DotNet6.CodeLibrary.MemoryTest;
using DotNet6.CodeLibrary.OverrideTest;
using DotNet6.CodeLibrary.ReaderWriterLockTest;
using DotNet6.CodeLibrary.RedisTest;
using DotNet6.CodeLibrary.ReferenceTest;
using DotNet6.CodeLibrary.ReflectionPerformanceTest;
using DotNet6.CodeLibrary.ReplaceMethodTest;
using DotNet6.CodeLibrary.SpanTest;
using DotNet6.CodeLibrary.StackFrameTest;
using DotNet6.CodeLibrary.SwitchTest;
using DotNet6.CodeLibrary.TaskTest;
using DotNet6.CodeLibrary.ThreadIDTest;
using DotNet6.CodeLibrary.WatchFileTest;
using DotNet6.CodeLibrary.XMLToJsonTest;


//Math.Round(with different MidpointRounding.ToEven)
MathRoundTestDemo.Run();

//redis
await RedisTestDemo.Run();

//线程安全
await ConcurrentTestDemo.Run();

//锁
//ReaderWriterLock & ReaderWriterLockSlim
ReaderWriterLockTestDemo.Run();
ReaderWriterLockSlimTestDemo.Run();
//lock & Monitor
LockTestDemo.Run();
MonitorTestDemo.Run();
//Interlocked
InterlockedTestDemo.Run();
//乐观锁
OptimisticLockTestDemo.Run();

//flags特性
FlagsTestDemo.Run();

//反射性能优化
ReflectionPerformanceTestDemo.Run();
EmitTestDemo.Run();

//设计模式
//建造者模式
BuilderPatternTestDemo.Run();

//引用类型和值类型
ReferenceTestDemo.Run();

//枚举转字典
EnumToDictionaryTestDemo.Run();

//高阶函数
HigherOrderFunctionTestDemo.Run();

//获取调用者信息
CallerTestDemo.Run();

//Task await
TaskAwaiterTestDemo.Run();
TaskWaiteTestDemo.Run();
TaskAndValueTaskTestDemo.Run();

//懒加载
LazyLoadTestDemo.Run();

AggregateTestDemo.Run();

SwitchTestDemo.Run();

await ConnectLinuxTestDemo.Run();

ExpressionTestDemo.Run();

//文件监控
WatchByFileProviderDemo.Run();
new FileSystemWatcherDemo().Run();

XMLToJsonTestDemo.Run();

StackFrameTestDemo.Run();

MemoryTestDemo.Run();

ThreadIDTestDemo.Run();

//ConcurrentDictionary
ConcurrentDictionaryNormalDemo.Run();
ConcurrentDictionaryLazyDemo.Run();

//自定义表达式
CustomExpressionTestDemo.Run();

//Span
SpanTestDemo.Run();

//重写
OverrideTestDemo.Run();

//计算角度
CalculateAngleDemo.Run();

//动态替换方法
ReplaceMethodTestDemo.Run();

Console.ReadLine();