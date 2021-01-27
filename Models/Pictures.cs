using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Pictur
    {
        [Key]
        public int PicID { get; set; }
        public int AppointmentID { get; set; }

        public string DoctorID { get; set; }

        public string UserID { get; set; }
      
        public string SideNotes { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string ProcedureName { get; set; }

        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        [Display(Name ="Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
}

 