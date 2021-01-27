using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Invoice
    {

        [Key]
       
        public int InvoiceID { get; set; }

        [Required]
        public int InvoiceNumber { get; set; }
        [Required]
        public string InvoiceDescription { get; set; }
        public int AppointmentID { get; set; }
        [ForeignKey("AppointmentID")]
        public virtual AppointmentModel AppointmentModel { get; set; }
    }
}