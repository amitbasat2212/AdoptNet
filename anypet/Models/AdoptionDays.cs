using anypet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptNet.Models
{

    //this class represent the adoption days and its fields
    public class AdoptionDays
    {

        public int Id { get; set; }
        public DateTime AdoptionDate { get; set; }

       //this field represent the connection of many to many 
        public List<Association> associationsAdopt { get; set; }

        public String LocationAdopt { get; set; }

        

    }
}
