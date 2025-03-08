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

    //This class is the first step in the process of user searching for lego sets. 

    //RebrickableService is to handle fetching data from the Rebrickable API and processing it into a 
    //format that can be used by the application.

    //Use the HttpClient to connect to Rebrickable's API endpoints and search for Lego sets.

    //The RebrickableService will be responsible for making the API calls, handling the responses, and 
    //converting the data into a format that can be used by the application.
    public class RebrickableService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public RebrickableService(IHttpClientFactory httpClient, IConfiguration apiKey)
        {
            _clientFactory = httpClient;
            //this is to get the API key from UserSecrets
            _configuration = apiKey;
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

    }





    // old design.  trying a new design and keeping old design in case the new is a fail.

    //public class RebrickableService
    //{
    //    private readonly IHttpClientFactory _clientFactory;
    //    private readonly RebrickableSettings _rebrickableSettings;

    //    public RebrickableService(IHttpClientFactory clientFactory, RebrickableSettings rebrickableSettings)
    //    {
    //        _clientFactory = clientFactory;
    //        _rebrickableSettings = rebrickableSettings;
    //    }
    //    public async Task<LegoSet> GetSetDetailsAsync(int setNum)
    //    {
    //        var client = _clientFactory.CreateClient("RebrickableClient");
    //        var response = await client.GetAsync($"lego/sets/{setNum}/");

    //        if (response.IsSuccessStatusCode)
    //        {

    //            var content = await response.Content.ReadAsStringAsync();
    //            return JsonConvert.DeserializeObject<LegoSet>(content);

    //        }
    //        return null;
    //    }

    //}
}