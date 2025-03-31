using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBrickVault.Core.Models
{

    //this is a model that represents a lego set stored in the app's db. This is used to store and manage Lego sets 
    //in the user's collection separate from the Rebrickable API data.
    public class DbLegoSet
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string SetNum { get; set; }   
        public string? Images { get; set; }  
        public int? PieceCount { get; set; }
        public int? Instructions { get; set; } 
        public int? PartsList { get; set; } 
        public ICollection<DbLegoPart> ListOfParts { get; set; } = new List<DbLegoPart>(); //this is a navigation property to DbLegoPart.

    }
} 
