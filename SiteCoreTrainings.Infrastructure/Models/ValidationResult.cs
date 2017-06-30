using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
