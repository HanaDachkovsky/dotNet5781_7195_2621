using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace dotNet5781_03B_7195_2621
{
    class ThreadBus//information that we need to send to doWork. contain bus and graphic details it
    {
        public Bus Bus { get; set; }
        public int Seconds { get; set; }//the length of the thread, presented in the watch of each line in the list box
        public int Index { get; set; }//the index ofthe bus in the list
        public ThreadBus(Bus bus, int seconds, int index)//ctor
        {
            Bus = bus;
            Seconds = seconds;
            Index = index;
        }
    }
}
