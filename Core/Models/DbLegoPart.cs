using System.ComponentModel.DataAnnotations.Schema;

namespace TheBrickVault.Core.Models
{
    public class DbLegoPart
    {
        public int Id { get; set; }
        [ForeignKey("SetNum")]
        public string SetNum { get; set; } //migrations keep on renaming the column to LegoSetId. WWWHHHYYYY?
        public string? PartNum { get; set; }

        //public string PartImageUrl { get; set; } maybe explore this later if time permits.


        public DbLegoSet LegoSet { get; set; } //Navigation property to SetNum


    }
}
