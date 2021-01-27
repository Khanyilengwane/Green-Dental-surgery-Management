using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class MyRooms
    {
        [Key]
        public int RoomID { get; set; }
   
        public string Category { get; set; }

      
    }
}