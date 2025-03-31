namespace TheBrickVault.Core.Models
{
    public class Imported_parts
    {
        [CsvHelper.Configuration.Attributes.Index(0)]
        public int part_num { get; set; }
        [CsvHelper.Configuration.Attributes.Index(1)]
        public string? name { get; set; }
        [CsvHelper.Configuration.Attributes.Index(2)]
        public string? part_cat_id { get; set; }
        [CsvHelper.Configuration.Attributes.Index(3)]
        public string? part_material { get; set; }

        public ICollection<Imported_inventory_parts> InventoryParts { get; set; } = new List<Imported_inventory_parts>();
    }
}
