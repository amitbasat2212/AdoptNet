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
        public int Id{ get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The name must contain only letters...")]
        public String Name { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Association Phone")]
        public long PhoneNumber { get; set; }

        [Display(Name = "Association Location")]
        public Location Location { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Association Email")]
        public String EmailOfUser { get; set; }

        //this field represnt the connection of many to one 
      
        public List<Animal> Animals { get; set; }    

        public AssociationImage Image { get; set; }

        //this field represent the connection of many to many 
        public List<AdoptionDays> AdoptionDays { get; set; }



    }
}
