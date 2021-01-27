using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class ProcedureList
    {
        [Key]
        public int ProID { get; set; }

        public int ProcedurID { get; set; }
        public virtual Procedure Procedure { get; set; }

        public string ToothName { get; set; }

    }
}