using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace dotNet5781_03B_7195_2621
{
    class ThreadBus
    {
        public Bus Bus { get; set; }
        public TextBlock TextTime { get; set; }
        public ProgressBar ProggressTime { get; set; }
        //public Grid GridLine{get;set;}
        public int Seconds { get; set; }
        public int Index { get; set; }
        public ThreadBus(Bus bus, TextBlock text, ProgressBar progressBar, int seconds, int index)
        {
            Bus = bus;
            ProggressTime = progressBar;
            TextTime = text;
            Seconds = seconds;
            Index = index;
        }
    }
}
