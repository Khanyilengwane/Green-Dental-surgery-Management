using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class NameTooth
    {
        [Key]
        public int toothID { get; set; }

        public string toothName { get; set; }
    }
}