using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BO
{
    /// <summary>
    /// represents a line
    /// </summary>
    public class Line
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public Enums.Areas Arae { get; set; }
        public IEnumerable<LineStation> Stations { get; set; }
        public string LastStationName { get; set; }


    }
}
