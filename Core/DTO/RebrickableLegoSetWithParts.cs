namespace TheBrickVault.Core.DTO
{
    
        public class RebrickableLegoSetWithParts
        {
            public RebrickableLegoSet Set { get; set; }
            public List<RebrickableLegoPart> Parts { get; set; } = new List<RebrickableLegoPart>();
            // Initialize the list to avoid null reference exceptions

        }
    
}
