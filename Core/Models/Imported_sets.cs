namespace TheBrickVault.Core.Models
{
    public class Imported_sets
    {
        public string set_num { get; set; }
        public string? name { get; set; }
        public int? year { get; set; }
        public string? theme_id { get; set; }
        public int? num_parts { get; set; }
        public string? img_url { get; set; }

        public ICollection<Imported_Inventories>? Inventories { get; set; } = new List<Imported_Inventories>();
        public ICollection<Imported_inventory_sets> InventorySets { get; set; } = new List<Imported_inventory_sets>();
    }
}
