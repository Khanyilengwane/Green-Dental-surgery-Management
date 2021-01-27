using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Rooms
    {
        [Key]
        public int ids { get; set; }

        [DisplayName("Room Name")]
        public string RoomName { get; set; }

        [DisplayName("Room ")]
        public int DoctorID { get; set; }
        public virtual DoctorModel DoctorModel { get; set; }



        [DisplayName("Room ")]
        public int RoomID { get; set; }
        public virtual MyRooms MyRooms { get; set; }
    }
}