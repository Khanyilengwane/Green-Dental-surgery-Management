using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Green.Models
{
    public class Procedure
    {
        [Key]
     
        public int ProcedurID { get; set; }
        public string ProcedureName { get; set; }
        public string Value { get; set; }
        public double Price { get; set; }
        public virtual List<AppointmentModel> Appointments { get; set; }


    }
}