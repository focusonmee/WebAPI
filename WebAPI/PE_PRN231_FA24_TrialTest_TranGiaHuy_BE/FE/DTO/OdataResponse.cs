using Newtonsoft.Json;

namespace FE.DTO
{
    public class OdataResponse<T>
    {
        [JsonProperty("Value")]
        public List<T>  Value { get; set; }

        [JsonProperty("@odata.count")]
        public int Count { get; set; }

    }
}
