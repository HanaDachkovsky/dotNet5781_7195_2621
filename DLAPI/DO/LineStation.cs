using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{/// <summary>
/// represents a station in a line
/// </summary>
    public class LineStation
    {
        public int LineId { get; set; }
        public int Station { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }//?
        public int NextStation { get; set; }//?
    }
}

