using Microsoft.EntityFrameworkCore;
using TheBrickVault.Infrastructure.Data;
using TheBrickVault.Core.Models;
using System.Net.Http;
using System.Net.Http.Json;




namespace TheBrickVault.Components.Services
{


    // old design.  trying a new design and keeping old design in case the new is a fail.

    //public class LegoSetService(LegoDbContext context)
    //{
    //    private readonly LegoDbContext _context = context;

    //    public async Task<LegoSet> GetLegoSetByIdAsync(int id)
    //    {
    //        return await _context.LegoSets.FindAsync(id);
    //    }

    //    public async Task<List<LegoSet>> GetLegoSetsAsync()
    //    {
    //        return await _context.LegoSets.ToListAsync();
    //    }

    //    public async Task<IEnumerable<LegoSet>> GetAllLegoSetsAsync()
    //    {
    //        return await _context.LegoSets.ToListAsync();
    //    }
    //    public async Task AddLegoSetAsync(LegoSet legoSet)
    //    {
    //        _context.LegoSets.Add(legoSet);
    //        await _context.SaveChangesAsync();
    //    }
    //    public async Task UpdateLegoSetAsync(LegoSet legoSet)
    //    {
    //        _context.LegoSets.Update(legoSet);
    //        await _context.SaveChangesAsync();
    //    }
    //    public async Task DeleteLegoSetAsync(int id)
    //    {
    //        var legoSet = await GetLegoSetByIdAsync(id);
    //        if (legoSet != null)
    //        {
    //            _context.LegoSets.Remove(legoSet);
    //            await _context.SaveChangesAsync();
    //        }
    //    }
    //}
}
