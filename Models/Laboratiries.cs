using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Laboratiries
    {
        [Key]
        [Display(Name ="Lab Name")]
        public int LabID { get; set; }
       // [Display(Name = "Lab Name")]

        public string LabName { get; set; }

        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }

    }
}