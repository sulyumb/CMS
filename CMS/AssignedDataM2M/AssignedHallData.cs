﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CMS.Models;

namespace CMS.AssignedDataM2M
{
    public class AssignedHallData
    {

        public int HallID { get; set; }
         
        public string Room { get; set; }

        [DisplayName("Number of Seats")]
        public int? SeatNo { get; set; }

        public bool Assigned { get; set; }

    }
}