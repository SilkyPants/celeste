
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Celeste.Models.Spansh;
using Newtonsoft.Json;

namespace Celeste.Services.Spanch
{
    public interface IRoadToRichesRouteService
    {
        public Task<string> GenerateRoute(R2RRouteParameters parameters);
        public Task<R2RRouteResponse> PollJob(string jobId);
    }

    public class RoadToRichesRouteService : IRoadToRichesRouteService
    {
        private readonly HttpClient _httpClient;

        public RoadToRichesRouteService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> GenerateRoute(R2RRouteParameters parameters)
        {            
            using(var response = await this._httpClient.PostAsync($"/api/riches/route?{parameters.toQuery()}", null)) {
                var content = await response.Content.ReadAsStringAsync();
                var r2rResponse = JsonConvert.DeserializeObject<R2RRouteResponse>(content);
                return r2rResponse?.JobId ?? string.Empty;
            }
        }

        public async Task<R2RRouteResponse> PollJob(string jobId)
        {            
            using(var response = await this._httpClient.GetAsync($"/api/results/{jobId}")) {
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