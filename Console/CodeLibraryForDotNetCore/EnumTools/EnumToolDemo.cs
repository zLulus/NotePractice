using CodeLibraryForDotNetCore.EnumTools.Dtos;
using CodeLibraryForDotNetCore.EnumTools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeLibraryForDotNetCore.EnumTools
{
    public class EnumToolDemo
    {
        public static void Run()
        {
            List<EnumInfo> result = new List<EnumInfo>();
            var list = Enum.GetValues(typeof(SexEnum)).Cast<SexEnum>();
            Console.WriteLine($"枚举结果如下：");
            foreach (var item in list)
            {
                var info = new EnumInfo() { Name = item.ToString(), Value = (int)item, Description = item.ToDescription() };
                result.Add(info);
                Console.WriteLine($"Name:{info.Name},Value:{info.Value},Description:{info.Description}");
            }

            //使用泛型
            List<EnumInfo> result2 = EnumTool.GetEnumList<SexEnum>();
        }
    }
}
