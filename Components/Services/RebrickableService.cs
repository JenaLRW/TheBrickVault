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
using Microsoft.AspNetCore.RateLimiting;



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
            _apiKey = configuration["Rebrickable:ApiKey"];   //this is to get the API key from UserSecrets
            _dbContext = dbContext;
        }

        //use HttpClient to connect to Rebrickable's API endpoints and search for Lego sets and their parts in a single method.
        //AI explanation: functionality -
        // 1. calls SearchLegoSetsAsycn to fetch a list of Lego sets matching the search query.
        // 2. Iterates over each set and calls FetchPartsForSetsAsync to fetch the parts for each set.
        // 3. Combines each set with its parts into a RebrickableLegoSetWithParts object.
        // 4. Returns a list of RebrickableLegoSetWithParts objects.
        public async Task<List<RebrickableLegoSetWithParts>> FetchSetsAndPartsAsync(string searchQuery, int currentPage = 1, int resultsPerPage = 10)
        {
            var sets = await SearchLegoSetsAsync(searchQuery, currentPage, resultsPerPage);
            var setsWithParts = new List<RebrickableLegoSetWithParts>();

            foreach (var set in sets)
            {
                var parts = await FetchPartsForSetsAsync(set.set_num);
                setsWithParts.Add(new RebrickableLegoSetWithParts
                {
                    Set = set,
                    Parts = parts
                });
                //await Task.Delay(500); // Delay to avoid hitting the API rate limit
            }
            Console.WriteLine($"[DEBUG] FetchSetsAndPartsAsync: Loaded {setsWithParts.Count} sets on page {currentPage}");

            return setsWithParts;
        }

        





        private async Task<List<RebrickableLegoSet>> SearchLegoSetsAsync(string searchQuery, int currentPage = 1, int resultsPerPage = 10)
        {
            Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: Searching for sets with query '{searchQuery}'.");

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: Empty search query, returning no sets.");

                return new List<RebrickableLegoSet>();
            }

            var url = $"{BaseUrl}?search={searchQuery}&key={_apiKey}&page={currentPage}&page_size={resultsPerPage}&inc_color_details=0";

            try
            {
                var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);
                var sets = response?.Results ?? new List<RebrickableLegoSet>();

                Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: Found {sets.Count} sets on page {currentPage}.");
                return sets;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: API request failed: {ex.Message}");
                return new List<RebrickableLegoSet>();
            }
        }





        public async Task<List<RebrickableLegoPart>> FetchPartsForSetsAsync(string setNum)   //fetch parts for a specific set from Rebrickable's API.
        {
            var allParts = new List<RebrickableLegoPart>();
            string url = $"{BaseUrl}{setNum}/parts/?key={_apiKey}";
            do
            {
            var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickablePartsResult>(url);
                if (response == null) break;

                allParts.AddRange(response.Results ?? new List<RebrickableLegoPart>());
                url = response.Next;
            } while (!string.IsNullOrEmpty(url));
            foreach (var part in allParts)
            {
                DbLegoPart newLegoPart = new DbLegoPart
                {
                    SetNum = setNum,
                    PartNum = part.part_num,
                    InvPartId = part.inv_part_id,
                    Quantity = part.quantity
                };
                await _dbContext.DbLegoParts.AddAsync(newLegoPart);
            }
            await _dbContext.SaveChangesAsync();
            return allParts;
        }
    





        public class RebrickableSearchResult
        {
            public List<RebrickableLegoSet> Results { get; set; } = new();
        }
        public class RebrickablePartsResult
        {
            public List<RebrickableLegoPart> Results { get; set; } = new();
            public string? Next { get; set; }
        }



        


        
        public async Task<Dictionary<int, int?>> GetUserPartsAsync()   //Query User's parts from DbLegoParts table
        
        {
            var parts = await _dbContext.DbLegoParts
                .GroupBy(p => p.InvPartId) 
                .Select(g => new { InvPartId = g.Key, Quantity = g.Sum(p => p.Quantity ?? 0) }) 
                .ToListAsync(); 

            return parts.ToDictionary(p => p.InvPartId, p => (int?)p.Quantity); 
        }




       

        



        private int GetTotalUserParts()
        {
            return _dbContext.DbLegoParts.Sum(p => p.Quantity) ?? 0;
        }






        //New method to fetch all sets and corresponding parts from the API.  Similar to SearchLegoSetsAsync but without a search query.
        public async Task<List<RebrickableLegoSetWithParts>> FetchAllSetsAndPartsAsync(int currentPage, int resultsPerPage)
        {
            Console.WriteLine("[DEBUG] FetchAllSetsAndPartsAsync started.");

            var allSetsWithParts = new List<RebrickableLegoSetWithParts>();
            var url = $"{BaseUrl}?key={_apiKey}&page={currentPage}&page_size={resultsPerPage}&inc_color_details=0";
            bool startAdding = false;

            try
            {
                var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);
                var currentResults = response?.Results ?? new List<RebrickableLegoSet>();

                if (currentResults.Count == 0)
                {
                    Console.WriteLine($"[DEBUG] No more results on page {currentPage}. Stopping search.");
                    return allSetsWithParts;
                }

                foreach (var set in currentResults)
                {
                    //if (set.set_num == "10001_1")
                    //{
                    //    startAdding = true;
                    //}

                    //if (startAdding)
                    //{
                        var parts = await FetchPartsForSetsAsync(set.set_num);
                        allSetsWithParts.Add(new RebrickableLegoSetWithParts
                        {
                            Set = set,
                            Parts = parts
                        });
                    //}
                }

                Console.WriteLine($"[DEBUG] FetchAllSetsAndPartsAsync: Found {currentResults.Count} sets on page {currentPage}.");


                await Task.Delay(1200);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] FetchAllSetsAndPartsAsync: API request failed on page {currentPage}: {ex.Message}");

            }


            Console.WriteLine($"[DEBUG] FetchAllSetsAndPartsAsync: Found {allSetsWithParts.Count} sets in total.");
            return allSetsWithParts;
        }
    }
}





//use HttpClient to connect to Rebrickable's API endpoints and search for Lego sets. Added API optimizations. 
//private async Task<List<RebrickableLegoSet>> SearchLegoSetsAsync(string searchQuery, int startPage = 1, int maxPages = 10, int resultsPerPage = 10)
//{
//    //debugging
//    Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: Searching for sets with query '{searchQuery}'.");


//    if (string.IsNullOrWhiteSpace(searchQuery))
//    {
//        //debugging
//        Console.WriteLine("[DEBUG] SearchLegoSetsAsync: Empty search query, returning no sets.");

//        return new List<RebrickableLegoSet>();
//    }

//    int currentPage = startPage;
//    var sets = new List<RebrickableLegoSet>();

//    while (currentPage < startPage + maxPages)
//    {
//        var url = $"{BaseUrl}?search={searchQuery}&key={_apiKey}&page={currentPage}&page_size={resultsPerPage}&inc_color_details=0";
//        Console.WriteLine($"[DEBUG] Fetching from url: {url}");

//        //debugging try/catch
//        try
//        {
//            var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);
//            var currentResults = response?.Results ?? new List<RebrickableLegoSet>();

//            if (currentResults.Count == 0)
//            {
//                Console.WriteLine($"[DEBUG] No more results on page {currentPage}. Stopping search.");
//                break;
//            }

//            sets.AddRange(currentResults);
//            Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: Found {currentResults.Count} sets on page {currentPage}.");

//            if (currentResults.Count < resultsPerPage)
//            {
//                Console.WriteLine($"[DEBUG] Page {currentPage} returned fewer than {resultsPerPage} results. No more pages available.");
//                break;
//            }

//            currentPage++;
//        }

//        catch (Exception ex)
//        {
//            Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: API request failed on page {currentPage}: {ex.Message}");
//            break; //exit the loop on error
//        }
//    }

//    Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: Found {sets.Count} sets in total.");
//    return sets;
//}





//private async Task<List<RebrickableLegoSet>> SearchLegoSetsAsync(string searchQuery)   //use HttpClient to connect to Rebrickable's API endpoints and search for Lego sets.  
//{

//    Console.WriteLine($"[DEBUG]  SearchLegoSetsAsync: Searching for sets with query '{searchQuery}'.");


//    if (string.IsNullOrWhiteSpace(searchQuery))
//    {
//        Console.WriteLine("[DEBUG] SearchLegoSetsAsync: Empty search query, returning no sets.");

//        return new List<RebrickableLegoSet>();
//    }

//    var url = $"{BaseUrl}?search={searchQuery}&key={_apiKey}";

//    try
//    {
//        var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);

//        if (response?.Results != null)
//        {
//            Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: Found {response.Results.Count} sets.");
//            return response.Results;
//        }
//        else
//        {
//            Console.WriteLine("[DEBUG] SearchLegoSetsAsync: No sets found.");
//            return new List<RebrickableLegoSet>();
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"[DEBUG] SearchLegoSetsAsync: API request failed: {ex.Message}");
//        return new List<RebrickableLegoSet>();
//    }                        
//}







//Find possible matches between Rebrickable's API and User's parts.  Filtering logic included. 
//    public async Task<List<RebrickableLegoSetWithParts>> FindMatchingSetsAsync()
//    {
//        Console.WriteLine("[DEBUG] FindMatchingSetsAsync started.");

//        //fetch user's parts from GetUserPartsAsync()
//        var userParts = await GetUserPartsAsync();

//        Console.WriteLine($"[DEBUG] User has {userParts.Count} parts.");

//        var matchingSets = new List<RebrickableLegoSetWithParts>();

//        int currentPage = 2;
//        int resultsPerPage = 1000;
//        int maxMatches = 10;

//        while (matchingSets.Count < maxMatches)
//        {
//            var allDtoSetsWithParts = await FetchAllSetsAndPartsAsync(currentPage, resultsPerPage);
//            if (allDtoSetsWithParts.Count == 0)
//            {
//                Console.WriteLine($"[DEBUG] No more results on page {currentPage}. Stopping search.");
//                break;
//            }

//            Console.WriteLine($"[DEBUG] Found {allDtoSetsWithParts.Count} sets from Rebrickable on page {currentPage}.");

//            foreach (var setWithParts in allDtoSetsWithParts) //Filtering logic
//            {
//                Console.WriteLine($"[DEBUG] Set Name: {setWithParts.Set.name}, Parts: {setWithParts.Set.num_parts}");

//                if (setWithParts.Set.num_parts <= 30)
//                {
//                    Console.WriteLine($"[DEBUG] Skipping set {setWithParts.Set.name} because it has too few parts.");
//                    continue;
//                }

//                string setNameLower = setWithParts.Set.name.ToLower();
//                if (setNameLower.Contains("minifigs") ||
//                    setNameLower.Contains("Figures") ||
//                    setNameLower.Contains("duplo") ||
//                    setNameLower.Contains("pack") ||
//                    setNameLower.Contains("dots") ||
//                    setNameLower.Contains("assorted") ||
//                    setNameLower.Contains("1:87"))

//                {
//                    Console.WriteLine($"[DEBUG] Skipping set {setWithParts.Set.name}.");
//                    continue;
//                }


//                bool partsMatch = true;

//                foreach (var part in setWithParts.Parts)
//                {
//                    if (!userParts.TryGetValue(part.inv_part_id, out int? userQuantity) || userQuantity < part.quantity)
//                    {
//                        partsMatch = false;
//                        break;
//                    }
//                }
//                if (partsMatch)
//                {
//                    Console.WriteLine($"[DEBUG] Found matching set: {setWithParts.Set.name}");
//                    matchingSets.Add(setWithParts);
//                    if (matchingSets.Count >= maxMatches) 
//                        break;
//                }
//            }
//            await Task.Delay(5000);
//            currentPage++;
//        }

//        Console.WriteLine($"[DEBUG] Found {matchingSets.Count} sets from Rebrickable.");
//        return matchingSets;
//    }



//    private int GetTotalUserParts()
//    {
//        return _dbContext.DbLegoParts.Sum(p => p.Quantity) ?? 0;
//    }



//    //New method to fetch all sets and corresponding parts from the API.  Similar to SearchLegoSetsAsync but without a search query.
//    public async Task<List<RebrickableLegoSetWithParts>> FetchAllSetsAndPartsAsync(int currentPage, int resultsPerPage)
//    { 
//        Console.WriteLine("[DEBUG] FetchAllSetsAndPartsAsync started.");

//        var allSetsWithParts = new List<RebrickableLegoSetWithParts>();
//        var url = $"{BaseUrl}?key={_apiKey}&page={currentPage}&page_size={resultsPerPage}&inc_color_details=0";

//        try
//        {
//            var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickableSearchResult>(url);
//            var currentResults = response?.Results ?? new List<RebrickableLegoSet>();

//            if (currentResults.Count == 0)
//            {
//                Console.WriteLine($"[DEBUG] No more results on page {currentPage}. Stopping search.");
//                return allSetsWithParts;
//            }

//            foreach (var set in currentResults)
//            {
//                var parts = await FetchPartsForSetsAsync(set.set_num);
//                allSetsWithParts.Add(new RebrickableLegoSetWithParts
//                {
//                    Set = set,
//                    Parts = parts
//                });
//            }

//            Console.WriteLine($"[DEBUG] FetchAllSetsAndPartsAsync: Found {currentResults.Count} sets on page {currentPage}.");


//            await Task.Delay(1200);

//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"[DEBUG] FetchAllSetsAndPartsAsync: API request failed on page {currentPage}: {ex.Message}");

//        }


//        Console.WriteLine($"[DEBUG] FetchAllSetsAndPartsAsync: Found {allSetsWithParts.Count} sets in total.");
//        return allSetsWithParts;
//    }
//}