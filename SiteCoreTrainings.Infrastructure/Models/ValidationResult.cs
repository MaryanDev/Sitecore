using System.Collections.Generic;
using Newtonsoft.Json;

namespace SiteCoreTrainings.Infrastructure.Models
{
    public class ValidationResult
    {
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("messages")]
        public List<ValidationMessage> Messages { get; set; }
    }
}
