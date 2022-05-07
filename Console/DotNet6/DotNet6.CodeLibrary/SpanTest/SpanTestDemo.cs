using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

namespace DotNet6.CodeLibrary.SpanTest
{
    /// <summary>
    /// https://mp.weixin.qq.com/s/AmL2qTS3hTAQS-vk7_g91g
    /// </summary>
    public static class SpanTestDemo
    {
        public static void Run()
        {
            //Test1();

            //需要Release模式
            var summary = BenchmarkRunner.Run<MemoryBenchmarkerDemo>();
        }

        private static void Test1()
        {
            Span<byte> arraySpan = stackalloc byte[100];  // 包含指针和Length的只读指针, 类似于go里面的切片

            byte data = 0;
            for (int ctr = 0; ctr < arraySpan.Length; ctr++)
                arraySpan[ctr] = data++;

            arraySpan.Fill(1);

            var arraySum = Sum(arraySpan);
            Console.WriteLine($"The sum is {arraySum}");   // 输出100

            arraySpan.Clear();

            var slice = arraySpan.Slice(0, 50); // 因为是只读属性， 内部New Span<>(), 产生新的切片
            arraySum = Sum(slice);
            Console.WriteLine($"The sum is {arraySum}");  // 输出0
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int Sum(Span<byte> array)
        {
            int arraySum = 0;
            foreach (var value in array)
                arraySum += value;

            return arraySum;
        }
    }

    [MemoryDiagnoser, RankColumn]
    public class MemoryBenchmarkerDemo
    {
        int NumberOfItems = 100000;

        // 对字符串切割， 会产生字符串小对象
        [Benchmark]
        public void StringSplit()
        {
            for (int i = 0; i < NumberOfItems; i++)
            {
                var s = "97 3";

                var arr = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var num1 = int.Parse(arr[0]);
                var num2 = int.Parse(arr[1]);

                _ = num1 + num2;
            }

        }

        // 对底层字符串切片
        [Benchmark]
        public void StringSlice()
        {
            for (int i = 0; i < NumberOfItems; i++)
            {
                var s = "97 3";
                var position = s.IndexOf(' ');
                ReadOnlySpan<char> span = s.AsSpan();
                var num1 = int.Parse(span.Slice(0, position));
                var num2 = int.Parse(span.Slice(position));

                _ = num1 + num2;

            }
        }
    }
}
