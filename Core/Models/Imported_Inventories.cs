namespace TheBrickVault.Core.Models
{
    public class Imported_Inventories
    {
        [CsvHelper.Configuration.Attributes.Index(0)]
        public int Id { get; set; }
        [CsvHelper.Configuration.Attributes.Index(1)]
        public string? version { get; set; }
        [CsvHelper.Configuration.Attributes.Index(2)]
        public string set_num { get; set; }

        public Imported_sets Set { get; set; }
        public ICollection<Imported_inventory_parts> InventoryParts { get; set; } = new List<Imported_inventory_parts>();
        public ICollection<Imported_inventory_sets> InventorySets { get; set; } = new List<Imported_inventory_sets>();

    }
}
