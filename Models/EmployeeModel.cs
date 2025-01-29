using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class EmployeeModel
    {
       
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile Number of 10 digits required")]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email Is Not Valid")]
        public string email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int fk_ShiftId { get; set; }
        [Required]
        public string ShiftName { get; set; }
        public int fk_CompanyID { get; set; }
        [Required]
        public string CompanyName { get; set; }

        public int FK_Country_Id {  get; set; } 

        public int FK_State_Id { get; set; }
        public int FK_City_Id { get; set; }

        public string Country {  get; set; }    
        public String State { get; set; }
        public String City { get; set; }

        public int FK_ShiftEndReasonId { get; set; }

        public string Reason {  get; set; }
        

        public TimeSpan ShiftEndTime { get; set; }  

        //[DataType(DataType.Time)]
        //public DateTime ShiftEndTime { get; set; }



    //public string shiftname { get; set; }

}
}