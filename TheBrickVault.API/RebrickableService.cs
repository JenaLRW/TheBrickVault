using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TheBrickVault.Core.Models;


namespace TheBrickVault.API
{
    public class RebrickableService
    {
        private readonly IHttpClientFactory _clientFactory;

        public RebrickableService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<LegoSet> GetLegoSetAsync(int setNum)
        {
            var client = _clientFactory.CreateClient("RebrickableClient");
            var response = await client.GetAsync($"lego/sets/{setNum}/");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LegoSet>(content);
            }
            return null;
        }

    }
        
}
