using Newtonsoft.Json;

namespace Celeste.Models.Spansh {

    public class R2RRouteParameters {
        
            public int Radius {get;set;}
            public int JumpRange {get;set;}
            public string StartingPoint {get;set;}
            public string Destination {get;set;}
            public int MaxResults {get;set;}
            public int MaxDistance {get;set;}
            public int MinScanValue {get;set;}
            public bool UseMappingValue {get;set;}
            public bool Loop {get;set;}

            public string toQuery() {
                var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
                query["radius"] = Radius.ToString();
                query["range"] = JumpRange.ToString();
                query["from"] = StartingPoint;
                query["to"] = Destination;
                query["max_results"] = MaxResults.ToString();
                query["max_distance"] = MaxDistance.ToString();
                query["min_value"] = MinScanValue.ToString();
                query["use_mapping_value"] = UseMappingValue ? "1" : "0";
                query["loop"] = Loop ? "1" : "0";

                return query.ToString();
            }
    }

    
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