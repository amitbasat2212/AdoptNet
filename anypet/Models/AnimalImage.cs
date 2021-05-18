using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace anypet.Models
{

    //this class is represanting the image of each animal 
    public class AnimalImage
    {
        public int Id { get; set; }

        //this is a connection of one to one 
        [Display(Name = "Image of the Pet")]
        public string Image { get; set; }

        [Display(Name = "Animal")]
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

    }
}
