﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// represents a station
    /// </summary>
    public class Station
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
