using AdoptNet.Models;
using anypet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace anypet.Models
{

    public enum Food
    {
        Chicken,
        Meat,
        Bonzo,

    }
    public class Products
    {
        public int Id { get; set; }

        public Food Food { get; set; }

        [RegularExpression(@"^[a-zA-Z_ ]+$", ErrorMessage = "The Toy  must contain only letters...")]
        public String Toy { get; set; }


        [RegularExpression(@"^[a-zA-Z_ ]+$", ErrorMessage = "The Toy  must contain only letters...")]
        public String Medicine { get; set; }

        public Animal Animal { get; set; }

        [Display(Name = "Animal name")]
        public int AnimalId { get; set; }

    }
}