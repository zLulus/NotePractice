using Newtonsoft.Json;

namespace DotNet6WebAPI
{
    public class DecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            decimal? dec = value as decimal?;
            if (dec == null)
                return;
            writer.WriteValue((dec.Value).ToString("0.00"));
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //do nothing
            return existingValue;
        }
    }
}
