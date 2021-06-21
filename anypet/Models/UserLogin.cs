using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptNet.Models
{
    public class UserLogin
    {
        [DataType(DataType.EmailAddress)]
        public String EmailOfUser { get; set; }

       [DataType(DataType.Password)]
       public String Password { get; set; }

    }
}
