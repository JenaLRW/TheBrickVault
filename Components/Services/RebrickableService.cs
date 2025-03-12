using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TheBrickVault.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;



namespace TheBrickVault.Components.Services
{
    //The comments are really just for me to remind myself the purpose of the class and how it fits into 
    //the overall architecture of the application.
    //
    //RebrickableService is to handle fetching data from the Rebrickable API and processing it into a 
    //format that can be used by the application.
    //
    //Use the HttpClient to connect to Rebrickable's API endpoints and search for Lego sets.
    //
    //The RebrickableService will be responsible for making the API calls, handling the responses, and 
    //converting the data into a format that can be used by the application.
    public class RebrickableService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiKey;
        private const string BaseUrl = "https://rebrickable.com/api/v3/lego/sets/";

        public RebrickableService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {

            _clientFactory = clientFactory;
            //this is to get the API key from UserSecrets
            _apiKey = configuration["Rebrickable:ApiKey"];
            
        }
        

        //use HttpClient to connect to Rebrickable's API endpoints and search for Lego sets.
        public async Task<string> GetLegoSetAsync(string searchQuery)
        {
            var client = _clientFactory.CreateClient("RebrickableClient");
            var response = await client.GetAsync($"lego/sets/?search={searchQuery}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return "Error retrieving data.";

        }

        public async Task<List<RebrickableLegoSet>> SearchLegoSetsAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return new List<RebrickableLegoSet>();
            }

                var url = $"{BaseUrl}?search={searchQuery}&key={_apiKey}";

                var response =  await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);
                return response?.Results ?? new List<RebrickableLegoSet>();
            

            //var client = _clientFactory.CreateClient("RebrickableClient");
            //var response = await client.GetAsync($"lego/sets/?search={searchQuery}");
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var legoSets = JsonConvert.DeserializeObject<RebrickableLegoSet>(content);
            //    return legoSets;
            //}
            //return null;
        }

    }
    public class RebrickableSearchResult
    {
        public List<RebrickableLegoSet> Results { get; set; } = new();
    }

}