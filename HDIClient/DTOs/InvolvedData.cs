using Newtonsoft.Json;

namespace HDIClient.DTOs
{
    public class InvolvedData
    {
        [JsonProperty("brand")]
        public string? Brand { get; set; }

        [JsonProperty("color")]
        public string? Color { get; set; }

        [JsonProperty("involvedLastName")]
        public string InvolvedLastName { get; set; }

        [JsonProperty("involvedName")]
        public string InvolvedName { get; set; }

        [JsonProperty("involvedNumber")]
        public string InvolvedNumber { get; set; }
      
        [JsonProperty("model")]
        public string? Model { get; set; }

        [JsonProperty("plate")]
        public string? Plate { get; set; }

        
    }
}
