using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class ShiftModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Time)]
        public DateTime start_time { get; set; }

        [DataType(DataType.Time)]
        public DateTime end_time { get; set; }

        public string companyName { get; set; }

        [Required]
        public int Company { get; set; } // Company ID 

        public string imagepath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; } // For uploading image
    }
}