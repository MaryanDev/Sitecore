using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SiteCoreTrainings.Infrastructure.Models
{
    public  class ValidationMessage
    {
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
