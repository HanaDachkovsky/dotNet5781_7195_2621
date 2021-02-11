using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    /// <summary>
    ///  represents a line that passes in station
    /// </summary>
    public class StationLine
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int LastStation { get; set; }
        public string NameLastStation { get; set; }
        
    }
}
