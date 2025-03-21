using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using TheBrickVault.Core.DTO;
using TheBrickVault.Core.Models;
using TheBrickVault.Infrastructure.Data;



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
        private readonly LegoDbContext _dbContext;
        private const string BaseUrl = "https://rebrickable.com/api/v3/lego/sets/";


        //Initializes the RebrickableService with an IHttpClientFactory and an IConfiguration object.
        public RebrickableService(IHttpClientFactory clientFactory, IConfiguration configuration, LegoDbContext dbContext)
        {

            _clientFactory = clientFactory;
            //this is to get the API key from UserSecrets
            _apiKey = configuration["Rebrickable:ApiKey"];
            _dbContext = dbContext;

        }


        //use HttpClient to connect to Rebrickable's API endpoints and search for Lego sets and their parts in a single method.
        //AI explanation: functionality -
        // 1. calls SearchLegoSetsAsycn to fetch a list of Lego sets matching the search query.
        // 2. Iterates over each set and calls FetchPartsForSetsAsync to fetch the parts for each set.
        // 3. Combines each set with its parts into a RebrickableLegoSetWithParts object.
        // 4. Returns a list of RebrickableLegoSetWithParts objects.
        public async Task<List<RebrickableLegoSetWithParts>> FetchSetsAndPartsAsync(string searchQuery)
        {


            var sets = await SearchLegoSetsAsync(string.IsNullOrWhiteSpace(searchQuery) ? "lego" : searchQuery);
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

Console.WriteLine($"SearchLegoSetsAsync: Searching for sets with query '{searchQuery}'.");


            if (string.IsNullOrWhiteSpace(searchQuery))
            {
Console.WriteLine("SearchLegoSetsAsync: Empty search query, returning no sets.");

                return new List<RebrickableLegoSet>();
            }

            var url = $"{BaseUrl}?search={searchQuery}&key={_apiKey}";

            try
            {
                var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);

                if (response?.Results != null)
                {
 Console.WriteLine($"SearchLegoSetsAsync: Found {response.Results.Count} sets.");
                    return response.Results;
                }
                else
                {
 Console.WriteLine("SearchLegoSetsAsync: No sets found.");
                    return new List<RebrickableLegoSet>();
                }
            }
            catch (Exception ex)
            {
Console.WriteLine($"SearchLegoSetsAsync: API request failed: {ex.Message}");
                return new List<RebrickableLegoSet>();
            }

            //return response?.Results ?? new List<RebrickableLegoSet>();
        }


        //fetch parts for a specific set from Rebrickable's API.
        public async Task<List<RebrickableLegoPart>> FetchPartsForSetsAsync(string setNum)
        {
            var url = $"{BaseUrl}{setNum}/parts/?key={_apiKey}";
            var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickablePartsResult>(url);
            var parts = response?.Results ?? new List<RebrickableLegoPart>();

            foreach (var part in parts)
            {
                DbLegoPart newLegoParts = new DbLegoPart
                {
                    SetNum = setNum,
                    InvPartId = part.inv_part_id,
                    //PartNum = part.part_num,
                    Quantity = part.quantity
                };
                await _dbContext.DbLegoParts.AddAsync(newLegoParts);

                await _dbContext.SaveChangesAsync();


            }
            return parts;
        }
        public class RebrickableSearchResult
        {
            public List<RebrickableLegoSet> Results { get; set; } = new();
        }
        public class RebrickablePartsResult
        {
            public List<RebrickableLegoPart> Results { get; set; } = new();
        }

        //Query User's parts from DbLegoParts table
        public async Task<Dictionary<int, int?>> GetUserPartsAsync()
        //parameter inside GetUserPartsAsync() is nothing right now, but if needed, can add userId or something like that
        {
            var parts = await _dbContext.DbLegoParts
                .GroupBy(p => p.InvPartId) //parts are grouped by InvPartId
                .Select(g => new { InvPartId = g.Key, Quantity = g.Sum(p => p.Quantity) }) //InvPartId is the key, Quantity is the sum of all quantities for that InvPartId
                .ToListAsync(); //convert to list

            return parts.ToDictionary(p => p.InvPartId, p => p.Quantity); //convert the list from query to a dictionary, InvPartId = key, Quantity = value.
        }

        //Sensing an impending doom! This method is the final piece.  Please please please work.
        //Find possible matches between Rebrickable's API and User's parts
        public async Task<List<RebrickableLegoSetWithParts>> FindMatchingSetsAsync()
        {
//debugging
Console.WriteLine("FindMatchingSetAsync started.");

            //Fetch user's parts from GetUserPartsAsync()
            var userParts = await GetUserPartsAsync();

//debugging
Console.WriteLine($"FindMatchingSetAsync fetched. {userParts.Count} user parts");

            //Fetch all sets and corresponding parts from Rebrickable's API using FetchSetsAndPartsAsync method up above
            var allDtoSetsWithParts = await FetchSetsAndPartsAsync("");

//debugging
Console.WriteLine($"FindMatchingSetAsync fetched. {allDtoSetsWithParts.Count} sets from API.");

            //Initalizes an empty list to store matching sets
            var matchingSets = new List<RebrickableLegoSetWithParts>();

            //loop through each Rebrickable's sets and its parts
            foreach (var setWithParts in allDtoSetsWithParts)
            {
                //Check if user's parts match Rebrickable's sets of parts. If they do, add to matchingSets list.
                bool partsMatch = true;

                //Check if user's parts match the DTO's set's parts. 
                foreach (var part in setWithParts.Parts)
                {
                    if (!userParts.ContainsKey(part.inv_part_id) || userParts[part.inv_part_id] < part.quantity)
                    {
                        partsMatch = false;
                        break;
                    }                  
                }

                //if all parts match, add to matching sets list
                if (partsMatch)
                {
                    matchingSets.Add(setWithParts);
                }
            }

//debugging
Console.WriteLine($"FindMatchingSetAsync found {matchingSets.Count} matching sets.");

            return matchingSets;

        }
    }
}