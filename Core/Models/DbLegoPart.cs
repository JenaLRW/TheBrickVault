using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBrickVault.Core.Models
{
    public class DbLegoPart
    {
        [Key]
        public int Id { get; set; } 
        public int InvPartId { get; set; } 
        public string SetNum { get; set; } 

        public string? PartNum { get; set; }
        
        //public string? Name { get; set; }

        public int? Quantity { get; set; }

       

    }
}
