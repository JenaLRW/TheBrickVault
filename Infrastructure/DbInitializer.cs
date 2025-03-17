using Microsoft.VisualBasic;
using TheBrickVault.Core.Models;
using TheBrickVault.Infrastructure.Data;

namespace TheBrickVault.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(LegoDbContext context)
        {
            if (context.DbLegoSets.Any())
                return;

            var legoSets = new List<DbLegoSet>

            {
            new DbLegoSet { Id = 1, Name = "Harry Potter", SetNum = "78945", PieceCount = 4654 },
            new DbLegoSet { Id = 2, Name = "Star Wars", SetNum = "457245", PieceCount = 244 }

            };

            context.DbLegoSets.AddRange(legoSets);
            context.SaveChanges();
        }
    }
}
