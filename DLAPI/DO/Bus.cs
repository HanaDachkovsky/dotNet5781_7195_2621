using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// bus as vehicle 
    /// </summary>
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }
        public BusStatus Status { get; set; }



    }
}
