using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Teeth
    {
        [Key]
        public string TeethID { get; set; }
        public string TeethName { get; set; }
        public int ProcedurID { get; set; }
        public virtual Procedure Procedure { get; set; }

    }
}