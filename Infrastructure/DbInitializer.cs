using Microsoft.VisualBasic;
using TheBrickVault.Core.Models;
using TheBrickVault.Infrastructure.Data;

namespace TheBrickVault.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(LegoDbContext context)
        {
            if (context.LegoSets.Any())
                return;

            var legoSets = new List<LegoSet>
            {  
            new LegoSet { Id = 1, Name = "Harry Potter", SetNum = "78945", PieceCount = 4654 },
            new LegoSet { Id = 2, Name = "Star Wars", SetNum = "457245", PieceCount = 244 }

            };

            context.LegoSets.AddRange(legoSets);
            context.SaveChanges();
        }
    }
}
