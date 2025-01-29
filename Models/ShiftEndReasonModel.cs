using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class ShiftEndReasonModel
    {

        public int FK_ShiftEndReasonId { get; set; }

        public string Reason { get; set; }

        [DataType(DataType.Time)]
        public DateTime ShiftEndTime { get; set; }

    }
}