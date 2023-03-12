using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CodeLibrary.SqlCommandTest
{
    public static class SqlCommandExtensions
    {
        public static void AddArrayParameters<T>(this SqlCommand cmd, string name, IEnumerable<T> values)
        {
            name = name.StartsWith("@") ? name : "@" + name;
            var names = string.Join(", ", values.Select((value, i) =>
            {
                var paramName = name + i;
                cmd.Parameters.AddWithValue(paramName, value);
                return paramName;
            }));
            cmd.CommandText = cmd.CommandText.Replace(name, names);
        }
    }
}
