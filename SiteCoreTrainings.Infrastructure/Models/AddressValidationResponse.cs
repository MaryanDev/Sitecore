using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SiteCoreTrainings.Infrastructure.Models
{
    public class AddressValidationResponse
    {
        [JsonProperty("validation_results")]
        public ValidationResult ValidationResult { get; set; }
    }
}
