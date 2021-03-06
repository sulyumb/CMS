﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CMS.Models;

namespace CMS.ViewModels
{
    public class HallViewModel
    {


        public int HallID { get; set; }
        public string Room { get; set; }

        [DisplayName("Number of Seats")]
        public int? SeatNo { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}