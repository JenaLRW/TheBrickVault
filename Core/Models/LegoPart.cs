using System.ComponentModel.DataAnnotations.Schema;

namespace TheBrickVault.Core.Models
{
    public class LegoPart
    {
        public int Id { get; set; }

        public string SetNum { get; set; } //migrations keep on renaming the column to LegoSetId. WWWHHHYYYY?
        public string? PartNum { get; set; }

        //public string PartImageUrl { get; set; } maybe explore this later if time permits.

        [ForeignKey("SetNum")]
        public LegoSet LegoSet { get; set; } //Navigation property to SetNum
    }
}
