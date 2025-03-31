namespace TheBrickVault.Core.Models
{
    public class Imported_inventory_parts
    {
        [CsvHelper.Configuration.Attributes.Index(0)]
        public int inventory_id { get; set; }
        [CsvHelper.Configuration.Attributes.Index(1)]
        public int part_num { get; set; }
        [CsvHelper.Configuration.Attributes.Index(2)]
        public int? quantity { get; set; }
        
        public Imported_Inventories Inventory { get; set; }
        public Imported_parts Part { get; set; }
    }
}
