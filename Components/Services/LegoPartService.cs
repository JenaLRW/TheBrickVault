using TheBrickVault.Infrastructure.Data;
using TheBrickVault.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace TheBrickVault.Components.Services
{
    public class LegoPartService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiKey;
        private const string BaseUrl = $"https://rebrickable.com/get/api/v3/lego/sets/";       
        private readonly LegoDbContext _dbContext;
        public LegoPartService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _apiKey = configuration["Rebrickable:ApiKey"];
        }
        public async Task<List<LegoPart>> GetLegoPartsAsync(string setNum)
        {
            return await _dbContext.LegoParts
                .Where(p => p.LegoSet.SetNum == setNum)
                .ToListAsync();
        }
        public async Task AddLegoPartAsync(LegoPart legoPart)
        {
            _dbContext.LegoParts.Add(legoPart);
            await _dbContext.SaveChangesAsync();
        }
    }
}
