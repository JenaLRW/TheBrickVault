using Microsoft.EntityFrameworkCore;
using TheBrickVault.Infrastructure.Data;
using TheBrickVault.Core.Models;



namespace TheBrickVault.Components.Services
{
    public class LegoSetService(LegoDbContext context)
    {
        private readonly LegoDbContext _context = context;

        public async Task<LegoSet> GetLegoSetByIdAsync(int id)
        {
            return await _context.LegoSets.FindAsync(id);
        }

        public async Task<List<LegoSet>> GetLegoSetsAsync()
        {
            return await _context.LegoSets.ToListAsync();
        }

        public async Task<IEnumerable<LegoSet>> GetAllLegoSetsAsync()
        {
            return await _context.LegoSets.ToListAsync();
        }
        public async Task AddLegoSetAsync(LegoSet legoSet)
        {
            _context.LegoSets.Add(legoSet);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLegoSetAsync(LegoSet legoSet)
        {
            _context.LegoSets.Update(legoSet);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLegoSetAsync(int id)
        {
            var legoSet = await GetLegoSetByIdAsync(id);
            if (legoSet != null)
            {
                _context.LegoSets.Remove(legoSet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
