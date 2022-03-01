using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.DesignPatterns.BuilderPatternTest
{
    internal class Car
    {
        // 汽车部件集合
        private readonly IDictionary<string, string?> parts = new Dictionary<string, string?>();

        /// <summary>
        /// 把单个部件添加到汽车部件集合中
        /// </summary>
        /// <param name="partName">部件名称</param>
        /// <param name="part">部件的具体内容，比如部件名是发动机，这里就是发动机的具体型号</param>
        public void Add(string partName, string? partValue)
        {
            this.parts.Add(partName, partValue);
        }

        /// <summary>
        /// 显示建造过程
        /// </summary>
        public void Show()
        {
            foreach (var item in this.parts)
            {
                Console.WriteLine($"组件{item.Key}-{item.Value}已装好");
            }
        }
    }
}
