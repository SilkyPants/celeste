using Newtonsoft.Json;

namespace Celeste.Models.Spansh {

    
    [System.Serializable]
    public class R2RRouteResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("job")]
        public string JobId { get; set; }

        [JsonProperty("error")]
        public string ErrorMessage { get; set; }
    }
}