namespace TheBrickVault.Core.DTO
{
    public class RebrickableLegoPart
    {
        public string? set_num { get; set; }
        public string? name { get; set; }
        public string? part_num { get; set; }

        public int inv_part_id { get; set; } // must be an integer due to API. 
        public int quantity { get; set; }
    }
}
