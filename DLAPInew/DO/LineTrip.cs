using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// represents the 
    /// </summary>
    public class LineTrip
    {
        public int Id { get; set; }//?
        public int LineId { get; set; }//?
        public TimeSpan StartAt { get; set; }
        public TimeSpan Frequency { get; set; }
        public TimeSpan FinishAt { get; set; }
    }
}