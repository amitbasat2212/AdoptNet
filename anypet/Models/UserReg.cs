using AdoptNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace anypet.Models
{
    //the class for creating a user in the website - adoter and associatin, later we will create a diffrent acsses to each user 
    public class UserReg
    {
        [DataType(DataType.EmailAddress)]
        public String EmailOfUser { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set;}
    

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The Private name must contain only letters...")]
        public String PrivateName { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The last name must contain only letters...")]
        public String LastName { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "the age is only in numbers ")]
        [Range(18,110)]
        public int Age { get; set; }

        public String Address { get; set; }

        public Boolean ThereIsAnimal{ get; set; }

        public Boolean HaveYouAdopted{ get; set; }

        //making it only match for numbers around 9 digit only
        [RegularExpression("^[0-9]+$", ErrorMessage = "the id is not right ")]
        [StringLength(9, MinimumLength = 8)]
        public long Id{ get; set; }

        
        [DataType(DataType.DateTime)]
        public DateTime DateOfCreate { get; set; }
               
    }

}
