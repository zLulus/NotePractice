using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.EnumToDictionaryTest
{
    public enum EnumToDictionaryEnum
    {
        A=1,
        B=2,
        C=3,
        D=4,
        E=5
    }

    public class EnumToDictionaryTestDemo
    {
        public static void Run()
        {
            FieldInfo[] fields = typeof(EnumToDictionaryEnum)
               .GetFields(BindingFlags.Static | BindingFlags.Public)
               ?? Array.Empty<FieldInfo>();

            var dictionary = fields.ToDictionary(k => k.Name, v => (int)v.GetValue(null));

            var dictionary2 = Enum.GetValues(typeof(EnumToDictionaryEnum))
               .Cast<EnumToDictionaryEnum>()
               .ToDictionary(k => k.ToString(), v => (int)v);
        }
    }
}
