namespace TheBrickVault.Core.Models
{
    public class Imported_inventory_sets
    {

        public int inventory_id { get; set; }
        public string set_num { get; set; }
        public int? quantity { get; set; }

        public Imported_Inventories Inventory { get; set; }
        public Imported_sets Set { get; set; }
    }

}
