using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Converters.ForForm
{
    public static class EnumTool
    {
        public static List<EnumInfo> GetEnumList<T>() where T : Enum
        {
            var type = typeof(T);
            List<EnumInfo> result = new List<EnumInfo>();
            var list = Enum.GetValues(type).Cast<T>();
            foreach (var item in list)
            {
                var info = new EnumInfo() { Name = item.ToString(), Value = (short)Convert.ChangeType(item, typeof(short)), Description = item.ToDescription() };
                result.Add(info);
            }
            return result;
        }
    }
}
