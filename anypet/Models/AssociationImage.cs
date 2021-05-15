using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anypet.Models
{
    //this class is represent the image of each association
    public class AssociationImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int AssociationId { get; set; }
        //this field represent the connection of one to one 
        public Association Association { get; set; }
    }
}
