using AdoptNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace anypet.Models
{
    //the class itself for the cats,dogs an so on , the details about each pet 
    public class Animal
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The name  must contain only letters...")]
        public String Name { get; set; }

        
        //need to varify this varibale but still dont know  how 
        //0 is a cat and 1 is a dog 
        public int Kind { get; set; }


        [RegularExpression("^[0-9]+$", ErrorMessage = "the age is only in numbers ")]
        [Range(0, 110)]
        public int Age { get; set; }


        //need to check this with validation- maby making it a list of female and male** 
        public String Gender { get; set; }


        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The Description must contain only letters...")]
        public String Description  { get; set; }


        //need to ne a list of small,medium and big 
        public LinkedList<String> Size { get; set; }

        public LinkedList<String> Location { get; set; }


        //this field represnt the connection of many to one 
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The Association must contain only letters...")]
        public Association association { get; set; } 

        public int IdAssociation { get; set; }

              
        //this field represent the connection of one to one 
        public AnimalImage imageAnimal { get; set; }

       




    }
}