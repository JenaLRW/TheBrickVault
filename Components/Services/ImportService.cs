using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;
using TheBrickVault.Core.DTO;
using TheBrickVault.Core.Models;
using TheBrickVault.Infrastructure.Data;


namespace TheBrickVault.Components.Services
{
    public class ImportService
    {
        //service to import .csv files into the database.
        private readonly LegoDbContext _dbContext;
        private readonly RebrickableService _rebrickableService;

        public ImportService(LegoDbContext dbContext, RebrickableService rebrickableService)
        {
            _dbContext = dbContext;
            _rebrickableService = rebrickableService;
        }





        public async Task ImportDataAsync()
        {
            await ImportSetsAsync("ImportedData/Imported_sets.csv");
            await ImportPartsAsync("ImportedData/Imported_parts.csv");
            await ImportInventoriesAsync("ImportedData/Imported_Inventories.csv");
            await ImportInventoryPartsAsync("ImportedData/Imported_inventory_parts.csv");
            await ImportInventorySetsAsync("ImportedData/Imported_inventory_sets.csv");

        }



        private async Task ImportSetsAsync(string filePath)
        {
            if (!File.Exists(filePath) || _dbContext.ImportedSets.Any()) return;

            using var reader = new StreamReader(filePath);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));

            var sets = csv.GetRecords<Imported_sets>().ToList();
            await _dbContext.ImportedSets.AddRangeAsync(sets);
            await _dbContext.SaveChangesAsync();
        }



        private async Task ImportPartsAsync(string filePath)
        {
            if (!File.Exists(filePath) || _dbContext.ImportedParts.Any()) return;
            using var reader = new StreamReader(filePath);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            var parts = csv.GetRecords<Imported_parts>().ToList();
            await _dbContext.ImportedParts.AddRangeAsync(parts);
            await _dbContext.SaveChangesAsync();
        }




        private async Task ImportInventoriesAsync(string filePath)
        {
            if (!File.Exists(filePath) || _dbContext.ImportedInventories.Any()) return;
            using var reader = new StreamReader(filePath);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            var inventories = csv.GetRecords<Imported_Inventories>().ToList();
            await _dbContext.ImportedInventories.AddRangeAsync(inventories);
            await _dbContext.SaveChangesAsync();
        }




        private async Task ImportInventoryPartsAsync(string filePath)
        {
            if (!File.Exists(filePath) || _dbContext.ImportedInventoryParts.Any()) return;
            using var reader = new StreamReader(filePath);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            var inventoryParts = csv.GetRecords<Imported_inventory_parts>().ToList();
            await _dbContext.ImportedInventoryParts.AddRangeAsync(inventoryParts);
            await _dbContext.SaveChangesAsync();
        }





        private async Task ImportInventorySetsAsync(string filePath)
        {
            if (!File.Exists(filePath) || _dbContext.ImportedInventorySets.Any()) return;
            using var reader = new StreamReader(filePath);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            var inventorySets = csv.GetRecords<Imported_inventory_sets>().ToList();
            await _dbContext.ImportedInventorySets.AddRangeAsync(inventorySets);
            await _dbContext.SaveChangesAsync();
        }





        public async Task<List<Imported_sets>> GetImportedSetsAsync()
        {
            return await _dbContext.ImportedSets.ToListAsync();
        }






        //Method to find matching sets in the Imported data based on the user's parts.

        public async Task<List<DbLegoSet>> FindMatchingImportedSetsAsync()
        {
            var userParts = await _rebrickableService.GetUserPartsAsync();

            var importedSets = await _dbContext.ImportedSets.ToListAsync();

            var matchingSets = new List<DbLegoSet>();

            foreach (var set in importedSets)
            {
                var requiredParts = await _dbContext.DbLegoParts
                    .Where(p => p.PartNum == set.set_num)
                    .ToListAsync();
                
                bool canBuild = true;

                foreach (var part in requiredParts)
                {
                    if (!userParts.TryGetValue(part.InvPartId, out int? userQuantity) || userQuantity < part.Quantity)
                    {
                        canBuild = false;
                        break;
                    }
                }

                if (canBuild)
                {
                    matchingSets.Add(new DbLegoSet
                    {
                        SetNum = set.set_num,
                        Name = set.name,
                        PieceCount = set.num_parts

                    });
                }
            }
            return matchingSets;
        }
        //public async Task<List<RebrickableLegoSetWithParts>> FindMatchingSetsAsync(int currentPage = 1, int resultsPerPage = 1000)
        //{
        //    Console.WriteLine("[DEBUG] FindMatchingSetsAsync started.");

        //    var userParts = await GetUserPartsAsync(); //fetch user's parts from GetUserPartsAsync()

        //    Console.WriteLine($"[DEBUG] User has {userParts.Values.Sum(q => q ?? 0)} parts.");

        //    var allDtoSetsWithParts = await FetchAllSetsAndPartsAsync(currentPage, resultsPerPage);

        //    Console.WriteLine($"[DEBUG] Found {allDtoSetsWithParts.Count} sets from Rebrickable on page {currentPage}.");

        //    var matchingSets = new List<RebrickableLegoSetWithParts>();

        //    foreach (var setWithParts in allDtoSetsWithParts) //Filtering logic
        //    {
        //        Console.WriteLine($"[DEBUG] Set Name: {setWithParts.Set.name}, Parts: {setWithParts.Set.num_parts}");

        //        if (setWithParts.Set.num_parts <= 30)
        //        {
        //            Console.WriteLine($"[DEBUG] Skipping set {setWithParts.Set.name} because it has too few parts.");
        //            continue;
        //        }

        //        string setNameLower = setWithParts.Set.name.ToLower();
        //        if (setNameLower.Contains("minifigs") ||
        //            setNameLower.Contains("Figures") ||
        //            setNameLower.Contains("duplo") ||
        //            setNameLower.Contains("pack") ||
        //            setNameLower.Contains("dots") ||
        //            setNameLower.Contains("assorted") ||
        //            setNameLower.Contains("1:87"))

        //        {
        //            Console.WriteLine($"[DEBUG] Skipping set {setWithParts.Set.name}.");
        //            continue;
        //        }


        //        bool partsMatch = true;

        //        foreach (var part in setWithParts.Parts)
        //        {
        //            if (!userParts.TryGetValue(part.inv_part_id, out int? userQuantity) || userQuantity < part.quantity)
        //            {
        //                partsMatch = false;
        //                break;
        //            }
        //        }
        //        if (partsMatch)
        //        {
        //            Console.WriteLine($"[DEBUG] Found matching set: {setWithParts.Set.name}");
        //            matchingSets.Add(setWithParts);
        //        }
        //    }

        //    Console.WriteLine($"[DEBUG] Found {matchingSets.Count} sets from Rebrickable.");
        //    return matchingSets;

        //}
    }
}
