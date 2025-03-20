using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBrickVault.Core.Models
{
    public class DbLegoPart
    {
        [Key]
        public int Id { get; set; } 
        public string SetNum { get; set; } //no longer a foreign key
        public string? PartNum { get; set; }
        //public string? Name { get; set; }
        public int? Quantity { get; set; }

        //[ForeignKey("SetNum")]
        //public DbLegoSet LegoSet { get; set; } //Navigation property to SetNum


        //public string PartImageUrl { get; set; } maybe explore this later if time permits.

    }
}
