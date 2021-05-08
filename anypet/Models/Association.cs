using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdoptNet.Models;

namespace anypet.Models
{

    //the information for the association itself, the information about each one 
    public class Association
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The name must contain only letters...")]
        public String Name { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The Location must contain only letters...")]
        public String Location { get; set; }

        [DataType(DataType.EmailAddress)]
        public String EmailOfUser { get; set; }

        //this field represnt the connection of many to one 
        public List<Animal> AssociationAnimals { get; set; }

        public AssociationImage associationImage { get; set; }

        //this field represent the connection of many to many 
        public List<AdoptionDays> adoptDays { get; set; }



    }
}
