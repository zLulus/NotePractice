using Newtonsoft.Json;

namespace DotNet6WebAPI.Dtos
{
    public class GetNumberWith2Digit
    {
        [JsonConverter(typeof(DecimalConverter))]
        public decimal Data { get; set; }
    }
}
