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
        private List<BusLine> allBuses;
       
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
                if (item.BusLineKey == _line.BusLineKey)
                {
                    if(item.FirstStation.BusStationKey!=_line.LastStation.BusStationKey || item.LastStation.BusStationKey != _line.FirstStation.BusStationKey)
                    {
                        //throw new;
                    }
                }
            }
            allBuses.Add(_line);
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
