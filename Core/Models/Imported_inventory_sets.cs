namespace TheBrickVault.Core.Models
{
    public class Imported_inventory_sets
    {
        [CsvHelper.Configuration.Attributes.Index(0)]
        public int inventory_id { get; set; }
        [CsvHelper.Configuration.Attributes.Index(1)]
        public string set_num { get; set; }
        [CsvHelper.Configuration.Attributes.Index(2)]
        public int? quantity { get; set; }

        public Imported_Inventories Inventory { get; set; }
        public Imported_sets Set { get; set; }
    }

}
