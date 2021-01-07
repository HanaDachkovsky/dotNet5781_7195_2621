﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Station
    {

        public int Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<StationLine> Lines { get; set; }////?

    }
}

