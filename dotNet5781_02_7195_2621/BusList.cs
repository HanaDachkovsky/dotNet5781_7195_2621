using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7195_2621
{
    class BusList : IEnumerable
    {
        List<BusLine> allBuses;
        public BusList()
        {
            allBuses = new List<BusLine>();
        }
        public IEnumerator GetEnumerator()
        {
            return allBuses.GetEnumerator();
        }
        public void Add(BusLine _line)///////////<-
        {
            foreach (BusLine item in allBuses)
            {
                if (item.BusLine == 5) ;
            }
        }
        public List<BusLine> searchLines(int code)
        {
            List<BusLine> listOfBuses = new List<BusLine>();
            foreach (BusLine item in allBuses)
            {
                if (item.CheckStation(code))
                    listOfBuses.Add(item);
            }
            if (listOfBuses.Count == 0) ;
            ///////////////////////////////////
            return listOfBuses;
        }
        public List<BusLine> sortLines()
        {
            allBuses.Sort();
            return (allBuses);
        }
        public BusLine this[int i]
        {
            get { return allBuses[i]; }
            set { allBuses[i] = value; }///////?
        }
    }
}
