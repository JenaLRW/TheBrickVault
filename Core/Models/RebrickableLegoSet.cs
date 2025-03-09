namespace TheBrickVault.Core.Models
{
    //This is a model that represents a Lego set retrived from the Rebrickable.com API.  This helps structure data
    //when searching for Lego sets through Rebrickable and storing relevant information in the db.
    public class RebrickableLegoSet
    {
        public string? set_num { get; set; }
        public string? name { get; set; }
        public int? year { get; set; }
        public int? num_parts { get; set; }
        public string? set_img_url { get; set; }
    }
}
