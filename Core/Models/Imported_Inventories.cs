namespace TheBrickVault.Core.Models
{
    public class Imported_Inventories
    {
        public int Id { get; set; } 
        public string? version { get; set; }
        public string set_num { get; set; }

        public Imported_sets Set { get; set; }
        public ICollection<Imported_inventory_parts> InventoryParts { get; set; } = new List<Imported_inventory_parts>();
        public ICollection<Imported_inventory_sets> InventorySets { get; set; } = new List<Imported_inventory_sets>();

    }
}
