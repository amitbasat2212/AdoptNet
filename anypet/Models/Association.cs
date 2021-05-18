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
        public int ID{ get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The name must contain only letters...")]
        public String Name { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }

        public Location Location { get; set; }

        [DataType(DataType.EmailAddress)]
        public String EmailOfUser { get; set; }

        //this field represnt the connection of many to one 
        [Display(Name = "the animals in the Association ")]
        public List<Animal> AssociationAnimals { get; set; }    

        public AssociationImage AssociationImage { get; set; }

        //this field represent the connection of many to many 
        public List<AdoptionDays> AdoptionDays { get; set; }



    }
}
