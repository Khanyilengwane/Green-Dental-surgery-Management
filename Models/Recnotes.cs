using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Recnotes
    {

        [Key]
        public int RecID { get; set; }
        public int AppointmentID { get; set; }
        public virtual AppointmentModel AppointmentModel { get; set; }
        
        public string DoctorID { get; set; }

        public string UserID { get; set; }
        [Display(Name = "SideNotes")]
        [DataType(DataType.MultilineText)]
        public string SideNotes { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string ProcedureName { get; set; }

        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }

        public bool Upload { get; set; }
        public string Tooth { get; set; }

    }
}