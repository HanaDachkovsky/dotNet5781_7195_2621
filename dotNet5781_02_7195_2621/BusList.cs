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
        private List<BusLine> allBuses;//list that contain all the buses
       //ctor
        public BusList()
        {
            allBuses = new List<BusLine>();
        }
        public IEnumerator GetEnumerator()
        {
            return allBuses.GetEnumerator();
        }
        public void Add(BusLine _line)//add a line 
        {
            foreach (BusLine item in allBuses)
            {
                if (item.BusLineKey == _line.BusLineKey)//check if the line is allready exist 
                {
                    if(item.FirstStation.BusStationKey!=_line.LastStation.BusStationKey || item.LastStation.BusStationKey != _line.FirstStation.BusStationKey)//if the line is back or forth line
                    {
                        throw new ArgumentException("the bus is allready exsit");
                    }
                }
            }
            allBuses.Add(_line);
        }
        public List<BusLine> searchLines(int code)
        {
            List<BusLine> listOfBuses = new List<BusLine>();//list that contain all the buses that through over 
            foreach (BusLine item in allBuses)//move all over the line buses
            {
                if (item.CheckStation(code))//if the station exist in the line, add the line to the list
                    listOfBuses.Add(item);
            }
            if (listOfBuses.Count == 0)
                throw new KeyNotFoundException("the bus number not exist");
            return listOfBuses;
        }
        public void Delete(int _line, int first, int last)//delete line
        {
            foreach (BusLine item in allBuses)
            {
                if (item.BusLineKey == _line && item.FirstStation.BusStationKey == first && item.LastStation.BusStationKey == last)
                {
                    allBuses.Remove(item);
                    return;
                }
            }
            throw new KeyNotFoundException("the bus line is not exsit");
        }

        public List<BusLine> sortLines()
        {
            allBuses.Sort();
            return (allBuses);
        }
        public List<BusLine> this[int i]//indexer
        {
            get
            {
                List<BusLine> listRe = new List<BusLine>();//list of all the line with this key(i)
                foreach (BusLine item in allBuses)
                {
                    if (item.BusLineKey == i)
                        listRe.Add(item);
                }
                if (listRe.Count == 0) 
                   throw new KeyNotFoundException("the bus number not exist");
                return listRe;
            }
        }
    }
}
