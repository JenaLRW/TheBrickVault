using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TheBrickVault.Core.Models;


namespace TheBrickVault.Components.Services
{
    public class RebrickableService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly RebrickableSettings _rebrickableSettings;

        public RebrickableService(IHttpClientFactory clientFactory, RebrickableSettings rebrickableSettings)
        {
            _clientFactory = clientFactory;
            _rebrickableSettings = rebrickableSettings;
        }
        public async Task<LegoSet> GetSetDetailsAsync(int setNum)
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