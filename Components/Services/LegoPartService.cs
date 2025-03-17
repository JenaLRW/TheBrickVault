using TheBrickVault.Infrastructure.Data;
using TheBrickVault.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;


namespace TheBrickVault.Components.Services
{
    //public class LegoPartService
    //{
    //    private readonly IHttpClientFactory _clientFactory;
    //    private readonly string _apiKey;
    //    private const string BaseUrl = $"https://rebrickable.com/get/api/v3/lego/sets/";
    //    private readonly LegoDbContext _dbContext;
    //    public LegoPartService(IHttpClientFactory clientFactory, IConfiguration configuration, LegoDbContext dbContext)
    //    {
    //        _clientFactory = clientFactory;
    //        _apiKey = configuration["Rebrickable:ApiKey"];
    //        _dbContext = dbContext;
    //    }

    //    //fetching saved sets from user's input
    //    public async Task<List<DbLegoSet>> GetSavedLegoSetsAsync()
    //    {
    //        return await _dbContext.DbLegoSets.ToListAsync();
    //    }

    //    //fetching parts for the saved sets
    //    public async Task<List<DbLegoPart>> GetPartsForSetAsync(string setNum)
    //    {
    //        return await _dbContext.DbLegoParts
    //            .Where(p => p.SetNum == setNum)
    //            .ToListAsync();
    //    }

    //    //fetching parts for a set from Rebrickable's API
    //    public async Task<List<DbLegoPart>> SearchPartsForSetAsync(string setNum)
    //    {
    //        if (string.IsNullOrWhiteSpace(setNum))
    //        {
    //            return new List<DbLegoPart>();
    //        }

    //        var ul = $"{BaseUrl}{setNum}/parts/?key={_apiKey}";

    //        var response = await _clientFactory.CreateClient().GetFromJsonAsync<RebrickablePartResult>(ul);
    //        return response?.Results ?? new List<DbLegoPart>();
    //    }

    //        //testing the fetch, printing out a list of parts
    //        //public async Task PrintLegoPartsDataAsync(string setNum)
    //        //{
    //        //    var parts = await GetPartsForSetAsync(setNum);
    //        //    Console.WriteLine($"Lego Parts for Set Number: {setNum}");
    //        //    foreach (var part in parts)
    //        //    {
    //        //        Console.WriteLine($"Part Number: {part.PartNum}, Set Number: {part.SetNum}");
    //        //    }
    //        //}

    //    public class RebrickablePartResult
    //    {
    //        public List<DbLegoPart> Results { get; set; }
    //    }
       
    //}
}
