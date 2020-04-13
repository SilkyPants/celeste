
using System.Net.Http;
using System.Threading.Tasks;

namespace Celeste.Services.Spanch
{
    public interface IRoadToRichesRouteService
    {
        public Task<string> GenerateRoute();
    }
    public class RoadToRichesRouteService : IRoadToRichesRouteService
    {
        private readonly HttpClient _httpClient;

        public RoadToRichesRouteService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> GenerateRoute()
        {
            using (var content = new StringContent(""))
            {
                var reponse = await this._httpClient.PostAsync("/generate", content);
            }
            
            return string.Empty;
        }
    }
}