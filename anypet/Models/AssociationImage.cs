using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace anypet.Models
{
    //this class is represent the image of each association
    public class AssociationImage
    {
        public int Id { get; set; }
        [Display(Name = "Association Image")]
        public string Image { get; set; }

        [Display(Name = "Association")]
        public int AssociationId { get; set; }
        //this field represent the connection of one to one 
        public Association Association { get; set; }
    }
}
