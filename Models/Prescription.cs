using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Prescription
    {
        [Key]
        public int prescriptionId { get; set; }
        public int AppointmentID { get; set; }
       // public int timesPerDay { get; set; }
        public string DoctorID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string UserID { get; set; }
        // public int unitPerTime { get; set; }
        // public int medicineId { get; set; }
        [Display(Name = "Medicines")]
        [DataType(DataType.MultilineText)]
        public string MedicineName { get; set; }
       
    }



}