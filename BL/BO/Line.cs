using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Line
    {
        public int Id { get; set; }//
        public int Code { get; set; }
        public Enums.Areas Arae { get; set; }
        public IEnumerable<LineStation> Stations { get; set; }

    }
}
