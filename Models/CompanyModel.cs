using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class CompanyModel
    {

       
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public string Address { get; set; }
        //[Required]
        //[StringLength(6, MinimumLength = 6, ErrorMessage = "PinCode must be exactly 6 digits.")]
        public string PinCode { get; set; }

      

        public string State { get; set; }

        public string City { get; set; }

        //[Required]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile Number of 10 digits required")]
        public string ContactNumber { get; set; }
    }
}