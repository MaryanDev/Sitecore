using Newtonsoft.Json;

namespace SiteCoreTrainings.Infrastructure.Models
{
    public class Location
    {
        [JsonProperty("lat")]
        public double Lattitude { get; set; }
        [JsonProperty("lng")]
        public double Longittude { get; set; }
    }
}
