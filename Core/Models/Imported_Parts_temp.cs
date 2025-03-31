namespace TheBrickVault.Core.Models
{
    public class Imported_Parts
    {
        public int part_num { get; set; }
        public string? name { get; set; }
        public string? part_cat_id { get; set; }
        public string? part_material { get; set; }

        public ICollection<Imported_inventory_parts> InventoryParts { get; set; } = new List<Imported_inventory_parts>();
    }
}
