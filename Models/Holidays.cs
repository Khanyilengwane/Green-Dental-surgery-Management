using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Holidays
    {
        [Key]
        [Display(Name ="Holiday")]
        public int HolidayId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime day { get; set; }
      
       

    }
}