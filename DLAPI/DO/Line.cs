using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{/// <summary>
/// represents bus line
/// </summary>
    public class Line
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public Areas Arae { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }

    }
}
