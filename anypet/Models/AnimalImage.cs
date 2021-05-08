using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anypet.Models
{

    //this class is represanting the image of each animal 
    public class AnimalImage
    {
        public int Id { get; set; }

        //this is a connection of one to one 
        public string Image { get; set; }
        public int AnimalId { get; set; }
        public Animal animal { get; set; }

    }
}
