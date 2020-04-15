
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Celeste.Models.Spansh;
using Newtonsoft.Json;

namespace Celeste.Services.Spanch
{
    public interface IRoadToRichesRouteService
    {
        public Task<string> GenerateRoute(
            int radius,
            int jumpRange,
            string startingPoint,
            string destination,
            int maxResults,
            int maxDistance,
            int minScanValue,
            bool useMappingValue,
            bool loop);
    }

    public class RoadToRichesRouteService : IRoadToRichesRouteService
    {
        private readonly HttpClient _httpClient;

        public RoadToRichesRouteService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> GenerateRoute(
            int radius,
            int jumpRange,
            string startingPoint,
            string destination,
            int maxResults,
            int maxDistance,
            int minScanValue,
            bool useMappingValue,
            bool loop
        )
        {
            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            query["radius"] = radius.ToString();
            query["range"] = jumpRange.ToString();
            query["from"] = startingPoint;
            query["to"] = destination;
            query["max_results"] = maxResults.ToString();
            query["max_distance"] = maxDistance.ToString();
            query["min_value"] = minScanValue.ToString();
            query["use_mapping_value"] = useMappingValue ? "1" : "0";
            query["loop"] = loop ? "1" : "0";
            
            using(var response = await this._httpClient.PostAsync($"/api/riches/route?{query}", null)) {
                var content = await response.Content.ReadAsStringAsync();
                var r2rResponse = JsonConvert.DeserializeObject<R2RRouteResponse>(content);
                return r2rResponse?.JobId ?? string.Empty;
            }
        }

        public async Task<R2RRouteResponse> PollJob(string jobId)
        {            
            using(var response = await this._httpClient.GetAsync($"/api/results?{jobId}")) {
                var content = await response.Content.ReadAsStringAsync();
                var r2rResponse = JsonConvert.DeserializeObject<R2RRouteResponse>(content);
                
                if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest) {
                    Console.WriteLine($"RoadToRichesRouteService: ERROR ({response.StatusCode}): {r2rResponse.ErrorMessage ?? "Unknown Error"}");
                } 
                
                return r2rResponse;
            }
        }
    }
}