﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// information about a bus that arrives at the station
    /// </summary>
    public class LineArrivalTime
    {
        public TimeSpan StartTime { get ; set; }
        public string Arrive { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public int LineCode { get; set ; }
        public string LastStation { get ; set; }


        

    }
}
