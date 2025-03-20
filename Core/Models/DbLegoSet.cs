using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBrickVault.Core.Models
{

    //this is a model that represents a lego set stored in the app's db. This is used to store and manage Lego sets 
    //in the user's collection separate from the Rebrickable API data.
    public class DbLegoSet
    {
        public int Id { get; set; } //Primary key
        public string? Name { get; set; }
        public string SetNum { get; set; }  //unique identifier for the Lego set.  This is also a foreign key to DbLegoPart.
        public string? Images { get; set; }  //NOT IMPORTANT RIGHT NOW
        public int? PieceCount { get; set; }
        public int? Instructions { get; set; } //might need to change type to string.
        public int? PartsList { get; set; } //not sure if type is int or string. 
        public ICollection<DbLegoPart> ListOfParts { get; set; } = new List<DbLegoPart>(); //this is a navigation property to DbLegoPart.

    }
} 
