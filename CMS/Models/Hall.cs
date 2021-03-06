﻿using System;
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
        public int HallID { get; set; }
        public string Room { get; set; }

        [DisplayName("Number of Seats")]
        public int? SeatNo { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}