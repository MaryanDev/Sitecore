using Newtonsoft.Json;

namespace SiteCoreTrainings.Infrastructure.Models
{
    public class AddressValidationResponse
    {
        [JsonProperty("validation_results")]
        public ValidationResult ValidationResult { get; set; }
    }
}
