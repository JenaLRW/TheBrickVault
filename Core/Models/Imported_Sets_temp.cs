namespace TheBrickVault.Core.Models
{
    public class Imported_sets
    {
        [CsvHelper.Configuration.Attributes.Index(0)]
        public string set_num { get; set; }
        [CsvHelper.Configuration.Attributes.Index(1)]
        public string? name { get; set; }
        [CsvHelper.Configuration.Attributes.Index(2)]
        public int? num_parts { get; set; }
        
        //[CsvHelper.Configuration.Attributes.Index(3)]
        //public int? year { get; set; }
        //[CsvHelper.Configuration.Attributes.Index(4)]
        //public string? theme_id { get; set; }
        
        //[CsvHelper.Configuration.Attributes.Index(5)]
        //public string? img_url { get; set; }

        public ICollection<Imported_Inventories>? Inventories { get; set; } = new List<Imported_Inventories>();
        public ICollection<Imported_inventory_sets> InventorySets { get; set; } = new List<Imported_inventory_sets>();
    }
}
