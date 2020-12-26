using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    /// <summary>
    /// represnt a trip of the user
    /// </summary>
    public class Trip
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int LineId { get; set; }
        public int InStation { get; set; }
        public TimeSpan InAt { get; set; }
        public TimeSpan OutAt { get; set; }

    }
}