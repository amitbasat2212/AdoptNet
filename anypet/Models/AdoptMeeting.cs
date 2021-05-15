using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace anypet.Models
{
    //a class for the adoption itself, when a person wants to adopt he spouuse to put details of him and a place to meet for the adopt 
    public class AdoptMeeting
    {
        //need to open as a diary to choose a date 

        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

              
        public  String Location { get; set; }

        [DataType(DataType.PhoneNumber)]
        public long PhoneNumber { get; set; }

        //we need to check if this is the write varibale -for time 
        [DataType(DataType.Time)]
        public int Time { get; set; }




    }
}
