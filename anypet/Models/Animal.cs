using AdoptNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace anypet.Models
{


    public enum Size
    {
        [Description("Small")]
        Small,
        [Description("medium")]
        Medium,
        [Description("Big")]
        Big


    }
    public enum Kind
    {
        Dog,
        Cat

    }

    public enum Location
    {
        Center,
        North,
        South
    }
    public enum Gender
    {
       Male,
       Feamle
    }



    //the class itself for the cats,dogs an so on , the details about each pet 
    public class Animal
    {
        public int Id { get; set; }


        [RegularExpression(@"^[a-zA-Z_ ]+$", ErrorMessage = "The name  must contain only letters...")]
        [Required(ErrorMessage = "You must input Name of the Animal")]
        public String Name { get; set; }

        [Display(Name = "species")]
        public Kind Kind { get; set; }


        [RegularExpression("^[0-9]+$", ErrorMessage = "the age is only in numbers ")]
        [Required(ErrorMessage = "You must input Age of the Animal")]
        [Range(0, 110)]
        public uint Age { get; set; }


        //need to check this with validation- maby making it a list of female and male** 
        [Required(ErrorMessage = "You must input the Gender of the Animal")]
        public Gender Gender { get; set; }


        [RegularExpression(@"^[a-zA-Z\s ]+$", ErrorMessage = "The Description must contain only letters...")]
        public String Description { get; set; }


        //need to ne a list of small,medium and big 
        [Required(ErrorMessage = "You must input the Size of the Animal")]
        public Size Size { get; set; }

        [Required(ErrorMessage = "You must input the Location of the Association")]
        public Location Location { get; set; }


        //this field represnt the connection of many to one 
        [Display(Name = "Association")]
        public int AssociationId { get; set; }


        public Association Association { get; set; }


        //this field represent the connection of one to one 
        public AnimalImage AnimalImage { get; set; }


        public Products AnimalProducts { get; set; }








    }
}












