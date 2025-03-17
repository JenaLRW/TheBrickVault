using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBrickVault.Core.Models
{

    //this is a model that represents a lego set stored in the app's db. This is used to store and manage Lego sets 
    //in the user's collection separate from the Rebrickable API data.
    public class DbLegoSet
    {
        public int Id { get; set; } //might delete this if SetNum is sufficient. Or use this to number/list the current sets the user has. 
        public string? Name { get; set; }
        //do SetNum first and only to see the Db build successfully then add the rest. 
        public string SetNum { get; set; }  //had to set it as string

        public string? Images { get; set; }  //NOT IMPORTANT RIGHT NOW
        public int? PieceCount { get; set; }

        public int? Instructions { get; set; } //might need to change type to string.
        public int? PartsList { get; set; } //not sure if type is int or string. 

        public ICollection<DbLegoPart> Parts { get; set; } = new List<DbLegoPart>(); //this is a navigation property to DbLegoPart.

    }
} 
