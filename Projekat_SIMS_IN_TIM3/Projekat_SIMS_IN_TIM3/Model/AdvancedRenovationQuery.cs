﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_SIMS_IN_TIM3.Model
{
    public class AdvancedRenovationQuery
    {
        public DateRange Range { get; set; }
        public int RoomId1 { get; set; }
        public int RoomId2 { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }

        public AdvancedRenovationQuery(DateTime start, DateTime end, int id1, int id2, int duration, string description)
        {
            this.Range = new DateRange(start, end);
            this.RoomId1 = id1;
            this.RoomId2 = id2;
            this.Duration = duration;
            this.Description = description;
        }
    }
}
