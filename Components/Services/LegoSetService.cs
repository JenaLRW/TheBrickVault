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

    //    public async Task<DbLegoSet> GetLegoSetByIdAsync(int id)
    //    {
    //        return await _context.DbLegoSets.FindAsync(id);
    //    }

    //    public async Task<List<DbLegoSet>> GetLegoSetsAsync()
    //    {
    //        return await _context.DbLegoSets.ToListAsync();
    //    }

    //    public async Task<IEnumerable<DbLegoSet>> GetAllLegoSetsAsync()
    //    {
    //        return await _context.DbLegoSets.ToListAsync();
    //    }
    //    public async Task AddLegoSetAsync(DbLegoSet legoSet)
    //    {
    //        _context.DbLegoSets.Add(legoSet);
    //        await _context.SaveChangesAsync();
    //    }
    //    public async Task UpdateLegoSetAsync(DbLegoSet legoSet)
    //    {
    //        _context.DbLegoSets.Update(legoSet);
    //        await _context.SaveChangesAsync();
    //    }
    //    public async Task DeleteLegoSetAsync(int id)
    //    {
    //        var legoSet = await GetLegoSetByIdAsync(id);
    //        if (legoSet != null)
    //        {
    //            _context.DbLegoSets.Remove(legoSet);
    //            await _context.SaveChangesAsync();
    //        }
    //    }
    //}
}
