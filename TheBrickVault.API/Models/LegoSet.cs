﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBrickVault.Core.Models
{
    public class LegoSet
    {
        public int Id { get; set; } //might delete this if SetNumber is sufficient. Or use this to number/list the current sets the user has. 
        public string Name { get; set; }
        //do SetNumber first and only to see the Db build successfully then add the rest. 
        public int SetNum { get; set; }
        public string Images {  get; set; }
        public int PieceCount { get; set; }
           
        public int Instructions {  get; set; } //might need to change type to string.
        public int PartsList { get; set; } //not sure if type is int or string. 

    }
}
