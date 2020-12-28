using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }
        public Enums.BusStatus Status { get; set; }
    }
}
