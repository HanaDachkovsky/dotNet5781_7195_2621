using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// represnt a bus on trip
    /// </summary>
    public class BusOnTrip
    {
        public int Id { get; set; }
        public int LicenseNum { get; set; }
        public int LineId { get; set; }
        public TimeSpan PlanedTakeOff { get; set; }
        public TimeSpan ActuallTakeOff { get; set; }
        public int PrevStation { get; set; }
        public TimeSpan PrevStationAt { get; set; }
        public TimeSpan NextStationAt { get; set; }
        //?
    }
}
