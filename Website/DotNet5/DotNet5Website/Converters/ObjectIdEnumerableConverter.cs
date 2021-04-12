using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DotNet5Website.Converters
{
    public class ObjectIdEnumerableConverter : JsonConverter<IEnumerable<long>>
    {
        public override IEnumerable<long> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<long> result = new List<long>();
            while (reader.Read())
            {
                if(reader.TokenType== JsonTokenType.EndObject || reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
                var str = reader.GetString();
                long l = 0;
                bool isSuccess = long.TryParse(str, out l);
                if (isSuccess)
                {
                    result.Add(l);
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<long> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
