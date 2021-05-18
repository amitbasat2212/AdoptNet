using anypet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptNet.Models
{

    //this class represent the adoption days and its fields
    public class AdoptionDays
    {

        public int Id { get; set; }

        [Display(Name = "Adoption Day")]
        public DateTime AdoptionDate { get; set; }

       //this field represent the connection of many to many 
        public List<Association> Associations { get; set; }

        [Display(Name = "Location of the Adoption Day")]
        public Location LocationAdopt { get; set; }

        

    }
}
