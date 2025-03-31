namespace TheBrickVault.Core.Models
{
    public class Imported_inventory_parts
    {
        public int inventory_id { get; set; }
        public int part_num { get; set; }
        public int? quantity { get; set; }

        public Imported_Inventories Inventory { get; set; }
        public Imported_parts Part { get; set; }
    }

}
