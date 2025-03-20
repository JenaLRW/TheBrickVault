using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBrickVault.Core.Models
{
    public class DbLegoPart
    {
        //public int Id { get; set; } this is no longer needed because SetNum is the primary key.


        [Key]
        public string SetNum { get; set; } //Primary key and foreign key to DbLegoSet
        public string? PartNum { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }

        [ForeignKey("SetNum")]
        public DbLegoSet LegoSet { get; set; } //Navigation property to SetNum


        //public string PartImageUrl { get; set; } maybe explore this later if time permits.

    }
}
