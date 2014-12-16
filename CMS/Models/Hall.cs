using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Hall
    {
        public enum Rooms
        {
            KingFaisal,
            Room1,
            Room2
        }
        [Key]
        public int ID { get; set; }
        public Rooms Room { get; set; }

        [DisplayName("Number of Seats")]
        public int? SeatNo { get; set; }

        //public virtual ICollection<Session> Sessions { get; set; }
    }
}