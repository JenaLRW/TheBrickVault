namespace TheBrickVault.Core.Models
{
    public class imported_inventory_parts
    {
        public int inventory_id { get; set; }
        public int part_num { get; set; }
        public int? quantity { get; set; }
        public string? color_id { get; set; }
        public string? is_spare { get; set; }
        
        public string? img_url { get; set; } 
    }
}
