using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class LineStation
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public double DistanceFromPrevStat { get; set; }
        public TimeSpan TimeFromPrevStat { get; set; }
        //קוד ושם תחנה
    }
}
