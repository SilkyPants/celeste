using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Celeste.Models.Spansh
{

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

        [JsonProperty("result")]
        public List<StarSystem> Systems { get; set; } = new List<StarSystem>();

        public bool JobComplete
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Status)) return false;
                return this.Status.Equals("ok", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public Route ToRoute() {
            return new Route {
                Id = new Guid(this.JobId),
                Systems = this.Systems,
                Name = $"{this.Systems.First().Name} -> {this.Systems.Last().Name}"
            };
        }
    }
}