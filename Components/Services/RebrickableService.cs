using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using TheBrickVault.Core.DTO;



namespace TheBrickVault.Components.Services
{
    //The comments are really just for me to remind myself the purpose of the class and how it fits into 
    //the overall architecture of the application.
    //
    //RebrickableService is to handle fetching data from the Rebrickable API and processing it into a 
    //format that can be used by the application.
    //
    //Use the HttpClient to connect to Rebrickable's API endpoints and search for Lego sets and their parts.
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
        

        //use HttpClient to connect to Rebrickable's API endpoints and search for Lego sets and their parts in a single method.
        public async Task<List<RebrickableLegoSetWithParts>> FetchSetsAndPartsAsync(string searchQuery)
        {
            var sets = await SearchLegoSetsAsync(searchQuery);
            var setsWithParts = new List<RebrickableLegoSetWithParts>();

            foreach (var set in sets)
            {
                var parts = await FetchPartsForSetsAsync(set.set_num);
                setsWithParts.Add(new RebrickableLegoSetWithParts
                {
                    Set = set,
                    Parts = parts
                });
            }
            return setsWithParts;
        }

        //use HttpClient to connect to Rebrickable's API endpoints and search for Lego sets.
        private async Task<List<RebrickableLegoSet>> SearchLegoSetsAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return new List<RebrickableLegoSet>();
            }

            var url = $"{BaseUrl}?search={searchQuery}&key={_apiKey}";
            var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);
            return response?.Results ?? new List<RebrickableLegoSet>();
        }


        //fetch parts for a specific set from Rebrickable's API.
        public async Task<List<RebrickableLegoPart>> FetchPartsForSetsAsync(string setNum)
        {
            var url = $"{BaseUrl}{setNum}/parts/?key={_apiKey}";
            var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickablePartsResult>(url);
            return response?.Results ?? new List<RebrickableLegoPart>();
        }
    }
    public class RebrickableSearchResult
    {
        public List<RebrickableLegoSet> Results { get; set; } = new();
    }
    public class RebrickablePartsResult
    {
        public List<RebrickableLegoPart> Results { get; set; } = new();
    }

    public class RebrickableLegoSetWithParts
    {
        public RebrickableLegoSet Set { get; set; }
        public List<RebrickableLegoPart> Parts { get; set; }
        
    }

}